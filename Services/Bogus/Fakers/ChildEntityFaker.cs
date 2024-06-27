using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus.Fakers
{
    public abstract class ChildEntityFaker<T> : EntityFaker<T> where T : ChildEntity
    {
        public ChildEntityFaker()
        {
            RuleFor(x => x.ParentId, x => new Random(x.Random.Number(1000)).Next(1, 50));
        }
    }
}
