using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Component;

namespace GeoWeb.Component
{
    public abstract class ComplexShapeBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<BaseObject> Shapes;

        public List<ComplexShapeEffect> Effects { get; set; }

        public ComplexShapeBase()
        {
            Shapes = new List<BaseObject>();
            Effects = new List<ComplexShapeEffect>();
        }

        public virtual string Render()
        {
            var effectContent = RenderImageEffects();
            var imageContent = string.Empty;
            foreach (var shape in Shapes)
            {
                imageContent += shape.Render();

            }
            return imageContent + effectContent;
        }

        private string RenderImageEffects()
        {
            string effectContent = string.Empty;
            foreach (var effect in this.Effects)
            {
                if (effect.EffectType == ComlexShapeEffectTypes.BackgroundColor)
                    effectContent += " $('#canvas2').css('background-color', '" + effect.Value + "');";
            }
            return effectContent;
        }

        public virtual void AddShape(BaseObject shape)
        {
            var index = Shapes.Count;

            shape.Id = index;
            Shapes.Add(shape);
        }
    }
}
