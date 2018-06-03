using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPaint.Component
{
    public class Line : BaseObject
    {
        public int EndPositionX { get; set; }
        public int EndPositionY { get; set; }

        public override string Render()
        {
            var fill = string.Empty;
            var borderColor = string.Empty;
            var borderWidth = string.Empty;
            var isBorderDashed = false;

            if (Effects != null)
            {
                fill = base.Effects.Any(x => x.EffectType == VirtualEffectTypes.Fill) ? base.Effects.First(x => x.EffectType == VirtualEffectTypes.Fill).Value : string.Empty;
                borderColor = base.Effects.Any(x => x.EffectType == VirtualEffectTypes.BorderColor) ? base.Effects.First(x => x.EffectType == VirtualEffectTypes.BorderColor).Value : string.Empty;
                borderWidth = base.Effects.Any(x => x.EffectType == VirtualEffectTypes.BorderWidth) ? base.Effects.First(x => x.EffectType == VirtualEffectTypes.BorderWidth).Value : string.Empty;
                isBorderDashed = base.Effects.Any(x => x.EffectType == VirtualEffectTypes.Dashed);

                if (Effects.Any(x => x.EffectType == VirtualEffectTypes.Expand))
                {
                    var size = Effects.FirstOrDefault(x => x.EffectType == VirtualEffectTypes.Expand).Value;
                    this.Expand(Convert.ToInt32(size));
                }
            }



            base.Render();

            return string.Format("addRect({0}, {1}, {2}, {3}, '{4}' ,'Line','{5}','{6}'" + "," + "'{7}',{8},{9});", this.PositionX, this.PositionY, 0, 0, fill, borderWidth, borderColor, isBorderDashed.ToString().ToLower(), this.EndPositionX, this.EndPositionY);
        }

        public override void Expand(int size)
        {


        }

        private int CalculateDistance(int x1, int y1, int x2, int y2)
        {
            var result = Math.Ceiling(Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)));

            return (int)result;
        }
    }
}
