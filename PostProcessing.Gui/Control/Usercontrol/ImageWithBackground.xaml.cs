using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PostProcessing.Gui
{
    /// <summary>
    /// Interaction logic for ImageWithBackground.xaml
    /// </summary>
    public partial class ImageWithBackground : UserControl
    {
        private ImageDrawing? _mainImage = null;

        private RectangleGeometry? _backgroundSize;

        public ImageWithBackground()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e) 
        {
            if (_backgroundSize is not null) _backgroundSize.Rect = new Rect(0, 0, Canvas.ActualWidth, Canvas.ActualHeight);
            CenterImage();
        }

        public void DisplayImage(ImageSource source) 
        {
            Rect rectangle = new(0, 0, source.Width, source.Height);

            if (_mainImage == null)
            {
                _mainImage = new ImageDrawing(source, rectangle);
                Canvas.DrawingGroup.Children.Add(_mainImage);
            }
            else if (_mainImage.ImageSource != source) 
            {
                Canvas.DrawingGroup.Children.Remove(_mainImage);

                _mainImage = new ImageDrawing(source, rectangle);
                Canvas.DrawingGroup.Children.Add(_mainImage);
            }
            else
            {
                _mainImage.Rect = rectangle;
            }

            CenterImage();
        }

        private void CenterImage() 
        {
            if (_mainImage is null) return;

            double imageRatio = _mainImage.ImageSource.Width / _mainImage.ImageSource.Height;
            double controlRatio = Canvas.ActualWidth / Canvas.ActualHeight;

            double width, height;

            if (imageRatio >= controlRatio)
            {
                width = Canvas.ActualWidth;
                height = width / imageRatio;
            }
            else 
            {
                height = Canvas.ActualHeight;
                width = height * imageRatio;
            }

            width = _mainImage.ImageSource.Width;
            height = _mainImage.ImageSource.Height;

            _mainImage.Rect = new Rect((Canvas.ActualWidth - width) / 2, (Canvas.ActualHeight - height) / 2, width, height);
        }
    }
}
