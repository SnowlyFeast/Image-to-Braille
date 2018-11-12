using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using OpenCL.Components;
using OpenCL.Manager;

namespace Image_to_Braille
{
    public partial class Form1 : Form
    {
        private enum MaskingFormat
        {
            Grayscale,
            Sobel
        }
        private enum SpacingCharacter
        {
            ForSteam,
            Braille1,
            Braille0,
            NormalSpace
        }

        private MaskingFormat maskingFormat;
        private SpacingCharacter spacingCharacter;
        private Point originalResolution;
        private Point desiredResolution;
        private bool keepAspectRatio;
        private bool fixedForSteamSize;
        private bool useOriginalSize;

        private double aspectRatio;

        private int threshold;
        private bool inverseBlackAndWhite;
        private bool applyLaplacian;
        private bool inputImageReady;
        
        Bitmap bitmapOriginal, bitmapResized, bitmapMasked, bitmapQuantized;
        ImageProcessor imageProcessor;

        //Spacing Chararter
        private static readonly string[] SPACES = {
            ((char)0x2000).ToString() + ((char)0x200A).ToString() + ((char)0x200A).ToString() + ((char)0x200A).ToString(),
            ((char)0x2801).ToString(),
            ((char)0x2800).ToString(),
            " " };

        //Breille character aspect ratio
        private const double CHAR_ASPECT_RATIO = 20 / 17;

        //Filename
        private const string CONFIG_FILENAME = "./config.cfg";
        private const string LOG_FILE_NAME = "./log.txt";

        //Source program path. Text file is embedded resource.
        private const string PROGRAM_PATH = "Image_to_Braille.OpenCl Program.cl";

        public Form1()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
            
            //Set ComboBox
            foreach (MaskingFormat f in (MaskingFormat[])Enum.GetValues(typeof(MaskingFormat)))
                comboBox_maskFormat.Items.Add(f.ToString());

            foreach (SpacingCharacter c in (SpacingCharacter[])Enum.GetValues(typeof(SpacingCharacter)))
                comboBox_spacingChar.Items.Add(c.ToString());


            Initialize();
            //Load config
            ClDeviceIndex targetDeviceIndex = ConfigManager.LoadConfig(CONFIG_FILENAME);

            if (targetDeviceIndex.Platform == -1 || targetDeviceIndex.Device == -1)
            {
                //Reset config if config is missing
                ConfigManager.RemoveConfig(CONFIG_FILENAME);
                targetDeviceIndex = ConfigManager.LoadConfig(CONFIG_FILENAME);
            }
            
            //Validate device
            if (!ClDeviceManager.ValidateDevice(targetDeviceIndex))
            {
                //Reset config if config is not valid
                ConfigManager.RemoveConfig(CONFIG_FILENAME);
                targetDeviceIndex = ConfigManager.LoadConfig(CONFIG_FILENAME);

                //If device is not valide, force close.
                if (!ClDeviceManager.ValidateDevice(targetDeviceIndex))
                {
                    MessageBox.Show("Target device is not valid."
                        + Environment.NewLine + "Check config.cfg and try other options.");
                    this.Load += (s, e) => Close();
                }
            }
            
            //Setup OpenCL device
            imageProcessor = new ImageProcessor(targetDeviceIndex, LoadProgramText());

            if (!imageProcessor.IsReady)
                this.Load += (s, e) => Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            imageProcessor.Dispose();
        }
        
        private void Initialize()
        {
            //Internal variable reset
            maskingFormat = MaskingFormat.Grayscale;
            spacingCharacter = SpacingCharacter.ForSteam;
            originalResolution = new Point(40, 40);
            desiredResolution = new Point(40, 40);
            keepAspectRatio = true;
            fixedForSteamSize = true;
            useOriginalSize = false;
            aspectRatio = 1.0;
            threshold = 128;
            inverseBlackAndWhite = false;
            applyLaplacian = false;

            //Winform reset
            comboBox_maskFormat.SelectedIndex = 0;
            comboBox_spacingChar.SelectedIndex = 0;
            comboBox_previewScaling.SelectedIndex = 0;
            checkBox_keepAspectRatio.Checked = keepAspectRatio;
            checkBox_steamFixed.Checked = fixedForSteamSize;
            checkBox_useOriginalSize.Checked = useOriginalSize;
            numeric_threshold.Value = threshold;
            trackBar_threshold.Value = threshold;
            checkBox_inverseBW.Checked = inverseBlackAndWhite;
            checkBox_applyLaplacian.Checked = applyLaplacian;
            groupBox_setting.Enabled = false;
            
            //Image holder reset
            pictureBox_original.Image = null;
            pictureBox_masked.Image = null;
            pictureBox_quantized.Image = null;

            //Bitmap holder reset
            if (bitmapOriginal != null)
                bitmapOriginal.Dispose();

            if (bitmapResized != null)
                bitmapResized.Dispose();

            if (bitmapMasked != null)
                bitmapMasked.Dispose();

            if (bitmapQuantized != null)
                bitmapQuantized.Dispose();

            bitmapOriginal = null;
            bitmapResized = null;
            bitmapMasked = null;
            bitmapQuantized = null;

            //Ready flag reset
            inputImageReady = false;
        }

