namespace SkillSnap.Models
{
    public class JobSkill
    {
        public int ?Id { get; set; }

        public int ?JobId { get; set; }
        public Job ?Job { get; set; }

        public string ?SkillName { get; set; }
    }
}
