using System.Runtime.InteropServices;

namespace PostProcessing.Gui
{
    [StructLayout(LayoutKind.Sequential)]
    public struct InitializeArgs
    {
        public string Path { get; set; }
    }
}