        private void LoadImage(Image image) {
            bitmapOriginal = (Bitmap)image;
            originalResolution = new Point(bitmapOriginal.Width, bitmapOriginal.Height);
            aspectRatio = (double)originalResolution.X / originalResolution.Y;
            label_sourceResolution.Text = "Original Resolution (" + originalResolution.X + ", " + originalResolution.Y + ")";

            pictureBox_original.Image = bitmapOriginal;
            SetDesiredSize(true);

            groupBox_setting.Enabled = true;
            inputImageReady = true;
        }
        
        private string LoadProgramText()
        {
            try
            {
                //Load program code
                string programText;
                using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(PROGRAM_PATH)))
                {
                    programText = streamReader.ReadToEnd();
                }
                return programText;
            }
            catch (Exception exception)
            {
                SaveErrorLog(exception);
            }
            //Else return empty string
            return String.Empty;
        }
        

        public static void SaveErrorLog(Exception exception)
        {
            string errorString = DateTime.Now.ToString() + "\t" + exception.Message
                + Environment.NewLine + exception.StackTrace;

            SaveErrorLog(errorString);
        }

        public static void SaveErrorLog(string message)
        {
            string text = DateTime.Now.ToString() + "\t" + message;
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(LOG_FILE_NAME, true))
                {
                    streamWriter.WriteLine(text);
                }
                MessageBox.Show("Error occurred. Check log.txt");
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to save log." + Environment.NewLine + e.Message + Environment.NewLine + message);
            }
        }


        private void FromResizing()
        {
            //Image not ready
            if (!inputImageReady)
            {
                MessageBox.Show("Image is not ready.");
                return;
            }

            //Generate resized bitmap
            if (bitmapResized != null)
                bitmapResized.Dispose();
            bitmapResized = new Bitmap(desiredResolution.X, desiredResolution.Y);
            imageProcessor.ResizeImage(bitmapOriginal, bitmapResized);

            //Apply laplacian
            if (applyLaplacian)
            {
                Bitmap temp = (Bitmap)bitmapResized.Clone();
                bitmapResized.Dispose();
                bitmapResized = new Bitmap(temp.Width, temp.Height);
                imageProcessor.ApplyLaplacian(temp, bitmapResized);
                temp.Dispose();
            }

            //Next step
            FromApplyingMask();
        }
        
        private void FromApplyingMask()
        {
            //Image not ready
            if (!inputImageReady)
            {
                MessageBox.Show("Image is not ready.");
                return;
            }

            //Generate masked image
            if (bitmapMasked != null)
                bitmapMasked.Dispose();
            bitmapMasked = new Bitmap(bitmapResized.Width, bitmapResized.Height);

            switch (maskingFormat)
            {
                case MaskingFormat.Grayscale:
                    imageProcessor.ApplyGrayScaleMask(bitmapResized, bitmapMasked);
                    break;

                case MaskingFormat.Sobel:
                    imageProcessor.ApplySobelMask(bitmapResized, bitmapMasked);
                    break;

                //case MaskingFormat.Hybrid:
                //    imageProcessor.ApplyHybridMask(bitmapResized, bitmapMasked);
                //    break;
            }

            //Update picturebox
            pictureBox_masked.Image = bitmapMasked;

            //Next step
            FromQuantizing();
        }

        private void FromQuantizing()
        {
            //Image not ready
            if (!inputImageReady)
            {
                MessageBox.Show("Image is not ready.");
                return;
            }

            //Generate quantized image
            if (bitmapQuantized != null)
                bitmapQuantized.Dispose();
            bitmapQuantized = new Bitmap(bitmapMasked.Width, bitmapMasked.Height);

            imageProcessor.QuantizeImage(bitmapMasked, bitmapQuantized, threshold * (inverseBlackAndWhite == true ? -1 : 1));
            
            //Update picturebox
            pictureBox_quantized.Image = bitmapQuantized;
        }

        private void GenerateBraille()
        {
            //Generate Unicode
            richTextBox_result.Text = imageProcessor.GenerateUnicode(bitmapQuantized, SPACES[(int)spacingCharacter]);

            richTextBox_result.Focus();
            richTextBox_result.SelectAll();
        }

        //Tool Strip
        private void File_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff"
                + "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Initialize();
                    LoadImage(Image.FromFile(dialog.FileName));
                }
                catch (Exception exception)
                {
                    SaveErrorLog(exception);
                }

                FromResizing();
            }
        }
        


        //Operation Format Settings
        private void ComboBox_maskFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!inputImageReady)
                return;

            switch (comboBox_maskFormat.SelectedIndex)
            {
                default:
                case (int)MaskingFormat.Grayscale:
                    maskingFormat = MaskingFormat.Grayscale;
                    break;
                    
                case (int)MaskingFormat.Sobel:
                    maskingFormat = MaskingFormat.Sobel;
                    break;

                //case 2:
                //    maskingFormat = MaskingFormat.Hybrid;
                //    break;
            }

            FromApplyingMask();
        }


        //Resolution Settings
        private void SetDesiredSize(bool fromWidth)
        {
            //Enable/Disable control
            if (fixedForSteamSize)
            {
                numeric_width.Enabled = false;

                if (keepAspectRatio)
                    numeric_height.Enabled = false;
                else
                    numeric_height.Enabled = true;
            }
            else
            {
                numeric_width.Enabled = true;
                numeric_height.Enabled = true;
            }


            int x, y;

            if (!useOriginalSize) //Use custom size
            {
                if (fixedForSteamSize)
                {
                    if (keepAspectRatio)
                    {
                        x = 118;
                        y = (int)(x * CHAR_ASPECT_RATIO / aspectRatio + 0.5);
                    }
                    else
                    {
                        x = 118;
                        y = (int)numeric_height.Value;
                    }
                }
                else
                {
                    if (keepAspectRatio) //Keep aspect ratio
                    {
                        Point size;
                        if (fromWidth)
                            size = SetDesiredSizeFromWidth();
                        else
                            size = SetDesiredSizeFromHeight();

                        x = size.X;
                        y = size.Y;
                    }
                    else //Custom ratio
                    {
                        x = (int)numeric_width.Value;
                        y = (int)numeric_height.Value;
                    }
                }
                

            }
            else //Use original size
            {
                x = originalResolution.X;
                y = originalResolution.Y;
            }

            //Remove odd
            x -= (x % 2);
            y -= (y % 4);
            desiredResolution = new Point(x, y);

            //Avoid looping
            numeric_width.ValueChanged -= Numeric_width_ValueChanged;
            numeric_height.ValueChanged -= Numeric_height_ValueChanged;

            numeric_width.Value = desiredResolution.X;
            numeric_height.Value = desiredResolution.Y;

            numeric_width.ValueChanged += Numeric_width_ValueChanged;
            numeric_height.ValueChanged += Numeric_height_ValueChanged;
        }

        private Point SetDesiredSizeFromWidth()
        {
            int x, y;

            x = (int)numeric_width.Value;
            
            if (x < 40)  //Minimum width
                x = 40;
            
            y = (int)(x * CHAR_ASPECT_RATIO / aspectRatio + 0.5);
            
            //If Y is smaller than 40, resize.
            if (y < 40)
            {
                y = 40;
                x = (int)(y / CHAR_ASPECT_RATIO * aspectRatio + 0.5);
            }

            return new Point(x, y);
        }
        private Point SetDesiredSizeFromHeight()
        {
            int x, y;

            y = (int)numeric_height.Value;

            if (y < 40) //Minimum height
                y = 40;

            x = (int)(y / CHAR_ASPECT_RATIO * aspectRatio + 0.5);

            //If X is smaller than 40, resize.
            if(x < 40)
            {
                x = 40;
                y = (int)(x * CHAR_ASPECT_RATIO / aspectRatio + 0.5);
            }

            return new Point(x, y);
        }

        private void Numeric_width_ValueChanged(object sender, EventArgs e)
        {
            SetDesiredSize(true);
            FromResizing();
        }
        private void Numeric_height_ValueChanged(object sender, EventArgs e)
        {
            SetDesiredSize(false);
            FromResizing();
        }
        private void CheckBox_steamFixed_CheckedChanged(object sender, EventArgs e)
        {
            fixedForSteamSize = checkBox_steamFixed.Checked;

            SetDesiredSize(true);
            FromResizing();
        }
        private void CheckBox_keepAspectRatio_CheckedChanged(object sender, EventArgs e)
        {
            keepAspectRatio = checkBox_keepAspectRatio.Checked;

            SetDesiredSize(true);
            FromResizing();
        }
        private void CheckBox_useOriginalSize_CheckedChanged(object sender, EventArgs e)
        {
            useOriginalSize = checkBox_useOriginalSize.Checked;
            panel_resolution.Enabled = !checkBox_useOriginalSize.Checked;

            SetDesiredSize(true);
            FromResizing();
        }


        //Threshold Settings
        private void Numeric_threshold_ValueChanged(object sender, EventArgs e)
        {
            //Avoid looping
            numeric_threshold.ValueChanged -= Numeric_threshold_ValueChanged;
            trackBar_threshold.ValueChanged -= TrackBar_threshold_Scroll;

            threshold = (int)numeric_threshold.Value;
            trackBar_threshold.Value = threshold;
            
            numeric_threshold.ValueChanged += Numeric_threshold_ValueChanged;
            trackBar_threshold.ValueChanged += TrackBar_threshold_Scroll;


            FromQuantizing();
        }
        
        private void TrackBar_threshold_Scroll(object sender, EventArgs e)
        {
            //Avoid looping
            numeric_threshold.ValueChanged -= Numeric_threshold_ValueChanged;
            trackBar_threshold.ValueChanged -= TrackBar_threshold_Scroll;

            threshold = trackBar_threshold.Value;
            numeric_threshold.Value = threshold;

            numeric_threshold.ValueChanged += Numeric_threshold_ValueChanged;
            trackBar_threshold.ValueChanged += TrackBar_threshold_Scroll;


            FromQuantizing();
        }

        private void CheckBox_inverseBW_CheckedChanged(object sender, EventArgs e)
        {
            inverseBlackAndWhite = checkBox_inverseBW.Checked;

            FromQuantizing();
        }
        
        private void CheckBox_applyLaplacian_CheckedChanged(object sender, EventArgs e)
        {
            applyLaplacian = checkBox_applyLaplacian.Checked;

            FromResizing();
        }


        //Spacing Character Settings
        private void ComboBox_spacingChar_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_spacingChar.SelectedIndex)
            {
                default:
                case (int)SpacingCharacter.ForSteam:
                    spacingCharacter = SpacingCharacter.ForSteam;
                    break;

                case (int)SpacingCharacter.Braille1:
                    spacingCharacter = SpacingCharacter.Braille1;
                    break;

                case (int)SpacingCharacter.Braille0:
                    spacingCharacter = SpacingCharacter.Braille0;
                    break;

                case (int)SpacingCharacter.NormalSpace:
                    spacingCharacter = SpacingCharacter.NormalSpace;
                    break;
            }

            SetSpacingCharacter(SPACES[(int)spacingCharacter]);
        }


        private void SetSpacingCharacter(String text)
        {
            label_spacing.Text = "]" + text + "[";
        }

        //Preview Settings
        private void ComboBox_previewScaling_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_previewScaling.SelectedIndex)
            {
                default:
                case 0:
                    pictureBox_original.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox_masked.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox_quantized.SizeMode = PictureBoxSizeMode.Zoom;
                    break;
                case 1:
                    pictureBox_original.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox_masked.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox_quantized.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 2:
                    pictureBox_original.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox_masked.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox_quantized.SizeMode = PictureBoxSizeMode.CenterImage;
                    break;

            }
        }


        //Buttons
        private void Button_genBraille_Click(object sender, EventArgs e)
        {
            GenerateBraille();
        }

    }
}
