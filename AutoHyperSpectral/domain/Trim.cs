using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHyperSpectral.domain
{
    public class Trim
    {
        public List<Shape> shapes { get; set; }
        public string imagePath { get; set; }
        public string imageData { get; set; }
        public int imageHeight { get; set; }
        public int imageWidth { get; set; }

    }

    public class Shape
    {
        public string label { get; set; }
        public List<List<float>> points { get; set; }
        public string shape_type { get; set; }
    }
}
