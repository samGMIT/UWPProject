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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace UWPProject2017
{
    public sealed partial class Carousel : UserControl
    {
        public Carousel()
        {
            this.InitializeComponent();
            init();
        }
        private Windows.UI.Xaml.Media.Animation.Storyboard animation =
    new Windows.UI.Xaml.Media.Animation.Storyboard();
        private List<CarouselImage> list = new List<CarouselImage>();

        private bool playing = true;

        
       
        private void layout(ref Canvas display)
        {
            display.Children.Clear();
            for (int index = 0; index < list.Count(); index++)
            {
                CarouselImage carouselImage = list[index];
                Image item = new Image();
                item.Width = 150;
                item.Source = carouselImage.Bitmap;
                carouselImage.Angle = index * ((Math.PI * 2) / list.Count);
                carouselImage.InitializePosition();

 
                Canvas.SetLeft(item, carouselImage.Position.X - (item.Width - CarouselImage.Perspective));
                Canvas.SetTop(item, carouselImage.Position.Y);
                carouselImage.Distance = 1 / (1 - (carouselImage.Position.X / CarouselImage.Perspective));
                item.RenderTransform = new ScaleTransform();
                item.Opacity = ((ScaleTransform)item.RenderTransform).ScaleX =
                    ((ScaleTransform)item.RenderTransform).ScaleY = carouselImage.Distance;
                
                item.Tag = carouselImage;
                display.Children.Add(item);
            }
        }

        private void rotate()
        {
            if (!playing)
            {
                animation.Begin();
                return;
            }
            int itemNo = 0;
            foreach (Image item in Display.Children)
            {
                CarouselImage carouselImage = (CarouselImage)item.Tag;

                carouselImage.UpdatePosition();
                Canvas.SetLeft(item, carouselImage.Position.X - (item.Width - CarouselImage.Perspective));
                Canvas.SetTop(item, carouselImage.Position.Y);
                carouselImage.UpdateZOrder();
                Canvas.SetZIndex(item, carouselImage.getZOrder());

                

                    // item.Opacity = ((ScaleTransform)item.RenderTransform).ScaleX =
                    //   ((ScaleTransform)item.RenderTransform).ScaleY = carouselImage.Distance;
                itemNo++;
            }
            animation.Begin();
        }
        public void Pause()
        {
            this.playing = false;

        }
        public void Play()
        {
            this.playing = true;

        }


        private void init()
        {
            animation.Completed += (object sender, object e) =>
            {
                rotate();
            };
            animation.Begin();
        }

        public void Add(Windows.UI.Xaml.Media.Imaging.BitmapImage image)
        {
            Point radius = getNextRadius();
            list.Add(new CarouselImage ( image, (int)radius.X, (int)radius.Y));
            layout(ref Display);
        }
        private Point getNextRadius()
        {
            if(list.Count % 2 == 0)
            {
                return new Point(-50, 200);
            }
            else
            {
                return new Point(50, 200);
            }
        }

        public void RemoveLast()
        {
            if (list.Any())
            {
                list.Remove(list.Last());
                layout(ref Display);
            }
        }

        public void New()
        {
            list.Clear();
            layout(ref Display);
        }
    }
}
