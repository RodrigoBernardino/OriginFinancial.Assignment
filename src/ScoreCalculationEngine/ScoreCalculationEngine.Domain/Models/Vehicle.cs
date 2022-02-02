using ScoreCalculationEngine.Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ScoreCalculationEngine.Domain.Models
{
    public sealed class Vehicle
    {
        [Range(0, int.MaxValue, ErrorMessage = "Vehicle's year must be a positive number")]
        public int? Year { get; set; }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this),
                null, true);
        }

        public ICollection<ValidationResult> Validate()
        {
            List<ValidationResult> results = new();

            Validator.TryValidateObject(this, new ValidationContext(this),
                results, true);

            if (Year > DateTime.Now.Year)
            {
                results.Add(new ValidationResult("Vehicle's year cannot be bigger than the current year",
                    new List<string> { "Vehicle.year".ToSnakeCase() }));
            }

            return results;
        }
    }
}
