using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Common;
using System.ComponentModel.Composition;

namespace GeoWeb.Engine
{
    public class BusinessEngineBase
    {
        public BusinessEngineBase()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }
        }
    }
}
