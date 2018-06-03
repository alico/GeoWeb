using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Entities;

namespace GeoPaint.Data.Contracts
{
    public interface IGeoPaintSQLRepository : IRepository<ComplexShape>
    {
    }
}
