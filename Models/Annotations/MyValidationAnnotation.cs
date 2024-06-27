﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MyValidationAnnotation : ValidationAttribute
    {
        public string Exclude { get; set; }

        public override bool IsValid(object? value)
        {
            return !((value as string)?.Contains(Exclude) ?? true);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Właściwość {name} nie może zawierać \"{Exclude}\"";
        }
    }
}
