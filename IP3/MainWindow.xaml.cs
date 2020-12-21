using IP1.Imaging;
using IP1.Imaging.Filters;
using IP1.Imaging.UtilsNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IP1.Imaging.ColorNS;
//using OpenCvSharp;
//using OpenCvSharp.Extensions;
using Microsoft.Win32;

namespace IP1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        Imaging.Image loadedImage;

        Imaging.Image result2;
        Imaging.Image result1;

        double brightnessPercent=100;
        string brightnessPercentText="100";

        public MainWindow()
        {

            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(image1, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetBitmapScalingMode(image2, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetBitmapScalingMode(imageOrig, BitmapScalingMode.NearestNeighbor);
            Convert.IsEnabled = false;
            ClearResults();

            textBoxBrightness_TextChanged(null, null);
        }

        private void ButtonLoadImage_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "All images(.jpeg;*.jpg;*.png;*.ppm)|*.jpeg;*.jpg;*.png;*.ppm; " +
                        "|JPEG Files (*.jpeg;*.jpg)|*.jpeg;*.jpg" +
                        "|PNG Files (*.png)|*.png" +
                        "|PPM Files (*.ppm)|*.ppm";
            Nullable<bool> fileDialogResult = dlg.ShowDialog();

            if (fileDialogResult == true)
            {
                Imaging.Image result;
                if (dlg.FileName.Split('.').Last() == "ppm")
                    result = Imaging.Image.Load(dlg.FileName.Substring(0, dlg.FileName.Length - 4));
                else
                {
                    result = (Imaging.Image)new Bitmap(dlg.FileName);
                }


                loadedImage = result;
                imageOrig.Source = Utils.ImageToBitmapSource(result);
                Convert.IsEnabled = true;
                ClearResults();
            }
        }

        public void ClearResults()
        {
            result1 = null;
            result2 = null;
            image1.Source = null;
            image2.Source = null;

            time1Text.Content = String.Empty;
            time2Text.Content = String.Empty;
            qualityNum.Content = String.Empty;

            buttonSave1.IsEnabled = false;
            buttonSave2.IsEnabled = false;
            buttonPushLeft1.IsEnabled = false;
            buttonPushLeft2.IsEnabled = false;
        }

        public double CheckTime(Action action)
        {
            DateTime StartTime = DateTime.Now;
            action.Invoke();
            return DateTime.Now.Subtract(StartTime).TotalSeconds;
        }

        public void OnFilterFinished(double time1, double time2)
        {
            time1Text.Content = time1.ToString();
            time2Text.Content = time2.ToString();

            

            Metrics mt = new Metrics();
            qualityNum.Content = mt.CompareImage(result2, result1);

            if(radioButtonBrightness.IsChecked == true)
            {
                label1.Content = "Яркость:";
                label2.Content = "Яркость через HSV:";
            }
            else
            {
                label1.Content = "Наш вариант:";
                label2.Content = "OpenCV:";
            }

            buttonSave1.IsEnabled = true;
            buttonSave2.IsEnabled = true;
            buttonPushLeft1.IsEnabled = true;
            buttonPushLeft2.IsEnabled = true;
        }

        /*public void RunFilter(Filter filter, Func<Mat, Mat> openCVFilter)
        {

            Mat mat = ((Bitmap)loadedImage).ToMat();//.CvtColor(ColorConversionCodes.RGB2BGR);
            double timeCustom = CheckTime(() => { result1 = filter.Run(loadedImage); });
            double timeOpenCV = CheckTime(() => { mat = openCVFilter.Invoke(mat); });
            result2 = (Imaging.Image)mat.ToBitmap();


            OnFilterFinished(timeCustom, timeOpenCV);

            imageOrig.Source = Utils.ImageToBitmapSource(loadedImage);
            image1.Source = Utils.ImageToBitmapSource(result1);
            image2.Source = Utils.ImageToBitmapSource(result2);
        }

        public void RunFilterBrightness()
        {
            Imaging.Image imageHSV;
            Imaging.Image imageRGB;

            if (checkBoxOrigHsv.IsChecked == true)
            {
                imageRGB = (Imaging.Image)((Bitmap)loadedImage).ToMat().CvtColor(ColorConversionCodes.HSV2BGR_FULL).ToBitmap();
                imageHSV = loadedImage;
            }
            else
            {

                imageRGB = loadedImage;
                imageHSV = (Imaging.Image)((Bitmap)loadedImage).ToMat().CvtColor(ColorConversionCodes.BGR2HSV_FULL).ToBitmap();
            }

            double timeBrightness = CheckTime(() => { result1 = new FilterBrightness(brightnessPercent).Run(imageRGB); });

            double timeBrightnessHSV = CheckTime(() => { result2 = new FilterBrightnessHSV(brightnessPercent).Run(imageHSV); });

            OnFilterFinished(timeBrightness, timeBrightnessHSV);

            imageOrig.Source = Utils.ImageToBitmapSource(loadedImage);
            image1.Source = Utils.ImageToBitmapSource(result1);
            image2.Source = Utils.ImageToBitmapSource((Imaging.Image)((Bitmap)result2).ToMat().CvtColor(ColorConversionCodes.HSV2BGR_FULL).ToBitmap());

            

        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            Filter customFilter = null;
            Func<Mat, Mat> openCVFilter = null;

            if (radioButtonGrayScale.IsChecked == true)
            {
                customFilter = new FilterGrayScale(FilterGrayScale.GrayScaleType.Gimp);
                openCVFilter = (Mat mat) => { return mat.CvtColor(ColorConversionCodes.RGB2GRAY); };
            }
            else if (radioButtonRGB2HSV.IsChecked == true)
            {
                
                customFilter = new FilterBGR2HSV();
                openCVFilter = (Mat mat) => { return mat.CvtColor(ColorConversionCodes.BGR2HSV_FULL); };
            }
            else if (radioButtonHSV2RGB.IsChecked == true)
            {
                customFilter = new FilterHSV2BGR();
                openCVFilter = (Mat mat) => { return mat.CvtColor(ColorConversionCodes.HSV2BGR_FULL); };
            }
            else if (radioButtonBrightness.IsChecked == true)
            {
                RunFilterBrightness();

                return;
            }
            else if (radioButtonMedian.IsChecked == true)
            {
                int radius = 1;
                customFilter = new FilterMedian(radius);
                openCVFilter = (Mat mat) => { return mat.MedianBlur(radius*2 + 1); };
            }
            else if (radioButtonGaussian.IsChecked == true)
            {
                customFilter = new FilterGaussian();
                openCVFilter = (Mat mat) => { return mat.GaussianBlur(new OpenCvSharp.Size(3, 3), 5, 0); };
            }


            if (customFilter != null && openCVFilter != null)
                RunFilter(customFilter, openCVFilter);
        }*/

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            FilterHough filter = new FilterHough();

            result1 = filter.Run(loadedImage);

            image1.Source = Utils.ImageToBitmapSource(result1);

            buttonSave1.IsEnabled = true;
            buttonPushLeft1.IsEnabled = true;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            Imaging.Image imgToSave = null;
            if (sender == buttonSave2)
                imgToSave = result2;
            else if (sender == buttonSave1)
                imgToSave = result1;

            if(result1!=null)
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

                dlg.DefaultExt = ".png";
                dlg.Filter = "All images(.jpeg;*.jpg;*.png;*.ppm)|*.jpeg;*.jpg;*.png;*.ppm; " +
                            "|JPEG Files (*.jpeg;*.jpg)|*.jpeg;*.jpg" +
                            "|PNG Files (*.png)|*.png" +
                            "|PPM Files (*.ppm)|*.ppm";
                Nullable<bool> fileDialogResult = dlg.ShowDialog();

                if (fileDialogResult == true)
                {

                    ((Bitmap)imgToSave).Save(dlg.FileName);

                    
                }

            }
        }

        private void textBoxBrightness_TextChanged(object sender, TextChangedEventArgs e)
        {
            int newBrightness;

            if (int.TryParse(textBoxBrightness.Text, out newBrightness))
            {
                brightnessPercent = newBrightness;
                brightnessPercentText = textBoxBrightness.Text;
            }
            else if (textBoxBrightness.Text == String.Empty)
            {
                brightnessPercent = 0;
                brightnessPercentText = String.Empty;
            }
            else
            {
                textBoxBrightness.Text = brightnessPercentText;
            }
        }
        

        private void noiseButton_Click(object sender, RoutedEventArgs e)
        {
            ClearResults();

            label1.Content = "Шум";
            label2.Content = "";

            result1 = new NoiseFilter(30).Run(loadedImage);


            buttonSave1.IsEnabled = true;
            buttonPushLeft1.IsEnabled = true;

            image1.Source = Utils.ImageToBitmapSource(result1);
        }

        private void buttonPushLeft_Click(object sender, RoutedEventArgs e)
        {

            if (sender == buttonPushLeft1)
            {
                loadedImage = new Imaging.Image(result1);
            }
            if (sender == buttonPushLeft2)
            {
                loadedImage = new Imaging.Image(result2);
            }
            imageOrig.Source = Utils.ImageToBitmapSource(loadedImage);
        }
    }
}
