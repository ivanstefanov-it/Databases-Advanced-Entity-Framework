using System;
using System.ComponentModel.DataAnnotations;

namespace BillsPaymentSystem.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExpirationDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currDateTime = DateTime.Now;
            var targetDateTime = (DateTime)value;

            if (currDateTime > targetDateTime)
            {
                return new ValidationResult("Card is expired!");
            }

            return ValidationResult.Success;
        }
    }
}
