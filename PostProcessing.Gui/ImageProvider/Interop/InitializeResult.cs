using System.Runtime.InteropServices;

namespace PostProcessing.Gui
{
    [StructLayout(LayoutKind.Sequential)]
    public struct InitializeResult
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ReturnCode { get; set; }
    }
}
