using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPaint.Entities
{
    public class ComplexShape : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        [NotMapped]
        public override int EntityId
        {
            get
            {
                return Id;
            }
            set
            {
                this.Id = value;
            }
        }
    }
}
