namespace SkillSnap.Models
{
    public class JobRecommendationViewModel
    {
        //public int ?JobId { get; set; }
        public string ?JobTitle { get; set; }
        public int ?MatchScore { get; set; }
        public List<string>? MatchedSkills { get; set; } = new();
        public string AIExplanation { get; set; } = string.Empty;

        //public string ConfidenceLevel { get; set; } = string.Empty;

    }
}
