using ScoreCalculationEngine.Domain.Models;
using System.Collections;
using System.Collections.Generic;

namespace ScoreCalculationEngine.Application.UnitTests.Fixtures
{
    public class ValidPersonalInfoGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            new object[] {
                new PersonalInformation
                {
                    Age = 35,
                    Dependents = 2,
                    Income = 0,
                    MaritalStatus = "married",
                    RiskQuestions = new[] { 0, 1, 0 },
                    House = new House { OwnershipStatus = "owned" },
                    Vehicle = new Vehicle { Year = 2018 }
                }
            },
            new object[] {
                new PersonalInformation
                {
                    Age = 35,
                    Dependents = 2,
                    Income = 100000,
                    MaritalStatus = "married",
                    RiskQuestions = new[] { 0, 1, 0 },
                    House = null,
                    Vehicle = null
                }
            },
            new object[] {
                new PersonalInformation
                {
                    Age = 35,
                    Dependents = 2,
                    Income = 100000,
                    MaritalStatus = "married",
                    RiskQuestions = new[] { 0, 1, 0 },
                    House = new House(),
                    Vehicle = new Vehicle()
                }
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
