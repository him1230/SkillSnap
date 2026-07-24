using Microsoft.AspNetCore.Identity;

namespace SkillSnap.Models
{
    public class Skill
    {
        public int ?Id { get; set; }
        public string ?Name { get; set; }
        public string ?UserId { get; set; }
        public IdentityUser ?User { get; set; }
    }
}
