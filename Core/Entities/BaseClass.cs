using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BaseClass<Tkey>
    {
        public Tkey Id { get; set; }
    }
}
