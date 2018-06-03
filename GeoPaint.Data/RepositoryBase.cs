using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Common;

namespace GeoPaint.Data
{
    public class RepositoryBase
    {
        public RepositoryBase()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }
        }
    }
}
