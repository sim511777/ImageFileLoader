using FreeImageAPI;
using OpenCvSharp;
using ShimLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageFileLoader {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
        }

        IntPtr imgBuf = IntPtr.Zero;
        int imgBw = 0;
        int imgBh = 0;
        int imgBitpp = 0;
        int imgStride = 0;
        ABufferDrawer drawer = null;

        private void ClearImageBuffer() {
            if (imgBuf != IntPtr.Zero) {
                Marshal.FreeHGlobal(imgBuf);
                imgBuf = IntPtr.Zero;
            }
        }

        private void SetImageBuffer() {
            imgBox.SetImageBuffer(imgBuf, imgBw, imgBh, imgBitpp, imgStride, drawer);
            imgBox.Invalidate();
        }

        private static ABufferDrawer BufferDrawerFromBitmap(Bitmap bmp) {
            var pixelFormat = bmp.PixelFormat;
            if (pixelFormat == PixelFormat.Format1bppIndexed) {
                var palette = bmp.Palette?.Entries.Select(color => color.ToArgb()).ToArray();
                var drawer = new BufferDrawer_1BitIndexed(palette);
                return drawer;
            }
            if (pixelFormat == PixelFormat.Format4bppIndexed) {
                var palette = bmp.Palette?.Entries.Select(color => color.ToArgb()).ToArray();
                var drawer = new BufferDrawer_4BitIndexed(palette);
                return drawer;
            }
            if (pixelFormat == PixelFormat.Format8bppIndexed) {
                var palette = bmp.Palette?.Entries.Select(color => color.ToArgb()).ToArray();
                var drawer = new BufferDrawer_8BitIndexed(palette);
                return drawer;
            }
            if (pixelFormat == PixelFormat.Format24bppRgb)
                return new BufferDrawer_24BitRgb();
            if (pixelFormat == PixelFormat.Format32bppRgb
                || pixelFormat == PixelFormat.Format32bppPArgb
                || pixelFormat == PixelFormat.Format32bppArgb)
                return new BufferDrawer_32BitRgb();

            return new BufferDrawer_Unknown();
        }

        private static ABufferDrawer BufferDrawerFromMat(MatType mt) {
            ABufferDrawer drawer = null;
            if (mt == MatType.CV_8UC1) {
                drawer = new BufferDrawer_8BitIndexed(null);
            } else if (mt == MatType.CV_8UC3) {
                drawer = new BufferDrawer_24BitRgb();
            } else if (mt == MatType.CV_8UC4) {
                drawer = new BufferDrawer_32BitRgb();
            }
            if (drawer == null)
                drawer = new BufferDrawer_Unknown();

            return drawer;
        }

        private static ABufferDrawer BufferDrawerFromFreeImage(FreeImageBitmap fib) {
            var pixelFormat = fib.PixelFormat;
            if (pixelFormat == PixelFormat.Format1bppIndexed) {
                var palette = fib.Palette?.Select(rgbq => (int)rgbq.uintValue).ToArray();
                var drawer = new BufferDrawer_1BitIndexed(palette);
                return drawer;
            }
            if (pixelFormat == PixelFormat.Format4bppIndexed) {
                var palette = fib.Palette?.Select(rgbq => (int)rgbq.uintValue).ToArray();
                var drawer = new BufferDrawer_4BitIndexed(palette);
                return drawer;
            }
            if (pixelFormat == PixelFormat.Format8bppIndexed) {
                var palette = fib.Palette?.Select(rgbq => (int)rgbq.uintValue).ToArray();
                var drawer = new BufferDrawer_8BitIndexed(palette);
                return drawer;
            }
            if (pixelFormat == PixelFormat.Format24bppRgb)
                return new BufferDrawer_24BitRgb();
            if (pixelFormat == PixelFormat.Format32bppRgb
                || pixelFormat == PixelFormat.Format32bppPArgb
                || pixelFormat == PixelFormat.Format32bppArgb)
                return new BufferDrawer_32BitRgb();

            return new BufferDrawer_Unknown();
        }

        private void btnOpenGdiPlus_Click(object sender, EventArgs e) {
            if (dlgOpen.ShowDialog(this) != DialogResult.OK)
                return;

            ClearImageBuffer();

            using (var bmp = new Bitmap(dlgOpen.FileName)) {
                drawer = BufferDrawerFromBitmap(bmp);
                imgBw = bmp.Width;
                imgBh = bmp.Height;
                imgBitpp = Image.GetPixelFormatSize(bmp.PixelFormat);

                var bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
                imgStride = bd.Stride;
                long bufSize = (long)imgStride * imgBh;
                imgBuf = Marshal.AllocHGlobal((IntPtr)bufSize);
                Util.Memcpy(imgBuf, bd.Scan0, bufSize);
                bmp.UnlockBits(bd);
            }

            SetImageBuffer();
        }

        private void btnOpenOpenCV_Click(object sender, EventArgs e) {
            if (dlgOpen.ShowDialog(this) != DialogResult.OK)
                return;

            ClearImageBuffer();

            using (var mat = new Mat(dlgOpen.FileName, ImreadModes.Unchanged)) {
                drawer = BufferDrawerFromMat(mat.Type());
                imgBw = mat.Width;
                imgBh = mat.Height;
                imgBitpp = mat.ElemSize() * 8;
                imgStride = (int)mat.Step();
                long bufSize = (long)imgStride * imgBh;
                imgBuf = Marshal.AllocHGlobal((IntPtr)bufSize);
                Util.Memcpy(imgBuf, mat.Data, bufSize);
            }

            SetImageBuffer();
        }

        private void btnOpenFreeImage_Click(object sender, EventArgs e) {
            if (dlgOpen.ShowDialog(this) != DialogResult.OK)
                return;

            ClearImageBuffer();

            using (var fib = new FreeImageBitmap(dlgOpen.FileName)) {
                drawer = BufferDrawerFromFreeImage(fib);
                imgBw = fib.Width;
                imgBh = fib.Height;
                imgBitpp = fib.ColorDepth;
                imgStride = fib.Pitch;
                long bufSize = (long)imgStride * imgBh;
                imgBuf = Marshal.AllocHGlobal((IntPtr)bufSize);
                //Util.Memcpy(imgBuf, fib.Bits, bufSize);
                for (int y = 0; y < imgBh; y++) {
                    IntPtr psrc = fib.Bits + imgStride * (imgBh - 1 - y);
                    IntPtr pdst = imgBuf + imgStride * y;
                    Util.Memcpy(pdst, psrc, imgStride);
                }
            }

            SetImageBuffer();
        }
    }
}
