using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using KT.Extensions;

namespace CZDataObjects.CustomAttributes
{
    public class DateCompareAttribute : CompareAttribute
    {
        public DateCompareAttribute(string otherProperty, string compareType)
            : base(otherProperty)
        { 
            switch(compareType)
            {
                case "e":
                    _isEqual = true;
                    break;

                case "a":
                    _isAfter = true;
                    break;

                case "b":
                    _isBefore = true;
                    break;

                default:
                    _isEqual = true;
                    break;

            }
        }

        private bool _isEqual;
        private bool _isAfter;
        private bool _isBefore;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (property == null)
            {
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Unknown property {0}", this.OtherProperty));
            }

            var otherValue = property.GetValue(validationContext.ObjectInstance, null) as string;
            if (string.Equals(value as string, otherValue, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));

        }
    }

    public class CaseInsensitiveCompareAttribute : CompareAttribute
    {
        public CaseInsensitiveCompareAttribute(string otherProperty)
            : base(otherProperty)
        { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (property == null)
            {
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Unknown property {0}", this.OtherProperty));
            }

            var otherValue = property.GetValue(validationContext.ObjectInstance, null) as string;
            if (string.Equals(value as string, otherValue, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));

        }
    }
}
