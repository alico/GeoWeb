using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Component;
using GeoPaint.DTO;

namespace GeoPaint.Engine.Contracts
{
    public interface IShapeEngine : IBusinessEngine
    {
        int Create(CreateComplexShapeRequestDTO complexShapeDto);
        ComplexShape Get(int ComplexShapeId);
        int Update(ComplexShape image);
        List<GetListResponseDTO> GetList();
    }
}
