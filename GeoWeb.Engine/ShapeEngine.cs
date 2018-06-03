using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Data.Contracts;
using GeoPaint.Component;
using GeoPaint.Engine.Contracts;
using GeoWeb.Engine;
using Newtonsoft.Json;
using GeoPaint.DTO;

namespace GeoPaint.Engine
{
    [Export(typeof(IShapeEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ShapeEngine : BusinessEngineBase, IShapeEngine
    {
        [Import]
        IRepositoryFactory _RepositoryFactory;

        [Import]
        IBusinessEngineFactory _EngineFactory;

        public ShapeEngine()
        {
        }

        public ShapeEngine(IRepositoryFactory repositoryFactory, IBusinessEngineFactory engineFactory)
        {
            _RepositoryFactory = repositoryFactory;
            _EngineFactory = engineFactory;
        }

        /// <summary>
        /// Creates an image which contains shape(s)
        /// </summary>
        /// <param name="complexShapeDto">Image data. Holds shape data and effects</param>
        /// <returns>Last record Id</returns>
        public int Create(CreateComplexShapeRequestDTO complexShapeDto)
        {
            if (complexShapeDto != null)
            {
                var image = ConvertDTOToComponent(complexShapeDto);

                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                var json = JsonConvert.SerializeObject(image, settings);

                if (json != null && !string.IsNullOrWhiteSpace(image.Name))
                {
                    var repository = _RepositoryFactory.GetRepository<GeoPaint.Entities.ComplexShape>();
                    var entity = new Entities.ComplexShape();
                    entity.Name = image.Name;
                    entity.Content = json;

                    repository.Insert(entity);

                    return entity.Id;
                }
                else
                {
                    throw new ArgumentNullException();
                }

            }
            else
            {
                throw new ArgumentNullException();
            }


        }


        /// <summary>
        /// Gets image information
        /// </summary>
        /// <param name="complexShapeId">Image Id</param>
        /// <returns>Image</returns>
        public ComplexShape Get(int complexShapeId)
        {
            if (complexShapeId != 0)
            {
                var repository = _RepositoryFactory.GetRepository<GeoPaint.Entities.ComplexShape>();
                var entity = repository.Get(complexShapeId);

                if (entity != null)
                {
                    var json = entity.Content;
                    JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                    var complexShape = JsonConvert.DeserializeObject<ComplexShape>(json, settings);

                    complexShape.Id = entity.Id;
                    complexShape.Name = entity.Name;

                    return complexShape;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

        }

        /// <summary>
        /// Updates image data
        /// </summary>
        /// <param name="image">Component</param>
        /// <returns>Last updated record Id</returns>
        public int Update(ComplexShape image)
        {
            if (image.Id > 0 && !string.IsNullOrWhiteSpace(image.Name))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                var json = JsonConvert.SerializeObject(image, settings);

                var repository = _RepositoryFactory.GetRepository<GeoPaint.Entities.ComplexShape>();
                var entity = repository.Get(image.Id);
                entity.Content = json;

                repository.Update(entity);
                return entity.Id;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Gets image list
        /// </summary>
        /// <returns>List of image</returns>
        public List<GetListResponseDTO> GetList()
        {
            var repository = _RepositoryFactory.GetRepository<GeoPaint.Entities.ComplexShape>();
            var entities = repository.GetList();
            var complexShapes = new List<GetListResponseDTO>();

            foreach (var entity in entities)
            {
                complexShapes.Add(new GetListResponseDTO()
                {
                    Id = entity.Id,
                    Name = entity.Name
                });
            }

            return complexShapes;
        }


        /// <summary>
        /// Converts image data transfer object to an entity
        /// </summary>
        /// <returns>Entity object</returns>
        private ComplexShape ConvertDTOToComponent(CreateComplexShapeRequestDTO complexShapeDto)
        {
            var complexShape = new ComplexShape();
            complexShape.Id = complexShapeDto.Id;
            complexShape.Name = complexShapeDto.Name;

            if (complexShape.Effects != null)
            {
                foreach (var effect in complexShapeDto.Effects)
                {
                    complexShape.Effects.Add(effect);
                }
            }

            foreach (var shape in complexShapeDto.Shapes)
            {
                BaseObject component = null;

                switch (shape.Type)
                {
                    case "Square":
                        {
                            component = new Square()
                            {
                                Effects = shape.Effects,
                                PositionX = shape.StartPositionX,
                                PositionY = shape.StartPositionY,
                                Width = shape.Width
                            };
                            break;
                        }
                    case "Circle":
                        {
                            component = new Circle()
                            {
                                Effects = shape.Effects,
                                PositionX = shape.StartPositionX,
                                PositionY = shape.StartPositionY,
                                Radius = shape.Width
                            };
                            break;
                        }
                    case "Line":
                        {
                            component = new Line()
                            {
                                Effects = shape.Effects,
                                PositionX = shape.StartPositionX,
                                PositionY = shape.StartPositionY,
                                EndPositionX = shape.EndPositionX,
                                EndPositionY = shape.EndPositionY
                            };
                            break;
                        }

                    default:
                        break;
                }
                if (component != null)
                    complexShape.AddShape(component);
            }

            return complexShape;

        }
    }
}
