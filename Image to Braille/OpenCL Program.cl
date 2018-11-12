#pragma OPENCL EXTENSION cl_khr_fp64 : enable

__constant sampler_t SAMPLER = CLK_NORMALIZED_COORDS_FALSE | CLK_ADDRESS_CLAMP_TO_EDGE | CLK_FILTER_NEAREST;

__constant double3 COLOR_COEF = {0.2126, 0.7152, 0.0722};

__constant double SOBEL_MASK_HOR[9] = {-1,  0,  1,
                                       -2,  0,  2,
                                       -1,  0,  1};
__constant double SOBEL_MASK_VER[9] = {-1, -2, -1,
                                        0,  0,  0,
                                        1,  2,  1};

__constant double mask_intensity[9] = {1, 2, 1,
                                       2, 4, 2,
                                       1, 2, 1};

__constant double LAPLACIAN_MASK[9] =  {0, -1,  0,
                                       -1,  5, -1,
                                        0, -1,  0};

__constant uint INTERP_MED_MASK[9] = {1,  4, 1,
                                      4, 12, 4,
                                      1,  4, 1};
__constant uint INTERP_MED_BITS = 5;  // log2(32) = 5

__constant uint INTERP_BIG_MASK[25] = {1,  4,  6,  4, 1,
                                       4, 16, 24, 16, 4,
                                       6, 24, 36, 24, 6,
                                       4, 16, 24, 16, 4,
                                       1,  4,  6,  4, 1};
__constant uint INTERP_BIG_BITS = 8;  //log2(256) = 8


__kernel void Resize(__read_only image2d_t src, __write_only image2d_t dst){
    int x = (int)get_global_id(0);
    int y = (int)get_global_id(1);

    int sWidth = get_image_width(src);
    int sHeight = get_image_height(src);

    int dWidth = get_image_width(dst);
    int dHeight = get_image_height(dst);


    int positionX = (int)( x * ((float)sWidth / dWidth));
    int positionY = (int)( y * ((float)sHeight / dHeight));

    uint4 pixel;
    int index = 0;
    uint r = 0 , g = 0 , b = 0;
    uint luma;

    float ratio = max((float)sWidth/dWidth, (float)sHeight/dHeight);
    if(ratio > 3.0f){
        for(int i = -2; i <= 2; i++){
            for(int j = -2; j <= 2; j++){
                pixel = read_imageui(src, SAMPLER, (int2)(positionX + j, positionY + i));
                r += pixel.x * INTERP_BIG_MASK[index];
                g += pixel.y * INTERP_BIG_MASK[index];
                b += pixel.z * INTERP_BIG_MASK[index];
                index++;
            }
        }
        r >>= INTERP_BIG_BITS;
        g >>= INTERP_BIG_BITS;
        b >>= INTERP_BIG_BITS;
    }else if(ratio <= 3.0f && ratio > 2.0f){
         for(int i = -1; i <= 1; i++){
             for(int j = -1; j <= 1; j++){
                pixel = read_imageui(src, SAMPLER, (int2)(positionX + j, positionY + i));
                r += pixel.x * INTERP_MED_MASK[index];
                g += pixel.y * INTERP_MED_MASK[index];
                b += pixel.z * INTERP_MED_MASK[index];
                index++;
            }
        }
        r >>= INTERP_MED_BITS;
        g >>= INTERP_MED_BITS;
        b >>= INTERP_MED_BITS;
    }else{
        pixel = read_imageui(src, SAMPLER, (int2)(positionX, positionY));
        r = pixel.x;
        g = pixel.y;
        b = pixel.z;
    }

    luma = (uint)(r * COLOR_COEF.x + g * COLOR_COEF.y + b * COLOR_COEF.z);

    pixel.x = luma;
    pixel.y = luma;
    pixel.z = luma;
    pixel.w = read_imageui(src, SAMPLER, (int2)(x, y)).w;   //Original alpha
    pixel = clamp(pixel, (uint)0 , (uint)255);

    write_imageui(dst, (int2)(x, y), pixel);
}

