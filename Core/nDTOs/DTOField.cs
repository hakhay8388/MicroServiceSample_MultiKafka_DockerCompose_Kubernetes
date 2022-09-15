using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.nDTOs
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DTOField : Attribute
    {
        public bool Nullable { get; private set; }

        public DTOField(bool _Nullable)
        {
            Nullable = _Nullable;
        }
    }
}
