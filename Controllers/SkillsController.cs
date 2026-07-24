using Microsoft.AspNetCore.Mvc;
using SkillSnap.Data;
using SkillSnap.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;



namespace SkillSnap.Controllers
{
    [Authorize]
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SkillsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Skills
        public IActionResult Index()
        {
            //var skills = _context.Skills.ToList();//no filter
            //var isAuth = User.Identity.IsAuthenticated;
            //return Content(isAuth.ToString());
            var userId = _userManager.GetUserId(User);
            var skills = _context.Skills
                                 .Where(s => s.UserId == userId)
                                 .ToList();

            return View(skills);
        }

        // GET: /Skills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Skills/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                //_context.Skills.Add(skill);
                //_context.SaveChanges();
                skill.UserId = _userManager.GetUserId(User);
                _context.Skills.Add(skill);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(skill);
        }
        // GET: Edit
        public IActionResult Edit(int id)
        {
            var skill = _context.Skills.Find(id);
            return View(skill);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Skill skill)
        {
            var userId = _userManager.GetUserId(User);

            var existingSkill = _context.Skills
                .FirstOrDefault(s => s.Id == skill.Id && s.UserId == userId);

            if (existingSkill == null)
                return Unauthorized();

            existingSkill.Name = skill.Name;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Skills/Delete/5
        public IActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(User);

            var skill = _context.Skills
                .FirstOrDefault(s => s.Id == id && s.UserId == userId);

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);

            var skill = _context.Skills
                .FirstOrDefault(s => s.Id == id && s.UserId == userId);

            if (skill == null)
            {
                return Unauthorized();
            }

            _context.Skills.Remove(skill);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }

    }
