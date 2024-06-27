using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ParentChildController : ApiController
    {

        [HttpGet]
        public IActionResult Get()
        {
            var parent = new Parent() { Name = "A" };
            var child = new Child() { Name = "B" };

            parent.Child = child;
            child.Parent = parent;

            return Ok(parent);
        }


        public class Parent
        {

            public string Name { get; set; }
            public Child Child { get; set; }

        }

        public class Child
        {

            public string Name { get; set; }
            public Parent Parent { get; set; }

        }
    }
}
