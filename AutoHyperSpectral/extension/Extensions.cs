using System.Collections.Generic;
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
            _bitmap = BitmapConverter.ToBitmap(viewMat);
            return _bitmap;
        }

        public static void DrawBox(this Graphics graphics, Bitmap bitmap,List<float> boxes, int index)
        {
            graphics = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Red, 2);
            graphics.DrawString(index.ToString(), new Font("MS UI Gothic", 15), Brushes.White, boxes[0], boxes[1]);
            graphics.DrawRectangle(pen, boxes[0], boxes[1], boxes[2] - boxes[0], boxes[3] - boxes[1]);

            pen.Dispose();
        }

        public static void FillLeaf(this Graphics graphics, Bitmap bitmap, List<List<bool>> masks)
        {
            graphics = Graphics.FromImage(bitmap);

            int imgWidth = masks[0].Count;
            int imgHeight = masks.Count;
            int j = 0;

            for (int y = 0; y < imgHeight; y++)
            {
                int l = 0;
                for (int x = 0; x < imgWidth; x++)
                {
                    if (masks[j][l] == true)
                    {
                        Pen pen = new Pen(Color.FromArgb(40, Color.Green), 1);
                        graphics.DrawRectangle(pen, new Rectangle(x, y, 1, 1));
                        pen.Dispose();
                    }
                    l++;
                }
                j++;
            }
           
        }
    }
}