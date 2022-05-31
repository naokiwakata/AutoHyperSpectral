using System.Collections.Generic;

namespace AutoHyperSpectral
{
    public class Predict
    {
        public List<List<float>> Boxes { get; set; }
        public List<List<List<bool>>> Masks { get; set; }
        public List<int> Classes { get; set; }
    }
}
