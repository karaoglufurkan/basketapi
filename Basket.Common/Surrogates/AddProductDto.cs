using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Basket.Common.Surrogates
{
    public class AddProductDto : IValidatableObject
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();

            if (UserId == 0)
            {
                validationResult.Add(new ValidationResult("Invalid user Id"));
            }

            if (Quantity <= 0)
            {
                validationResult.Add(new ValidationResult("Invalid quantity"));
            }

            return validationResult;
        }
    }
}
