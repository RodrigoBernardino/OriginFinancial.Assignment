using ScoreCalculationEngine.Domain.Extensions;
using ScoreCalculationEngine.Domain.Utils;
using System.ComponentModel.DataAnnotations;

namespace ScoreCalculationEngine.Domain.Models
{
    public sealed class PersonalInformation
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Age must be a positive number")]
        public int Age { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Dependents must be a positive number")]
        public int Dependents { get; set; }

        public House? House { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Income must be a positive number")]
        public int Income { get; set; }

        [Required]
        [StringRange(AllowableValues = new[] { "single", "married" }, ErrorMessage = "Marital status must be either 'single' or 'married'")]
        public string? MaritalStatus { get; set; }

        public int[]? RiskQuestions { get; set; }

        public Vehicle? Vehicle { get; set; }

        public bool IsValid()
        {
            bool isValid = Validator.TryValidateObject(this, new ValidationContext(this),
                null, true);

            if (House is not null)
            {
                isValid = isValid && House.IsValid();
            }

            if (Vehicle is not null)
            {
                isValid = isValid && Vehicle.IsValid();
            }

            return isValid;
        }

        public ICollection<ValidationResult> Validate()
        {
            List<ValidationResult> results = new();

            Validator.TryValidateObject(this, new ValidationContext(this),
                results, true);

            if (RiskQuestions?.Length != 3)
            {
                results.Add(new ValidationResult("Risk Questions must have three answers", 
                    new List<string>{ "RiskQuestions".ToSnakeCase() }));
            }

            if (House is not null)
            {
                results.AddRange(House.Validate());

            }

            if (Vehicle is not null)
            {
                results.AddRange(Vehicle.Validate());
            }

            return results;
        }
    }
}
