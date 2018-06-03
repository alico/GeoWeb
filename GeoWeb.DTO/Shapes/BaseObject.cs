using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPaint.Component
{
    public abstract class BaseObject
    {
        public int Id { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public List<ShapeEffect> Effects { get; set; }

        public virtual string Render()
        {
            if (Effects != null)
                if (Effects.Any(x => x.EffectType == VirtualEffectTypes.Move))
                {
                    var point = Effects.FirstOrDefault(x => x.EffectType == VirtualEffectTypes.Move).Value.Split(',');
                    this.Move(Convert.ToInt32(point[0]), Convert.ToInt32(point[1]));
                }

            return string.Empty;
        }

        public virtual string RenderEffects()
        {
            var effect = string.Empty;
            foreach (var effectItem in Effects)
            {
                switch (effectItem.EffectType)
                {
                    case VirtualEffectTypes.None:
                        break;
                    case VirtualEffectTypes.BorderWidth:

                    case VirtualEffectTypes.Fill:

                    case VirtualEffectTypes.BorderColor:
                        effect += string.Format("{0} nolu objeye {1} efekti {2} değeri ile uygulandı", this.Id, effectItem.EffectType, effectItem.Value);
                        break;
                    default:
                        break;
                }
            }
            return effect;
        }

        public virtual void Move(int positionX, int positionY)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public virtual void Expand(int size)
        {

        }

        public BaseObject()
        {
            Effects = new List<ShapeEffect>();
        }
    }
}
