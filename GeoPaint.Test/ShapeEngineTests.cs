using System;
using System.ComponentModel.Composition;
using System.Linq;
using GeoPaint.Component;
using GeoPaint.Data.Contracts;
using GeoPaint.DTO;
using GeoPaint.Engine;
using GeoPaint.Engine.Contracts;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace GeoPaint.Test
{
    [TestFixture]
    [Category("UnitTests.ShapeEngineTests")]
    public class ShapeEngineTests
    {
        [Import]
        IRepositoryFactory _mockDataRepositoryFactory;

        [Import]
        IBusinessEngineFactory _mockBusinessEngineFactory;

        [SetUp]
        public void SetUp()
        {
            _mockDataRepositoryFactory = Substitute.For<IRepositoryFactory>();

            _mockBusinessEngineFactory = Substitute.For<IBusinessEngineFactory>();

        }

        [TearDown]
        public void TearDown()
        {

        }

        public ShapeEngineTests()
        {

        }

        [Test]
        public void Get_HappyPath_NotNull()
        {

            var complaxeShape = new Entities.ComplexShape()
            {
                Id = 1,
                Name = "Test",
                Content = "{\"$type\":\"GeoPaint.Component.ComplexShape, GeoWeb.DTO\",\"Shapes\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.BaseObject, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.Line, GeoWeb.DTO\",\"EndPositionX\":50,\"EndPositionY\":30,\"Id\":0,\"PositionX\":0,\"PositionY\":0,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"5\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"blue\"}]}},{\"$type\":\"GeoPaint.Component.Square, GeoWeb.DTO\",\"Width\":30,\"Id\":1,\"PositionX\":80,\"PositionY\":80,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"2\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"black\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":2,\"Value\":\"red\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":4,\"Value\":\"false\"}]}},{\"$type\":\"GeoPaint.Component.Circle, GeoWeb.DTO\",\"Radius\":30,\"Id\":2,\"PositionX\":10,\"PositionY\":20,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"2\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"black\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":2,\"Value\":\"yellow\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":4,\"Value\":\"true\"}]}}]},\"Id\":0,\"Name\":\"Tool\",\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ComplexShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[]}}",
                EntityId = 2
            };

            _mockDataRepositoryFactory.GetRepository<Entities.ComplexShape>().Get(Arg.Any<int>()).ReturnsForAnyArgs(complaxeShape);
            var engine = new ShapeEngine(_mockDataRepositoryFactory, _mockBusinessEngineFactory);

            var result = engine.Get(1);

            //_mockDataRepositoryFactory.GetRepository<Entities.ComplexShape>().Get(Arg.Any<int>()).Received();

            Assert.AreEqual(complaxeShape.Id, result.Id);
            Assert.AreEqual(complaxeShape.Name, result.Name);

        }


        [TestCase(null, TestName = "GetComplexShape_NullValue_ReturnsArgumentNullException")]
        [TestCase(0, TestName = "GetComplexShape_ZeroValue_ReturnsArgumentNullException")]

        public void GetComplexShape_NullOrZeroValue_ReturnsArgumentNullException(int shapeId)
        {

            var complaxeShape = new Entities.ComplexShape();

            complaxeShape.Id = shapeId;

            _mockDataRepositoryFactory.GetRepository<Entities.ComplexShape>().Get(Arg.Any<int>()).ReturnsForAnyArgs(complaxeShape);

            var engine = new ShapeEngine(_mockDataRepositoryFactory, _mockBusinessEngineFactory);

            Assert.That(() => engine.Get(Arg.Any<int>()), Throws.ArgumentNullException);

        }

        [Test]
        public void CreateComplexShape_Null_ReturnsArgumentNullException()
        {
            var complaxeShape = new CreateComplexShapeRequestDTO()
            {
                Name = "",
                Shapes = new System.Collections.Generic.List<CreateShapeRequestDTO>()
                {
                   new CreateShapeRequestDTO()
                }
            };

            var engine = new ShapeEngine(_mockDataRepositoryFactory, _mockBusinessEngineFactory);

            Assert.That(() => engine.Create(complaxeShape), Throws.ArgumentNullException);

        }

        [Test]
        public void CreateComplexShape_HappyPathWithoutEffect_NotNull()
        {
            int result = 0;
            var complaxeShape = new CreateComplexShapeRequestDTO()
            {
                Name = "Simple Image",
                Shapes = new System.Collections.Generic.List<CreateShapeRequestDTO>()
                {
                   new CreateShapeRequestDTO()
                   {
                   Type="Square",
                   StartPositionX = 1,
                   StartPositionY = 2,
                   Width = 5,
                   }
                }
            };

            var entity = new Entities.ComplexShape()
            {
                Id = 1,
                Name = complaxeShape.Name,
                Content = "{\"$type\":\"GeoPaint.Component.ComplexShape, GeoWeb.DTO\",\"Shapes\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.BaseObject, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.Line, GeoWeb.DTO\",\"EndPositionX\":50,\"EndPositionY\":30,\"Id\":0,\"PositionX\":0,\"PositionY\":0,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"5\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"blue\"}]}},{\"$type\":\"GeoPaint.Component.Square, GeoWeb.DTO\",\"Width\":30,\"Id\":1,\"PositionX\":80,\"PositionY\":80,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"2\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"black\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":2,\"Value\":\"red\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":4,\"Value\":\"false\"}]}},{\"$type\":\"GeoPaint.Component.Circle, GeoWeb.DTO\",\"Radius\":30,\"Id\":2,\"PositionX\":10,\"PositionY\":20,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"2\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"black\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":2,\"Value\":\"yellow\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":4,\"Value\":\"true\"}]}}]},\"Id\":0,\"Name\":\"Tool\",\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ComplexShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[]}}",

            };
            _mockDataRepositoryFactory.GetRepository<Entities.ComplexShape>().Insert(entity);

            var engine = new ShapeEngine(_mockDataRepositoryFactory, _mockBusinessEngineFactory);

            result = engine.Create(complaxeShape);

            Assert.AreEqual(complaxeShape.Name, entity.Name);

        }

        [Test]
        public void CreateComplexShape_HappyPathWithEffect_NotNull()
        {
            int result = 0;
            var complaxeShape = new CreateComplexShapeRequestDTO()
            {
                Name = "Simple Image",
                Shapes = new System.Collections.Generic.List<CreateShapeRequestDTO>()
                {
                 new CreateShapeRequestDTO()
                {
                    Type = "Square",
                    StartPositionX = 80,
                    StartPositionY = 80,
                    Width = 5,
                    Effects = new System.Collections.Generic.List<Component.ShapeEffect>()
                           {
                               new Component.ShapeEffect()
                               {
                                   Value="2",
                                   EffectType = VirtualEffectTypes.BorderWidth
                               }
                           }
                }
                }

            };


            var entity = new Entities.ComplexShape()
            {
                Id = 1,
                Name = complaxeShape.Name,
                Content = "{\"$type\":\"GeoPaint.Component.ComplexShape, GeoWeb.DTO\",\"Shapes\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.BaseObject, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.Line, GeoWeb.DTO\",\"EndPositionX\":50,\"EndPositionY\":30,\"Id\":0,\"PositionX\":0,\"PositionY\":0,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"5\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"blue\"}]}},{\"$type\":\"GeoPaint.Component.Square, GeoWeb.DTO\",\"Width\":30,\"Id\":1,\"PositionX\":80,\"PositionY\":80,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"2\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"black\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":2,\"Value\":\"red\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":4,\"Value\":\"false\"}]}},{\"$type\":\"GeoPaint.Component.Circle, GeoWeb.DTO\",\"Radius\":30,\"Id\":2,\"PositionX\":10,\"PositionY\":20,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"2\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"black\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":2,\"Value\":\"yellow\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":4,\"Value\":\"true\"}]}}]},\"Id\":0,\"Name\":\"Tool\",\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ComplexShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[]}}",

            };
            _mockDataRepositoryFactory.GetRepository<Entities.ComplexShape>().Insert(entity);

            var engine = new ShapeEngine(_mockDataRepositoryFactory, _mockBusinessEngineFactory);

            result = engine.Create(complaxeShape);

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var deserializedComplexShape = JsonConvert.DeserializeObject<ComplexShape>(entity.Content, settings);

            Assert.AreEqual(complaxeShape.Name, entity.Name);
            Assert.AreEqual(complaxeShape.Shapes.First().StartPositionX, deserializedComplexShape.Shapes[1].PositionX);
            Assert.AreEqual(complaxeShape.Shapes.First().StartPositionY, deserializedComplexShape.Shapes[1].PositionY);
            Assert.AreEqual(complaxeShape.Shapes.First().Effects.First().EffectType, deserializedComplexShape.Shapes[1].Effects.First().EffectType);
            Assert.AreEqual(complaxeShape.Shapes.First().Effects.First().Value, deserializedComplexShape.Shapes[1].Effects.First().Value);
        }

        [Test]
        public void UpdateComplexShape_HappyPathWithEffect_NotNull()
        {
            int result = 0;
            var complaxeShape = new CreateComplexShapeRequestDTO()
            {
                Name = "Simple Image",
                Shapes = new System.Collections.Generic.List<CreateShapeRequestDTO>()
                {
                 new CreateShapeRequestDTO()
                {
                    Type = "Square",
                    StartPositionX = 80,
                    StartPositionY = 80,
                    Width = 5,
                    Effects = new System.Collections.Generic.List<Component.ShapeEffect>()
                           {
                               new Component.ShapeEffect()
                               {
                                   Value="2",
                                   EffectType = VirtualEffectTypes.BorderWidth
                               }
                           }
                }
                }

            };


            var entity = new Entities.ComplexShape()
            {
                Id = 1,
                Name = complaxeShape.Name,
                Content = "{\"$type\":\"GeoPaint.Component.ComplexShape, GeoWeb.DTO\",\"Shapes\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.BaseObject, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.Line, GeoWeb.DTO\",\"EndPositionX\":50,\"EndPositionY\":30,\"Id\":0,\"PositionX\":0,\"PositionY\":0,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"5\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"blue\"}]}},{\"$type\":\"GeoPaint.Component.Square, GeoWeb.DTO\",\"Width\":30,\"Id\":1,\"PositionX\":80,\"PositionY\":80,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"2\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"black\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":2,\"Value\":\"red\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":4,\"Value\":\"false\"}]}},{\"$type\":\"GeoPaint.Component.Circle, GeoWeb.DTO\",\"Radius\":30,\"Id\":2,\"PositionX\":10,\"PositionY\":20,\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":1,\"Value\":\"2\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":3,\"Value\":\"black\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":2,\"Value\":\"yellow\"},{\"$type\":\"GeoPaint.Component.ShapeEffect, GeoWeb.DTO\",\"EffectType\":4,\"Value\":\"true\"}]}}]},\"Id\":0,\"Name\":\"Tool\",\"Effects\":{\"$type\":\"System.Collections.Generic.List`1[[GeoPaint.Component.ComplexShapeEffect, GeoWeb.DTO]], mscorlib\",\"$values\":[]}}",

            };
            _mockDataRepositoryFactory.GetRepository<Entities.ComplexShape>().Insert(entity);

            var engine = new ShapeEngine(_mockDataRepositoryFactory, _mockBusinessEngineFactory);

            result = engine.Create(complaxeShape);

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var deserializedComplexShape = JsonConvert.DeserializeObject<ComplexShape>(entity.Content, settings);

            Assert.AreEqual(complaxeShape.Name, entity.Name);
            Assert.AreEqual(complaxeShape.Shapes.First().StartPositionX, deserializedComplexShape.Shapes[1].PositionX);
            Assert.AreEqual(complaxeShape.Shapes.First().StartPositionY, deserializedComplexShape.Shapes[1].PositionY);
            Assert.AreEqual(complaxeShape.Shapes.First().Effects.First().EffectType, deserializedComplexShape.Shapes[1].Effects.First().EffectType);
            Assert.AreEqual(complaxeShape.Shapes.First().Effects.First().Value, deserializedComplexShape.Shapes[1].Effects.First().Value);
        }


    }
}
