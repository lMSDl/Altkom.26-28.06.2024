using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ShoppingList : Entity
    {
        public string? Name { get; set; }
        public DateTime DateTime { get; } = DateTime.Now;
    }
}
