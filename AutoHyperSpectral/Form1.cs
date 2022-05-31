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
            MemoryStream ms = new MemoryStream();
            _bitmap.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            string imageBase64 = Convert.ToBase64String(byteImage);

            var parameters = new Dictionary<string, string>()
            {
                { "post_img", imageBase64},
            };

            using (var client = new HttpClient())
            {
                string jsonString = JsonSerializer.Serialize(parameters);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync($"http://127.0.0.1:5000/findHyperLeaf", content);
                var b = response.Content;

                if (response.IsSuccessStatusCode)
                {
                    //レスポンスからJSON文字列を取得
                    string contentStream =  await response.Content.ReadAsStringAsync();

                    Predict predict = JsonSerializer.Deserialize<Predict>(contentStream);
                    Console.WriteLine(predict.Boxes[0]);
                }
                else
                {
                    MessageBox.Show("Failed to communicate");
                }
            }
        }
    }
}
