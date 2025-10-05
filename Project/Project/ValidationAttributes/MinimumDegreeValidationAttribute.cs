using Project.Models; // We need this to access the Course model
using System.ComponentModel.DataAnnotations;

namespace Project.ValidationAttributes
{
    // This is a custom validation attribute to ensure MinDegree is less than Degree.
    public class MinimumDegreeValidationAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        // The constructor takes the name of the property to compare against (i.e., "Degree")
        public MinimumDegreeValidationAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        // This is the main validation method that gets executed.
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Get the value of the property this attribute is attached to (MinDegree)
            var minValue = (int?)value;

            // Get the property to compare against (Degree)
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            // Get the value of the Degree property
            var maxValue = (int?)property.GetValue(validationContext.ObjectInstance);

            // The core validation logic
            if (minValue.HasValue && maxValue.HasValue && minValue.Value >= maxValue.Value)
            {
                // If the validation fails, return an error message.
                return new ValidationResult(ErrorMessage ?? "Minimum degree must be less than the maximum degree.");
            }

            // If the validation succeeds, return success.
            return ValidationResult.Success;
        }
    }
}

