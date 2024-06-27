using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus.Fakers
{
    public class ProductFaker : ChildEntityFaker<Product>
    {
        public ProductFaker()
        {
            RuleFor(x => x.Name, x => x.Commerce.Product());
            RuleFor(x => x.Price, x => float.Parse(x.Commerce.Price()));
        }
    }
}
