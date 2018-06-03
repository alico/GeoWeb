using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GeoPaint.Engine;
using GeoPaint.Entities;

namespace GeoPaint.DummyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //var engine = new ShapeEngine();
            //var complex = new ComplexShape();

            //complex.Shapes = new List<BaseObjectDTO>();
            //var effect = new ComplexShapeEffect();
            //effect.Id = 80;
            //complex.Effects.Add(effect);

            //var point = new PointDTO();
            //point.Id = 1;
            //point.PositionX = 10;
            //point.PositionY = 20;
            //point.Effect.Id = 23;


            //var square = new SquareDTO();
            //square.Id = 1;
            //square.PositionX = 10;
            //square.PositionY = 20;
            //square.Effect.Id = 25;
            //square.Width = 90;

            //complex.Shapes.Add(point);
            //complex.Shapes.Add(square);


            //engine.CreateComplexShape(complex);


            //var obj = engine.GetComplexShape();


            var json = "{\"Shapes\":[{\"Id\":1,\"PositionX\":10,\"PositionY\":20,\"Effect\":{\"Id\":23}},{\"Width\":90,\"Id\":1,\"PositionX\":10,\"PositionY\":20,\"Effect\":{\"Id\":25}}],\"Effects\":[{\"Id\":80}]}";

            var shape = new ComplexShape();
            shape.Content = json;
            shape.Name = "Ali test";
            //var repo = RepositoryFactory.GetRepository<ComplexShape>();

            //repo.Insert(shape);




        }
    }
}
