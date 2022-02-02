using ScoreCalculationEngine.Application.DTOs;
using ScoreCalculationEngine.Application.Services;
using ScoreCalculationEngine.Application.UnitTests.Fixtures;
using ScoreCalculationEngine.Domain.Models;
using System;
using Xunit;

namespace ScoreCalculationEngine.Application.UnitTests.Tests
{
    public class ScoreCalculationTests
    {
        [Theory]
        [ClassData(typeof(InvalidPersonalInfoGenerator))]
        public void Validate_invalid_personal_information(PersonalInformation personalInformation)
        {
            //Arrange
            ScoreCalculationService scoreCalculationService = new();

            //Act
            Func<RiskProfile> act = () => scoreCalculationService.CalculateScore(personalInformation);

            //Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [ClassData(typeof(ValidPersonalInfoGenerator))]
        public void Validate_valid_personal_information(PersonalInformation personalInformation)
        {
            //Arrange
            ScoreCalculationService scoreCalculationService = new();

            //Act
            RiskProfile riskProfile = scoreCalculationService.CalculateScore(personalInformation);

            //Assert
            Assert.NotNull(riskProfile);
        }

        [Fact]
        public void Validate_risk_profile_results()
        {
            //Arrange
            ScoreCalculationService scoreCalculationService = new();
            PersonalInformation personalInformation = new()
            {
                Age = 35,
                Dependents = 2,
                Income = 0,
                MaritalStatus = "married",
                RiskQuestions = new[] { 0, 1, 0 },
                House = new House { OwnershipStatus = "owned" },
                Vehicle = new Vehicle { Year = 2018 }
            };

            //Act
            RiskProfile riskProfile = scoreCalculationService.CalculateScore(personalInformation);

            //Assert
            Assert.NotNull(riskProfile);
            Assert.Equal("regular", riskProfile.Auto);
            Assert.Equal("ineligible", riskProfile.Disability);
            Assert.Equal("economic", riskProfile.Home);
            Assert.Equal("regular", riskProfile.Life);
        }
    }
}