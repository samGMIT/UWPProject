
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPProject2017
{
    class CarouselImage
    {
        public  const double Perspective = 90;

        public Point Position { get;  set; } 
        public Point Radius { get ; set; }
        public double Speed { get; set; }
        
        public double Distance { get; set; }
        public double Angle { get; set; }
        public Windows.UI.Xaml.Media.Imaging.BitmapImage Bitmap { get; set; }
        

        public CarouselImage(BitmapImage image, int radiusX, int radiusY)
        {
            Bitmap = image;
            Radius = new Point(radiusX, radiusY);
            
            Speed = 0.0125;


        }
        public void InitializePosition()
        {
            Position = new Point(Math.Cos(Angle) * Radius.X, Math.Sin(Angle) * Radius.Y);

            
        }
        public void UpdatePosition()
        {
            Angle -= Speed;
            Position = new Point(Math.Cos(Angle) * Radius.X, Math.Sin(Angle) * Radius.Y);
           
        }
        public void UpdateZOrder()
        {
            double extent = Math.Sqrt(Position.X * Position.X + Position.Y * Position.Y );
            if (Radius.X >= 0)
            {

                Distance = 1 * (1 - (extent / Perspective));
               
            }
            else
            {
                Distance = 1 / (1 - (extent / Perspective));
                
            }

        }
        public int getZOrder()
        {
            if (Radius.X >= 0)
            {
                return (int)-Position.X;
            }
            else
            {
               return (int) Position.X;
            }

        }
       


    }
}