__kernel void Laplacian(__read_only image2d_t src, __write_only image2d_t dst){
    int x = (int)get_global_id(0);
    int y = (int)get_global_id(1);

    uint4 pixel;
    double h = 0;
    int index = 0;

    for(int i = -1; i <= 1; i++){
        for(int j = -1; j <= 1; j++){
            pixel = read_imageui(src, SAMPLER, (int2)(x + j, y + i));
            h += (pixel.x + pixel.y + pixel.z) * LAPLACIAN_MASK[index] / 3;
            index++;
        }
    }
    int res = clamp((int)h, 0, 255);
    pixel.x = res;
    pixel.y = res;
    pixel.z = res;
    pixel.w = read_imageui(src, SAMPLER, (int2)(x, y)).w;

    write_imageui(dst, (int2)(x, y), pixel);
}

__kernel void Sobel(__read_only image2d_t src, __write_only image2d_t dst){
    int x = (int)get_global_id(0);
    int y = (int)get_global_id(1);

    uint4 pixel;
    double h = 0, v = 0;

    //Sobel operation
    int index = 0;
    for(int i=-1; i<= 1; i++){
      for(int j=-1; j<= 1; j++){
        pixel = read_imageui(src, SAMPLER, (int2)(x + j, y + i));
        h += (pixel.x + pixel.y + pixel.z) / 3 * SOBEL_MASK_HOR[index];
        v += (pixel.x + pixel.y + pixel.z) / 3 * SOBEL_MASK_VER[index];
        index++;
      }
    }

    //Calculate magnitude
    int sqr = 255 - (int)sqrt(h * h + v * v);
    sqr = clamp(sqr, 0, 255);
    pixel.x = sqr;
    pixel.y = sqr;
    pixel.z = sqr;
    pixel.w = read_imageui(src, SAMPLER, (int2)(x, y)).w;   //Original alpha

    write_imageui(dst, (int2)(x, y), pixel);
}

__kernel void Grayscale(__read_only image2d_t src, __write_only image2d_t dst){
    int x = (int)get_global_id(0);
    int y = (int)get_global_id(1);

    uint4 pixel;

    pixel = read_imageui(src, SAMPLER, (int2)(x, y));
    uint avg = (uint)((pixel.x + pixel.y + pixel.z) / 3.0);
    pixel.x = avg;
    pixel.y = avg;
    pixel.z = avg;

    write_imageui(dst, (int2)(x, y), pixel);
}

__kernel void Hybrid(__read_only image2d_t src, __write_only image2d_t dst){
    int x = (int)get_global_id(0);
    int y = (int)get_global_id(1);

    uint4 pixel;
    double h = 0, v = 0;
    double intensity = 0;
    int index = 0;


    for(int i = -1; i <= 1; i++){
        for(int j = -1; j <= 1; j++){
            pixel = read_imageui(src, SAMPLER, (int2)(x + j, y + i));
            h += (pixel.x + pixel.y + pixel.z) * SOBEL_MASK_HOR[index] / 3;
            v += (pixel.x + pixel.y + pixel.z) * SOBEL_MASK_VER[index] / 3;

            intensity += (pixel.x + pixel.y + pixel.z) / 3 * mask_intensity[index] / 16;
            index++;
        }
    }

    uint4 gray = read_imageui(src, SAMPLER, (int2)(x, y));
    uint avg = (uint)((gray.x + gray.y + gray.z) / 3.0);
    uint sqr = (uint)sqrt(h * h + v * v);
    int data = avg - sqr;
    data = clamp(data, 0, 255);

    if(intensity < 128 && sqr > 128)
        data = 255 - data;

    pixel.x = data;
    pixel.y = data;
    pixel.z = data;
    pixel.w = gray.w;   //Original alpha
    pixel = clamp(pixel, (uint)0 , (uint)255);


    write_imageui(dst, (int2)(x, y), pixel);
}

