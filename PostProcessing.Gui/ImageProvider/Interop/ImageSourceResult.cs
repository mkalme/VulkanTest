using System;
using System.Runtime.InteropServices;

namespace PostProcessing.Gui
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ImageSourceResult
    {
        public IntPtr ImageBuffer { get; set; }
    }
}
