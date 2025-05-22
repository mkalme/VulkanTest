using System.Windows;

namespace PostProcessing.Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ImageProvider imageProvider = new(Dispatcher);

            MainWindow window = new(imageProvider);
            window.Closing += (sender, args) => imageProvider.Dispose();
            
            window.ShowDialog();
        }
    }
}
