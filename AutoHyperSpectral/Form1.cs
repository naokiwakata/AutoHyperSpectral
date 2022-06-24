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
using AutoHyperSpectral.domain;

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

        // 葉っぱ検出予測結果
        private LeafPredict _leafPredict;
        private List<List<float>> _predictBoxes = new List<List<float>>();
        private List<List<List<bool>>> _predictMasks = new List<List<List<bool>>>();
        private List<int> _predictClasses = new List<int>();

        // 病気予測結果
        private DiseasePredict _diseasePredict;

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
            _leafPredict = await http.PredictImage(_bitmap);
            _predictBoxes = _leafPredict.Boxes;
            _predictMasks = _leafPredict.Masks;
            _predictClasses = _leafPredict.Classes;

            Console.WriteLine("SUCEESS");

            int boxesCount = _leafPredict.Boxes.Count;

            for (int i = 0; i < boxesCount; i++)
            {
                CreateCheckBox(i);
                DrawBox(i);
            }
            pictureBox1.Image = _bitmap;
            textBox1.Text = "Finish!!!";
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
        private void SaveCSV(object sender, EventArgs e)
        {
            Console.WriteLine("start");
            var i = 0;
            foreach (var checkBox in _checkBoxes)
            {
                if (checkBox.Checked == true)
                {
                    Util util = new Util();
                    //util.SaveToCSV(_predictMasks[i], _videoCapture,i);
                    SaveToCSV(_predictMasks[i], i);
                    Console.WriteLine(i);
                }
                i++;
            }
        }

        private void SaveJson(object sender, EventArgs e)
        {
            Console.WriteLine("start");
            var i = 0;
            foreach (var checkBox in _checkBoxes)
            {
                if (checkBox.Checked == true)
                {
                 
                    SaveToJSON(_predictMasks[i], i);
                    Console.WriteLine(i);
                }
                i++;
            }
        }

        private async void judgeDisease(object sender, EventArgs e)
        {
            Console.WriteLine("start");
            var i = 0;
            var jsons = new Dictionary<string, List<PixelSpectral>>();
            foreach (var checkBox in _checkBoxes)
            {
                if (checkBox.Checked == true)
                {
                    Util util = new Util();
                    //util.SaveToCSV(_predictMasks[i], _videoCapture,i);
                    var json  = getJSON(_predictMasks[i], i);

                    jsons.Add(checkBox.Text,json);
                    Console.WriteLine(i);
                }
                i++;
            }
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                
                WriteIndented = false
            };
            string jsonString = JsonSerializer.Serialize(jsons, options);
            string savefile = "C:/Users/wakanao/source/repos/AutoHyperSpectral/" + "test" + ".json";

            File.WriteAllText(savefile, jsonString);
            Console.WriteLine(jsonString);
            Http http = new Http();
            _diseasePredict = await http.JudgeDisease(jsonString);
            Console.WriteLine(_diseasePredict);
        }

        private List<PixelSpectral> getJSON(List<List<bool>> masks, int index)
        {
            int imgWidth = masks[0].Count;
            int imgHeight = masks.Count;
            int j = 0;
            List<PixelSpectral> pixelSpectrals = new List<PixelSpectral>();

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


                        //60band
                        int[] bands = new int[60];
                        for (int i = 0; i < 60; i++)
                        {
                            Vec3b pixel = mat.At<Vec3b>(i * interval, x);
                            bands[i] = pixel.Item0;
                        }
                        PixelSpectral pixelSpectral =
                      new PixelSpectral
                      {
                          X = x,
                          Y = y,
                          band1 = bands[0],
                          band2 = bands[1],
                          band3 = bands[2],
                          band4 = bands[3],
                          band5 = bands[4],
                          band6 = bands[5],
                          band7 = bands[6],
                          band8 = bands[7],
                          band9 = bands[8],
                          band10 = bands[9],
                          band11 = bands[10],
                          band12 = bands[11],
                          band13 = bands[12],
                          band14 = bands[13],
                          band15 = bands[14],
                          band16 = bands[15],
                          band17 = bands[16],
                          band18 = bands[17],
                          band19 = bands[18],
                          band20 = bands[19],
                          band21 = bands[20],
                          band22 = bands[21],
                          band23 = bands[22],
                          band24 = bands[23],
                          band25 = bands[24],
                          band26 = bands[25],
                          band27 = bands[26],
                          band28 = bands[27],
                          band29 = bands[28],
                          band30 = bands[29],
                          band31 = bands[30],
                          band32 = bands[31],
                          band33 = bands[32],
                          band34 = bands[33],
                          band35 = bands[34],
                          band36 = bands[35],
                          band37 = bands[36],
                          band38 = bands[37],
                          band39 = bands[38],
                          band40 = bands[39],
                          band41 = bands[40],
                          band42 = bands[41],
                          band43 = bands[42],
                          band44 = bands[43],
                          band45 = bands[44],
                          band46 = bands[45],
                          band47 = bands[46],
                          band48 = bands[47],
                          band49 = bands[48],
                          band50 = bands[49],
                          band51 = bands[50],
                          band52 = bands[51],
                          band53 = bands[52],
                          band54 = bands[53],
                          band55 = bands[54],
                          band56 = bands[55],
                          band57 = bands[56],
                          band58 = bands[57],
                          band59 = bands[58],
                          band60 = bands[59],
                      };
                        pixelSpectrals.Add(pixelSpectral);
                    }
                    l++;
                }
                j++;
            }
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                // インデントフォーマットあり
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(pixelSpectrals, options);
            return pixelSpectrals;
        }
    // 関数化

    private void CreateCheckBox(int index)
        {
            int height = 150 + (index+1) *20;
            var checkBox = new CheckBox();
            checkBox.Location = new System.Drawing.Point(30, height);
            checkBox.Checked = true;
            checkBox.Size = new System.Drawing.Size(75, 23);
            checkBox.Name = index.ToString();
            checkBox.TabIndex = 0;
            checkBox.Text = index.ToString();
            checkBox.UseVisualStyleBackColor = true;

            _checkBoxes.Add(checkBox);
            Controls.Add(checkBox);
        }
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
       
        private void SaveToCSV(List<List<bool>> masks,int index)
        {
            string savefile = "C:/Users/wakanao/source/repos/AutoHyperSpectral/" +index.ToString() + ".csv";
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

        private void SaveToJSON(List<List<bool>> masks, int index)
        {
            string savefile = "C:/Users/wakanao/source/repos/AutoHyperSpectral/" + index.ToString() + ".json";
          
            int imgWidth = masks[0].Count;
            int imgHeight = masks.Count;
            int j = 0;
            List<PixelSpectral> pixelSpectrals = new List<PixelSpectral>();

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
                          
                           
                            //60band
                            int[] bands = new int[60];
                            for (int i = 0; i < 60; i++)
                            {
                                Vec3b pixel = mat.At<Vec3b>(i * interval, x);
                                bands[i] = pixel.Item0;
                            }
                            PixelSpectral pixelSpectral =
                          new PixelSpectral
                          {
                              X = x,
                              Y = y,
                              band1 = bands[0],
                              band2 = bands[1],
                              band3 = bands[2],
                              band4 = bands[3],
                              band5 = bands[4],
                              band6 = bands[5],
                              band7 = bands[6],
                              band8 = bands[7],
                              band9 = bands[8],
                              band10 = bands[9],
                              band11 = bands[10],
                              band12 = bands[11],
                              band13 = bands[12],
                              band14 = bands[13],
                              band15 = bands[14],
                              band16 = bands[15],
                              band17 = bands[16],
                              band18 = bands[17],
                              band19 = bands[18],
                              band20 = bands[19],
                              band21 = bands[20],
                              band22 = bands[21],
                              band23 = bands[22],
                              band24 = bands[23],
                              band25 = bands[24],
                              band26 = bands[25],
                              band27 = bands[26],
                              band28 = bands[27],
                              band29 = bands[28],
                              band30 = bands[29],
                              band31 = bands[30],
                              band32 = bands[31],
                              band33 = bands[32],
                              band34 = bands[33],
                              band35 = bands[34],
                              band36 = bands[35],
                              band37 = bands[36],
                              band38 = bands[37],
                              band39 = bands[38],
                              band40 = bands[39],
                              band41 = bands[40],
                              band42 = bands[41],
                              band43 = bands[42],
                              band44 = bands[43],
                              band45 = bands[44],
                              band46 = bands[45],
                              band47 = bands[46],
                              band48 = bands[47],
                              band49 = bands[48],
                              band50 = bands[49],
                              band51 = bands[50],
                              band52 = bands[51],
                              band53 = bands[52],
                              band54 = bands[53],
                              band55 = bands[54],
                              band56 = bands[55],
                              band57 = bands[56],
                              band58 = bands[57],
                              band59 = bands[58],
                              band60 = bands[59],
                          };
                            pixelSpectrals.Add(pixelSpectral);
                        }
                        l++;
                    }
                    j++;
                }
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                // インデントフォーマットあり
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(pixelSpectrals,options);
            File.WriteAllText(savefile, jsonString);
        }
    }
}
