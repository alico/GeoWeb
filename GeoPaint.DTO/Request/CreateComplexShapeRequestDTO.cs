using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Component;

namespace GeoPaint.DTO
{
    public class CreateComplexShapeRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ComplexShapeEffect> Effects { get; set; }
        public List<CreateShapeRequestDTO> Shapes { get; set; }
    }
}
