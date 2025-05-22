
using System.Runtime.InteropServices;

namespace PostProcessing.Gui
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ImageArgs
    {
        public ulong Milliseconds { get; set; }

        public ImageArgs(ulong milliseconds)
        {
            Milliseconds = milliseconds;
        }
    }
}
