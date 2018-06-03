using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPaint.Entities
{
    public abstract class EntityBase
    {
        [NotMapped]
        public abstract int EntityId { get; set; }
    }
}
