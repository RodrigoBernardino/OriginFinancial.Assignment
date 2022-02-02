using ScoreCalculationEngine.Application.DTOs;
using ScoreCalculationEngine.Domain.Extensions;
using ScoreCalculationEngine.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ScoreCalculationEngine.Application.Services
{
    public class ScoreCalculationService
    {
        public RiskProfile CalculateScore(PersonalInformation personalInfo)
        {
            if (!personalInfo.IsValid())
            {
                ICollection<ValidationResult> validationResults = personalInfo.Validate();
                if (validationResults.Any())
                {
                    throw new ArgumentException(string.Join($";{Environment.NewLine}", 
                        validationResults.Select(r => $"[{r.MemberNames.FirstOrDefault()?.ToSnakeCase()}] {r.ErrorMessage}")));
                }
            }

            int baseScore = personalInfo.RiskQuestions?.Sum() 
                ?? throw new NullReferenceException("risk_question must not be null");

            RiskProfile riskProfile = new(baseScore);

            if (personalInfo.Income == 0)
            {
                riskProfile.DisabilityScore.IsIneligible = true;
            }

            if (personalInfo.Vehicle?.Year is null)
            {
                riskProfile.AutoScore.IsIneligible = true;
            }

            if (personalInfo.House?.OwnershipStatus is null)
            {
                riskProfile.HomeScore.IsIneligible = true;
            }

            switch (personalInfo.Age)
            {
                case > 60:
                    riskProfile.DisabilityScore.IsIneligible = true;
                    riskProfile.LifeScore.IsIneligible = true;
                    break;
                case < 30:
                    riskProfile.AutoScore.Value -= 2;
                    riskProfile.DisabilityScore.Value -= 2;
                    riskProfile.HomeScore.Value -= 2;
                    riskProfile.LifeScore.Value -= 2;
                    break;
                case >= 30 and < 40:
                    riskProfile.AutoScore.Value -= 1;
                    riskProfile.DisabilityScore.Value -= 1;
                    riskProfile.HomeScore.Value -= 1;
                    riskProfile.LifeScore.Value -= 1;
                    break;
            }

            if (personalInfo.Income > 200000)
            {
                riskProfile.AutoScore.Value -= 1;
                riskProfile.DisabilityScore.Value -= 1;
                riskProfile.HomeScore.Value -= 1;
                riskProfile.LifeScore.Value -= 1;
            }

            if (personalInfo.House?.OwnershipStatus == "mortgaged")
            {
                riskProfile.DisabilityScore.Value += 1;
                riskProfile.HomeScore.Value += 1;
            }

            if (personalInfo.Dependents > 0)
            {
                riskProfile.DisabilityScore.Value += 1;
                riskProfile.LifeScore.Value += 1;
            }

            if (personalInfo.MaritalStatus == "married")
            {
                riskProfile.DisabilityScore.Value -= 1;
                riskProfile.LifeScore.Value += 1;
            }

            if (DateTime.Now.Year - personalInfo.Vehicle?.Year <= 5)
            {
                riskProfile.AutoScore.Value += 1;
            }

            return riskProfile;
        }
    }
}
