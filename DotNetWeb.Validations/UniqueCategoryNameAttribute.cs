using DotNetWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace DotNetWeb.Validations
{
    public class UniqueCategoryNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var category = (Category)validationContext.ObjectInstance;

            if (category.Name == category.DisplayOrder.ToString())
            {
                return new ValidationResult("The Category Name cannot exactly match the Display Order.");
            }

            return ValidationResult.Success!;
        }
    }
}
