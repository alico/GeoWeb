using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Component;

namespace GeoPaint.DTO
{
    public class CreateShapeRequestDTO
    {
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }
        public int EndPositionX { get; set; }
        public int EndPositionY { get; set; }
        public int Width { get; set; }
        public string Type { get; set; }
        public List<ShapeEffect> Effects { get; set; }
    }
}
