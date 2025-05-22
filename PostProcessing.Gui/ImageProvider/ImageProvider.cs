using System;
using System.Buffers;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PostProcessing.Gui
{
    public class ImageProvider : IImageProvider, IDisposable
    {
        public Dispatcher Dispatcher { get; set; }

        [DllImport("PostProcessing.Vulkan.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern InitializeResult initialize(InitializeArgs args);

        [DllImport("PostProcessing.Vulkan.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ImageSourceResult provideImageSource(ImageArgs args);

        [DllImport("PostProcessing.Vulkan.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void dispose();

        private readonly WriteableBitmap _bitmap;
        private bool _disposed;

        public ImageProvider(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;

            InitializeArgs args = new()
            {
                Path = "image.png"
            };

            InitializeResult result = initialize(args);
            _bitmap = CreateBitmap(result.Width, result.Height);
        }

        private static WriteableBitmap CreateBitmap(int width, int height)
        {
            byte[] buffer = ArrayPool<byte>.Shared.Rent(width * height * 4);

            WriteableBitmap bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            bitmap.WritePixels(new Int32Rect(0, 0, width, height), buffer, width * 4, 0);

            ArrayPool<byte>.Shared.Return(buffer);

            return bitmap;
        }

        public unsafe ImageSource? ProvideImageSource(ImageArgs args)
        {
            if (_disposed) return null;
            ImageSourceResult result = provideImageSource(args);
            if (_disposed) return null;

            Dispatcher.Invoke(new Action(() =>
            {
                if (_disposed) return;

                _bitmap.Lock();
                IntPtr bitmapPtr = _bitmap.BackBuffer;

                int length = _bitmap.PixelWidth * _bitmap.PixelHeight * 4;
                Buffer.MemoryCopy(result.ImageBuffer.ToPointer(), bitmapPtr.ToPointer(), length, length);

                _bitmap.AddDirtyRect(new Int32Rect(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
                _bitmap.Unlock();
            }));

            if (_disposed) return null;
            return _bitmap;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                dispose();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
