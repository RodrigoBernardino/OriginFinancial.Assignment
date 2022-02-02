using System.ComponentModel.DataAnnotations;

namespace ScoreCalculationEngine.Domain.Utils
{
    public sealed class StringRangeAttribute : ValidationAttribute
    {
        public string[]? AllowableValues { get; set; }
        public bool IsNullable { get; set; }
        public string? MemberName { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(MemberName))
            {
                validationContext.MemberName = MemberName;
            }

            if (IsNullable && value is null)
            {
                return ValidationResult.Success;
            }

            if (AllowableValues?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            if (string.IsNullOrWhiteSpace(ErrorMessage))
            {
                ErrorMessage =
                    $"Please enter one of the allowable values: {string.Join(", ", AllowableValues ?? new[] { "No allowable values found" })}.";
            }

            return new ValidationResult(ErrorMessage, new List<string> { GetMemberName(validationContext) });
        }

        private string GetMemberName(ValidationContext validationContext)
        {
            return MemberName ?? validationContext.MemberName ?? string.Empty;
        }
    }
}
