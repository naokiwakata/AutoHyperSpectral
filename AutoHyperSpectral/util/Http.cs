using AutoHyperSpectral.domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoHyperSpectral.util
{
    internal class Http
    {
        private LeafPredict _leafPredict;
        private DiseasePredict _diseasePredict;
        public async Task<LeafPredict> PredictImage(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Jpeg);
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
                client.Timeout = TimeSpan.FromMinutes(3); //wait for 3 minutes
                HttpResponseMessage response =
                    await client.PostAsync($"http://127.0.0.1:5000/findHyperLeaf", content);

                if (response.IsSuccessStatusCode)
                {
                    //レスポンスからJSON文字列を取得
                    string contentStream = await response.Content.ReadAsStringAsync();

                    _leafPredict = JsonSerializer.Deserialize<LeafPredict>(contentStream);
                }
                else
                {
                    throw new Exception("failed to communicate");
                }
            }
            return _leafPredict;

        }
    
    public async Task<DiseasePredict> JudgeDisease(String spectralJson)
    {

        var parameters = new Dictionary<string, string>()
            {
                { "spectrals", spectralJson},
            };

        using (var client = new HttpClient())
        {
            string jsonString = JsonSerializer.Serialize(parameters);
            var content = new StringContent(spectralJson, Encoding.UTF8, "application/json");
            client.Timeout = TimeSpan.FromMinutes(3); //wait for 3 minutes
            HttpResponseMessage response =
                await client.PostAsync($"http://127.0.0.1:5000/judgeDisease", content);

            if (response.IsSuccessStatusCode)
            {
                //レスポンスからJSON文字列を取得
                string contentStream = await response.Content.ReadAsStringAsync();
                Console.WriteLine(contentStream);

                _diseasePredict = JsonSerializer.Deserialize<DiseasePredict>(contentStream);                   
                }
            else
            {
                throw new Exception("failed to communicate");
            }
        }
            return _diseasePredict;

    }
}
}
