using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace PostProcessing.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IImageProvider ImageProvider { get; }

        private bool _closed = false;
        private string _title;

        public MainWindow(IImageProvider imageProvider)
        {
            InitializeComponent();

            ImageProvider = imageProvider;
            _title = Title;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                DateTime last = DateTime.Now;
                int fps = 0;

                while (!_closed)
                {
                    ImageArgs args = new((ulong)DateTime.Now.Millisecond);
                    ImageSource? imageSource = ImageProvider.ProvideImageSource(args);

                    fps++;

                    TimeSpan now = DateTime.Now - last;
                    if (now.TotalSeconds >= 1)
                    {
                        int estimatedFps = (int)(fps / now.TotalSeconds);
                        string title = $"{_title} | FPS: {estimatedFps}";

                        if (_closed) return;
                        ImageContainer.Dispatcher.Invoke(() =>
                        {
                            if (!_closed) Title = title;
                        });

                        fps = 0;
                        last = DateTime.Now;
                    }

                    if (imageSource is null || _closed) continue;
                    ImageContainer.Dispatcher.Invoke(() =>
                    {
                        if (!_closed) ImageContainer.DisplayImage(imageSource);
                    });
                }
            }).Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _closed = true;
        }
    }
}
