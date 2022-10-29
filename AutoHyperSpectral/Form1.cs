using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using ExtensionMethods;
using AutoHyperSpectral.util;
using System.IO;
using System.Text.Json;
using AutoHyperSpectral.domain;
using System.Linq;
using Point = OpenCvSharp.Point;

namespace AutoHyperSpectral
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        // 読み込むもの
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

            Dialog dialog = new Dialog();
            _filename = dialog.openDialog();


            if (_filename == "") return;

            // 動画を選んだら処理開始
            clearState();

            _videoCapture = new VideoCapture(_filename);
            _bitmap = _videoCapture.ToBmp(toolStripProgressBar1);
            pictureBox1.Image = _bitmap;
            pictureBox2.Image = _bitmap;
            detectLeafButton.Enabled = true;


        }
        private async void PostImage(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = 4;

            _videoCapture = new VideoCapture(_filename);
            _bitmap = _videoCapture.ToBmp();

            textBox1.Text = "...Loading...";

            if (_bitmap == null)
            {
                return;
            }

            toolStripProgressBar1.Value = 2;

            // connect to FLASK
            Http http = new Http();
            _leafPredict = await http.PredictImage(_bitmap);

            toolStripProgressBar1.Value = 3;

            _predictBoxes = _leafPredict.Boxes;
            _predictMasks = _leafPredict.Masks;
            _predictClasses = _leafPredict.Classes;

            Console.WriteLine("SUCEESS");

            // show check box & bounding box
            int boxesCount = _leafPredict.Boxes.Count;
            for (int i = 0; i < boxesCount; i++)
            {
                CreateCheckBox(i);
                DrawBox(i);
            }

            toolStripProgressBar1.Value = 4;

            // show image
            pictureBox1.Image = _bitmap;

            detectLeafButton.Enabled = true;
            selectLeafButton.Enabled = true;
            textBox1.Text = "Finish!!!";
            toolStripProgressBar1.Value = 0;
        }

        private void SelectLeaf(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = 2;
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
            saveCsvButton.Enabled = true;
            saveJsonButton.Enabled = true;
            judgeDiseaseButton.Enabled = true;
            toolStripProgressBar1.Value = 0;

        }
        private void SaveCSV(object sender, EventArgs e)
        {
            Console.WriteLine("start");
            var i = 0;
            toolStripProgressBar1.Value = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = _checkBoxes.Where(o => o.Checked == true).Count() + 2;

            foreach (var checkBox in _checkBoxes)
            {
                if (checkBox.Checked == true)
                {
                    Util util = new Util();
                    //util.SaveToCSV(_predictMasks[i], _videoCapture,i);
                    SaveToCSV(_predictMasks[i], i);
                    Console.WriteLine(i);
                    toolStripProgressBar1.Value++;

                }
                i++;
            }
            toolStripProgressBar1.Value = 0;
        }

        private void SaveJson(object sender, EventArgs e)
        {
            Console.WriteLine("start");
            var i = 0;

            var a = _checkBoxes.Where(o => o.Checked == true).Count();
            toolStripProgressBar1.Value = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = _checkBoxes.Where(o => o.Checked == true).Count() + 2;
            foreach (var checkBox in _checkBoxes)
            {
                if (checkBox.Checked == true)
                {

                    SaveToJSON(_predictMasks[i], i);
                    Console.WriteLine(i);
                    toolStripProgressBar1.Value++;

                }
                i++;

            }
            toolStripProgressBar1.Value = 0;


        }

        private async void judgeDisease(object sender, EventArgs e)
        {
            _videoCapture = new VideoCapture(_filename);
            _bitmap = _videoCapture.ToBmp(toolStripProgressBar1);

            Console.WriteLine("start");
            var i = 0;
            var jsons = new Dictionary<string, List<PixelSpectral>>();
            foreach (var checkBox in _checkBoxes)
            {
                if (checkBox.Checked == true)
                {
                    Util util = new Util();
                    //util.SaveToCSV(_predictMasks[i], _videoCapture,i);
                    var json = getJSON(_predictMasks[i], i);

                    jsons.Add(checkBox.Text, json);
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

            var l = 0;
            foreach (var checkBox in _checkBoxes)
            {
                if (checkBox.Checked == true)
                {

                    DrawJudgeResult(l);
                }
                l++;
            }
            pictureBox1.Image = _bitmap;
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

        private void saveHyperImage(object sender, EventArgs e)
        {
            if (_bitmap != null)
            {

                var saveFileName = _filename.Split('.')[0] + "_hyper" + ".jpg";

                _bitmap.Save(
                   saveFileName,
                   System.Drawing.Imaging.ImageFormat.Jpeg
                );
            }
        }
        // 関数化

        private void CreateCheckBox(int index)
        {
            int height = 90 + (index + 1) * 20;
            var checkBox = new CheckBox();
            checkBox.Location = new System.Drawing.Point(120, height);
            checkBox.Checked = true;
            checkBox.Size = new System.Drawing.Size(75, 23);
            checkBox.Name = index.ToString();
            checkBox.TabIndex = 0;
            checkBox.Text = index.ToString();
            checkBox.UseVisualStyleBackColor = true;

            _checkBoxes.Add(checkBox);
            panel1.Controls.Add(checkBox);
        }
        private void DrawBox(int i)
        {
            List<float> boxes = _predictBoxes[i];
            List<List<bool>> masks = _predictMasks[i];
            int classes = _predictClasses[i];

            Graphics graphics = CreateGraphics();
            graphics.DrawBox(_bitmap, boxes, i);
            graphics.FillLeaf(_bitmap, masks);

            graphics.Dispose();
        }
        private void DrawJudgeResult(int i)
        {
            List<float> boxes = _predictBoxes[i];
            List<List<bool>> masks = _predictMasks[i];
            int classes = _predictClasses[i];

            Graphics graphics = CreateGraphics();
            graphics.DrawJudgeResult(_bitmap, boxes, i, _diseasePredict);

            graphics.Dispose();
        }
        private void clearState()
        {
            deactivateButton();

            // clear checkbox 
            foreach (var checkBox in _checkBoxes)
            {
                Controls.Remove(checkBox);
            }
            _checkBoxes.Clear();

        }
        private void deactivateButton()
        {
            detectLeafButton.Enabled = false;
            selectLeafButton.Enabled = false;
            saveCsvButton.Enabled = false;
            saveJsonButton.Enabled = false;
            judgeDiseaseButton.Enabled = false;

        }
        private void SaveToCSV(List<List<bool>> masks, int index)
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
            string jsonString = JsonSerializer.Serialize(pixelSpectrals, options);
            File.WriteAllText(savefile, jsonString);
        }


        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        Mat _maskedMat;
        Rect _rect;

        private void stackMaskButton_Click(object sender, EventArgs e)
        {
            Dialog dialog = new Dialog();

            var jsonFile = dialog.openJsonFile();


            if (jsonFile == "") return;

            using (StreamReader file = File.OpenText(jsonFile))
            {
                var trim
                    = JsonSerializer.Deserialize<Trim>(file.ReadToEnd());

                // マスク画像を作成する
                // 黒の下地の画像を作成
                _maskedMat = new Mat(trim.imageHeight, trim.imageWidth, MatType.CV_8U, new Scalar(1));
                _maskedMat.SetTo(Scalar.Black);

                // 領域を白でくり抜く
                List<Point> points = trim.ToPoints();
                List<List<Point>> ListOfListOfPoints = new List<List<Point>>();
                ListOfListOfPoints.Add(points);
                Cv2.FillPoly(_maskedMat, ListOfListOfPoints, Scalar.White);

                // 領域を見つけ出す。BoundingRect
                _rect = Cv2.BoundingRect(_maskedMat);
                Cv2.Rectangle(_maskedMat, _rect, Scalar.White);

                // マスク画像の重ね合わせ
                Graphics graphics = CreateGraphics();
                graphics.FillLeafByMaskImage(_bitmap, _maskedMat);
                pictureBox1.Image = _bitmap;
            }
            createCsvButton.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // 領域の左上と右下の座標からfor文を回す
            int x1 = _rect.X;
            int y1 = _rect.Y;
            int width = _rect.Width;
            int height = _rect.Height;

            // CSVファイルを保存する
            var saveFile = "D:/wakata_research/test.csv";
            using (StreamWriter streamWriter = new StreamWriter(saveFile, false))
            {
                //行名を書く
                String lineName = "X,Y,";
                for (int i = 0; i < 60; i++)
                {
                    lineName = $"{lineName}" + $"band{i + 1},";

                }
                streamWriter.WriteLine(lineName);

                // 領域についてfor文を回す
                for (int y = y1; y < y1 + height; y++)
                {
                    Console.WriteLine(y);
                    //画像を生成
                    _videoCapture.PosFrames = y;
                    var mat = new Mat();
                    _videoCapture.Read(mat);


                    int interval = mat.Height / 60;
                    for (int x = x1; x < x1 + width; x++)
                    {
                        // マスク画像の白黒で判別し波長情報を抜き出す
                        var isWhite = _maskedMat.At<Vec3b>(y, x).Item1 == 255;
                        if (isWhite)
                        {
                            // 60バンドの波長情報書き込む
                            String bandStr = $"{x},{y},";
                            for (int i = 0; i < 60; i++)
                            {
                                Vec3b pixel = mat.At<Vec3b>(i * interval, x);
                                bandStr = bandStr + pixel.Item0 + ",";
                            }
                            streamWriter.WriteLine(bandStr);
                        }

                    }

                }

            }
        }
    }


}
