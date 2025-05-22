using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PostProcessing.Gui
{
    /// <summary>
    /// Interaction logic for CanvasControl.xaml
    /// </summary>
    public partial class CanvasControl : UserControl
    {
        public DrawingGroup DrawingGroup { get; } = new DrawingGroup();

        public CanvasControl()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.PushClip(new RectangleGeometry(new Rect(0, 0, ActualWidth, ActualHeight)));
            drawingContext.DrawDrawing(DrawingGroup);
        }
    }
}
