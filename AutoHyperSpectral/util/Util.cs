﻿using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using AutoHyperSpectral;

namespace AutoHyperSpectral.util
{
    internal class Util
    {
     
        private void SaveToCSV(List<List<bool>> masks, VideoCapture videoCapture,int index)
        {
            Form1 form1 = new Form1();
            var progressBar = form1.toolStripProgressBar1;

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

                progressBar.Visible = true;
                progressBar.Minimum = 1;
                progressBar.Maximum = imgHeight;
                progressBar.Value = 1;
                progressBar.Step = 1;

                for (int y = 0; y < imgHeight; y++)
                {
                    //画像を生成
                    videoCapture.PosFrames = y;
                    var mat = new Mat();
                    videoCapture.Read(mat);

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
                    progressBar.PerformStep();
                }
            }
        }
    }
}
