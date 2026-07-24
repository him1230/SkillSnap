using Microsoft.ML.Data;

namespace SkillSnap.Models
{
    public class JobSkillOutput
    {
        [ColumnName("PredictedLabel")]
        public string JobRole { get; set; } = "";

        // Multiclass confidence scores (0–1)
        public float[] Score { get; set; } = Array.Empty<float>();
    }
}
