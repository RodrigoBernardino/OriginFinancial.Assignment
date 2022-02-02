namespace ScoreCalculationEngine.Application.DTOs
{
    internal sealed class Score
    {
        public Score(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
        public bool IsIneligible { get; set; }

        public string GetScoreFriendlyValue()
        {
            if (IsIneligible)
            {
                return "ineligible";
            }

            switch (Value)
            {
                case <= 0:
                    return "economic";
                case 1:
                case 2:
                    return "regular";
                case >= 3:
                    return "responsible";
            }
        }
    }
}
