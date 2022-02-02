using ScoreCalculationEngine.Domain.Utils;
using System.ComponentModel.DataAnnotations;

namespace ScoreCalculationEngine.Domain.Models
{
    public sealed class House
    {
        [StringRange(AllowableValues = new[] { "owned", "mortgaged" }, MemberName = "House.ownershipStatus", IsNullable = true, ErrorMessage = "House's ownership status must be either 'owned' or 'mortgaged'")]
        public string? OwnershipStatus { get; set; }

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

            return results;
        }
    }
}
