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
using OpenCvSharp.Extensions;

namespace AutoHyperSpectral
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private Bitmap _bitmap;
        private string _filename;
        private VideoCapture _videoCapture;

        // GUI 
        private List<CheckBox> _checkBoxes = new List<CheckBox>();

        // 予測結果
        private Predict _predict;
        private List<List<float>> _predictBoxes = new List<List<float>>();
        private List<List<List<bool>>> _predictMasks = new List<List<List<bool>>>();
        private List<int> _predictClasses = new List<int>();

        private void OpenAvi(object sender, EventArgs e)
        {
            button2.Enabled = false;

            Dialog dialog = new Dialog();
            _filename = dialog.openDialog();

            if (_filename == "") return;

            _videoCapture = new VideoCapture(_filename);
            _bitmap = _videoCapture.ToBmp();
            pictureBox1.Image = _bitmap;
            button2.Enabled = true;
        }

        private async void PostImage(object sender, EventArgs e)
        {
            textBox1.Text = "...Loading...";
            // connect to FLASK
            if(_bitmap == null)
            {
                return; 
            }
            Http http = new Http();
            _predict = await http.PredictImage(_bitmap);

            _predictBoxes = _predict.Boxes;
            _predictMasks = _predict.Masks;
            _predictClasses = _predict.Classes;

            Console.WriteLine("SUCEESS");

            // Draw $ CreateBox
            int height = 200;
            int boxesCount = _predict.Boxes.Count;

            for (int i = 0; i < boxesCount; i++)
            {
                // create CheckBox
                var checkBox = new CheckBox();
                checkBox.Location = new System.Drawing.Point(30, height);
                checkBox.Checked = true;
                checkBox.Size = new System.Drawing.Size(75, 23);
                checkBox.Name = i.ToString();
                checkBox.TabIndex = 0;
                checkBox.Text = i.ToString();
                checkBox.UseVisualStyleBackColor = true;

                _checkBoxes.Add(checkBox);
                Controls.Add(checkBox);
                height = height + 20;
                DrawBox(i);
            }
            pictureBox1.Image = _bitmap;
            textBox1.Text = "Finish";
        }

        private void SelectLeaf(object sender, EventArgs e)
        {
            _videoCapture = new VideoCapture(_filename);
            _bitmap = _videoCapture.ToBmp();

            Console.WriteLine("--------------");
            var i = 0;
            foreach (var checkBox in _checkBoxes)
            {
                if (checkBox.Checked == true)
                {
                    DrawBox(i);
                    Console.WriteLine(i);
                }
                i++;
            }
            pictureBox1.Image = _bitmap;

        }

        // 関数化
        private void DrawBox(int i)
        {
            List<float> boxes = _predictBoxes[i];
            List<List<bool>> masks = _predictMasks[i];
            int classes = _predictClasses[i];

            Graphics graphics = CreateGraphics();
            graphics.DrawBox(_bitmap, boxes,i);
            graphics.FillLeaf(_bitmap, masks);

            graphics.Dispose();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("start");
            var i = 0;
            foreach (var checkBox in _checkBoxes)
            {
                if (checkBox.Checked == true)
                {
                    SaveToCSV(_predictMasks[i],i);
                    Console.WriteLine(i);
                }
                i++;
            }
        }
        private void SaveToCSV(List<List<bool>> masks,int index)
        {
            string savefile = "C:/Users/wakanao/source/repos/AutoHyperSpectral/" + index.ToString() + ".csv";
            using (StreamWriter streamWriter = new StreamWriter(savefile, false))
            {
                //行名を書く
                String lineName = "X,Y,";
                for (int i = 0; i < 60; i++)
                {
                    lineName = $"{lineName}" + $"band{i + 1},";

                }
                streamWriter.WriteLine(lineName);

                int imgWidth = masks[0].Count;
                int imgHeight = masks.Count;
                int j = 0;

                for (int y = 0; y < imgHeight; y++)
                {
                    //画像を生成
                    _videoCapture.PosFrames = y;
                    var mat = new Mat();
                    _videoCapture.Read(mat);

                    int interval = mat.Height / 60;

                    int l = 0;
                    for (int x = 0; x < imgWidth; x++)
                    {
                        if (masks[j][l] == true)
                        {
                            String bandStr = $"{x},{y},";
                            //60band
                            for (int i = 0; i < 60; i++)
                            {
                                Vec3b pixel = mat.At<Vec3b>(i * interval, x);
                                bandStr = bandStr + pixel.Item0 + ",";
                            }
                            streamWriter.WriteLine(bandStr);
                        }
                        l++;
                    }
                    j++;
                }
            }
        }
    }
}
