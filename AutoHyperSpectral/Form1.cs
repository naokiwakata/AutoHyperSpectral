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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            Http http = new Http();

            Predict predict = await http.PredictImage(_bitmap);
            this._predict = predict;
            Console.WriteLine("SUCEESS");
        }
    }
}
