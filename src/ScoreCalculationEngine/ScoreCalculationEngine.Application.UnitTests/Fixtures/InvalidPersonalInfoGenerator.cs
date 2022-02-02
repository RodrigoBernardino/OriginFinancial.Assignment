using ScoreCalculationEngine.Domain.Models;
using System.Collections;
using System.Collections.Generic;

namespace ScoreCalculationEngine.Application.UnitTests.Fixtures
{
    public class InvalidPersonalInfoGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            new object[] { 
                new PersonalInformation
                {
                    Age = -2,
                    Dependents = -1,
                    Income = -2,
                    MaritalStatus = "alone",
                    RiskQuestions = new[] { 0, 1 },
                    House = new House { OwnershipStatus = "borrowed" },
                    Vehicle = new Vehicle { Year = -3000 }
                }
            },
            new object[] {
                new PersonalInformation
                {
                    Age = 35,
                    Dependents = 2,
                    Income = 0,
                    MaritalStatus = "married",
                    RiskQuestions = new[] { 0, 1, 0 },
                    House = new House { OwnershipStatus = "borrowed" },
                    Vehicle = new Vehicle { Year = -3000 }
                }
            },
            new object[] {
                new PersonalInformation
                {
                    Age = 100,
                    Dependents = -30,
                    Income = int.MinValue,
                    MaritalStatus = "married",
                    RiskQuestions = new[] { 0, 1, 0 },
                    House = new House { OwnershipStatus = string.Empty },
                    Vehicle = new Vehicle { Year = 2030 }
                }
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
