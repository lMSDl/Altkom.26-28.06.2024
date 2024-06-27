using Models.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product : ChildEntity
    {
        [NotNull] //wykjluczenie null
        [Required] //wykluczenie null i empty
        [MyValidationAnnotation(Exclude = "jabłka")]
        [MyValidationAnnotation(Exclude = "babany")]
        public string? Name { get; set; }
        [Range(0.01, float.MaxValue, ErrorMessage = "Cena nie może być mniejsza od 0")]
        public float Price { get; set; }

        public int DefaultInt { get; set; }
        public string? DefaultString { get; set; }
        public float ReadOnlyFloat => 50;
    }
}
