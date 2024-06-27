using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product : ChildEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }

        public int DefaultInt { get; set; }
        public string DefaultString { get; set; }
        public float ReadOnlyFloat => 50;
    }
}