__kernel void Quantize(__read_only image2d_t src, __write_only image2d_t dst, int threshold){
    int x = (int)get_global_id(0);
    int y = (int)get_global_id(1);

    uint4 pixel;

    pixel = read_imageui(src, SAMPLER, (int2)(x, y));
    uint sum = (uint)((pixel.x + pixel.y + pixel.z) / 3.0);

    if(threshold >= 0){
        if(sum >= threshold){
            pixel.x = 255;
            pixel.y = 255;
            pixel.z = 255;
            write_imageui(dst, (int2)(x, y), pixel);
        }
        else{
            pixel.x = 0;
            pixel.y = 0;
            pixel.z = 0;
            write_imageui(dst, (int2)(x, y), pixel);
        }
    }
    else{
        threshold *= -1;

        if(sum >= threshold){
            pixel.x = 0;
            pixel.y = 0;
            pixel.z = 0;
            write_imageui(dst, (int2)(x, y), pixel);
        }
        else{
            pixel.x = 255;
            pixel.y = 255;
            pixel.z = 255;
            write_imageui(dst, (int2)(x, y), pixel);
        }
    }
}

__kernel void GenereateUnicode(__read_only image2d_t src, __global short *ptr){
    int x = get_global_id(0) * 2;
    int y = get_global_id(1) * 4;

    int pos = (y / 4) * (get_image_width(src) / 2 + 2) + (x / 2);

    int value = 0;

    value += read_imageui(src, SAMPLER, (int2)(x, y)).x         == 255 ? 0 : 1;
    value += read_imageui(src, SAMPLER, (int2)(x, y + 1)).x     == 255 ? 0 : 2;
    value += read_imageui(src, SAMPLER, (int2)(x, y + 2)).x     == 255 ? 0 : 4;
    value += read_imageui(src, SAMPLER, (int2)(x, y + 3)).x     == 255 ? 0 : 64;
    value += read_imageui(src, SAMPLER, (int2)(x + 1, y)).x     == 255 ? 0 : 8;
    value += read_imageui(src, SAMPLER, (int2)(x + 1, y + 1)).x == 255 ? 0 : 16;
    value += read_imageui(src, SAMPLER, (int2)(x + 1, y + 2)).x == 255 ? 0 : 32;
    value += read_imageui(src, SAMPLER, (int2)(x + 1, y + 3)).x == 255 ? 0 : 128;

    ptr[pos] = 0x2800 + value;

    if( (x + 2) == get_image_width(src)){
        ptr[pos+1] = 0x000D;
        ptr[pos+2] = 0x000A;
    }
}


__kernel void GammaCorrection(__read_only image2d_t src, __write_only image2d_t dst, double gamma){
    int x = get_global_id(0);
    int y = get_global_id(1);

    uint4 pixel = read_imageui(src, SAMPLER, (int2)(x, y));
    double powerIn = ((pixel.x + pixel.y + pixel.z) / 765.0); // grayscale / (3 * 255)
    double powerOut = pow(powerIn, gamma);
    double scaleFactor = powerOut / powerIn;

    pixel.x = (uint)(pixel.x * scaleFactor);
    pixel.y = (uint)(pixel.y * scaleFactor);
    pixel.z = (uint)(pixel.z * scaleFactor);
    pixel = clamp(pixel, (uint)0, (uint)255);

    write_imageui(dst, (int2)(x, y), pixel);
}

__kernel void ContrastStretch(__read_only image2d_t src, __write_only image2d_t dst, int min, int max){
    int x = get_global_id(0);
    int y = get_global_id(1);

    uint4 pixel = read_imageui(src, SAMPLER, (int2)(x, y));

    uint gray = (pixel.x + pixel.y + pixel.z) / 3;
    uint value;

    if(gray <= min)
        value = 0;
    else if(gray >= max)
        value = 255;
    else
        value = (uint)((double)(gray - min) / (max - min) * 255);

    pixel.x = value;
    pixel.y = value;
    pixel.z = value;

    write_imageui(dst, (int2)(x, y), pixel);
}
