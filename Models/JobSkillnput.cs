using Microsoft.ML.Data;

namespace SkillSnap.Models
{
    public class JobSkillInput
    {
        [ColumnName("Skills")]
        public string Skills { get; set; } = "";
        [ColumnName("JobRole")]
        public string JobRole { get; set; } = "";
    }
}
