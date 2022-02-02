#pragma warning disable 1591
using ScoreCalculationEngine.Domain.Extensions;
using System.Text.Json;

namespace ScoreCalculationEngine.Api.Utils
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToSnakeCase();
        }
    }
}
