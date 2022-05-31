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
        private Predict _predict;
        public async Task<Predict> PredictImage(Bitmap bitmap)
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
                HttpResponseMessage response =
                    await client.PostAsync($"http://127.0.0.1:5000/findHyperLeaf", content);

                if (response.IsSuccessStatusCode)
                {
                    //レスポンスからJSON文字列を取得
                    string contentStream = await response.Content.ReadAsStringAsync();

                    _predict = JsonSerializer.Deserialize<Predict>(contentStream);
                }
                else
                {
                    throw new Exception("failed to communicate");
                }
            }
            return _predict;

        }
    }
}
