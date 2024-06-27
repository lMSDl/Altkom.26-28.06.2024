using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Person : Entity
    {
        [MaxLength(10)]
        [MinLength(1)]
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
