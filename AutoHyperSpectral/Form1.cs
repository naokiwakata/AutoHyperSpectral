using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using AutoHyperSpectral;
using ExtensionMethods;
using AutoHyperSpectral.util;
using System.IO;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoHyperSpectral
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap _bitmap;
        private Predict _predict;

        private void openAvi(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;

            Dialog dialog = new Dialog();
            string filename = dialog.openDialog();

            if (filename == "") return;

            VideoCapture videoCapture = new VideoCapture(filename);

            _bitmap = videoCapture.ToBmp();

            String jpgFileName = filename.Split('.')[0] + ".jpg";
            /* _bitmap.Save(
               jpgFileName,
               System.Drawing.Imaging.ImageFormat.Jpeg
           );*/
            pictureBox1.Image = _bitmap;
            button2.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            if(_bitmap == null)
            {
                return; 
            }
            Http http = new Http();

            Predict predict = await http.PredictImage(_bitmap);
            this._predict = predict;
            Console.WriteLine("SUCEESS");
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(_predict == null)
            {
                return;
            }
            int numberBoxes = _predict.Boxes.Count;

            for(int i = 0; i < numberBoxes; i++)
            {
                List<float> boxes = _predict.Boxes[i];
                List<List<bool>> masks = _predict.Masks[i];
                int classes = _predict.Classes[i];

                //短径を描画
                Graphics graphics = CreateGraphics();
                graphics.DrawBox(_bitmap, boxes);
                graphics.Dispose();

                //葉部分を塗りつぶす
                graphics = Graphics.FromImage(_bitmap);

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

                            //半透明のPenを作成する
                            Pen p = new Pen(Color.FromArgb(40, Color.Green), 1);
                            //四角を描画する
                            graphics.DrawRectangle(p, new Rectangle(x, y, 1, 1));
                        }
                        l++;
                    }
                    j++;
                }
                graphics.Dispose();
                pictureBox1.Image = _bitmap;
            }
        }
    }
}
