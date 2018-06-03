using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPaint.Component
{
    public class Point : BaseObject
    {
        public override string Render()
        {
            var effect = base.RenderEffects();

            var shape = string.Format("{0} nolu obje nokta ({1},{2}) konumunda.", this.Id, this.PositionX, this.PositionY);
            
            return shape + effect;
        }
    }
}
