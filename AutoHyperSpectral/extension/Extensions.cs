using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static Bitmap ToBmp(this VideoCapture capture)
        {
            Bitmap _bitmap;
            int frameCount = capture.FrameCount;
            //画像を作り高さと幅を作るだけ
            Mat originalMat = new Mat();
            capture.Read(originalMat);

            int wavelengthsRange = originalMat.Height;
            int width = originalMat.Width;

            originalMat.Dispose();

            //疑似近赤外の画像を書き出す
            int band40 = wavelengthsRange / 60 * 40;
            int band30 = wavelengthsRange / 60 * 30;
            int band20 = wavelengthsRange / 60 * 20;
            Mat viewMat = new Mat(frameCount, width, MatType.CV_8UC3);

            for (int i = 0; i < frameCount; i++)
            {
                Mat mat = new Mat();
                bool success = capture.Read(mat);
                if (!success) break;
                for (int j = 0; j < width; j++)
                {
                    byte NIR = mat.At<Vec3b>(band40, j).Item0;
                    byte red = mat.At<Vec3b>(band30, j).Item0;
                    byte green = mat.At<Vec3b>(band20, j).Item0;
                    Vec3b pixel = new Vec3b(green, red, NIR);
                    viewMat.Set(i, j, pixel);
                }
                mat.Dispose();
            }
            capture.Dispose();


            _bitmap = BitmapConverter.ToBitmap(viewMat);
            return _bitmap;
        }
    }
}