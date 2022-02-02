namespace ScoreCalculationEngine.Application.DTOs
{
    public sealed class RiskProfile
    {
        internal RiskProfile(int baseScore)
        {
            AutoScore = new Score(baseScore);
            DisabilityScore = new Score(baseScore);
            HomeScore = new Score(baseScore);
            LifeScore = new Score(baseScore);
        }

        public string Auto => AutoScore.GetScoreFriendlyValue();
        public string Disability => DisabilityScore.GetScoreFriendlyValue();
        public string Home => HomeScore.GetScoreFriendlyValue();
        public string Life => LifeScore.GetScoreFriendlyValue();

        internal Score AutoScore { get; set; }
        internal Score DisabilityScore { get; set; }
        internal Score HomeScore { get; set; }
        internal Score LifeScore { get; set; }
    }
}
