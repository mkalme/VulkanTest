using System.Windows.Media;

namespace PostProcessing.Gui
{
    public interface IImageProvider
    {
        ImageSource? ProvideImageSource(ImageArgs args);
    }
}
