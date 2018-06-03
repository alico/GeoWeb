using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPaint.Component
{
    public class Triangle : BaseObject
    {
        public int SecondCornerX { get; set; }
        public int SecondCornerY { get; set; }
        public int ThirdCornerX { get; set; }
        public int ThirdCornerY { get; set; }

        public override string Render()
        {
            return string.Format("{0} nolu obje Üçgen ilk köşesi ({1},{2}) konumunda ikinci köşesi ({3},{4}) üçüncü köşesi ({5},{6}) ve  konumunda.", this.Id, this.PositionX, this.PositionY,this.SecondCornerX,this.SecondCornerY,this.ThirdCornerX,this.ThirdCornerY);
        }
    }
}
