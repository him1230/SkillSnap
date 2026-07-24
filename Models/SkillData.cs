using Microsoft.ML.Data;

namespace SkillSnap.Models
{
    public class SkillData
    {
        [LoadColumn(0)]
        public float UserSkillCount { get; set; }

        [LoadColumn(1)]
        public float JobSkillCount { get; set; }

        [LoadColumn(2)]
        public float MatchedSkillCount { get; set; }

        [LoadColumn(3)]
        public float MatchPercentage { get; set; }

        [LoadColumn(4)]
        public bool IsMatch { get; set; } // Label
    }
}
