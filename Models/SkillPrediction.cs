using Microsoft.ML.Data;

namespace SkillSnap.Models
{
    public class SkillPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool IsMatch { get; set; }

        public float Score { get; set; }
    }
}
