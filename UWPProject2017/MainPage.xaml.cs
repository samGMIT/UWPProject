using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Media.Capture;
using Windows.Storage;

using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPProject2017
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int photoNumber = 1;

        public MainPage()
        {
            this.InitializeComponent();
        }
       async private void AddtoDisplay(String filename)
        {
                
            Windows.Storage.StorageFile sampleFile = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);

            BitmapImage bitmapImage = new BitmapImage();
            FileRandomAccessStream stream = (FileRandomAccessStream)await sampleFile.OpenAsync(FileAccessMode.Read);

            bitmapImage.SetSource(stream);


            //BitmapImage pic = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(Value.Text));
            Display.Add(bitmapImage);
           
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Display.New();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Display.RemoveLast();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();

            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);
           
            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }
            StorageFolder destinationFolder = ApplicationData.Current.LocalFolder;
            //await ApplicationData.Current.LocalFolder.CreateFolderAsync("ProfilePhotoFolder",
            //  CreationCollisionOption.OpenIfExists);
            String picName = "ProfilePhoto" + photoNumber + ".jpg";
            photoNumber++;

            await photo.CopyAsync(destinationFolder, picName, NameCollisionOption.ReplaceExisting);
            AddtoDisplay(picName);
            
       

        }

        private void playPauseButton_Click(object sender, RoutedEventArgs e)
        {
            

            if(String.Compare(playPauseButton.Content.ToString(), "Pause") == 0)
            {
                Display.Pause();
                playPauseButton.Content = "Play";
            }
            else
            {
                Display.Play();
                playPauseButton.Content = "Pause";
            }

        }
    }
}
