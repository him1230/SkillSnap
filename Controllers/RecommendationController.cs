using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using SkillSnap.Data;
using SkillSnap.Models;
using SkillSnap.Services;

[Authorize]
public class RecommendationController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    // private readonly SkillMatchService _mlService;
    private readonly MLContext _mlContext;
    private readonly ITransformer _model;

    public RecommendationController(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager, MLContext mlContext,ITransformer model)
    {
        _context = context;
        _userManager = userManager;
        // _mlService = mlService;
        _mlContext = mlContext;
        _model = model;
    }

    //public IActionResult Index()
    //{
    //    //    var userId = _userManager.GetUserId(User);
    //    //    var userSkills = _context.Skills
    //    //.Where(s => s.UserId == userId)
    //    //.Select(s => s.Name)
    //    //.ToList();

    //    //    string skillText = string.Join(",", userSkills);

    //    //    var engine = _mlContext.Model
    //    //        .CreatePredictionEngine<JobSkillInput, JobSkillOutput>(_model);

    //    //    var prediction = engine.Predict(new JobSkillInput
    //    //    {
    //    //        Skills = skillText,
    //    //         JobRole = ""
    //    //    });
    //    //    float maxScore = prediction.Score.Max(); // 0–1
    //    //    int confidencePercent = (int)(maxScore * 100);
    //    //    var recommendations = new List<JobRecommendationViewModel>();
    //    //    recommendations.Add(new JobRecommendationViewModel
    //    //    {
    //    //        JobTitle = prediction.JobRole,
    //    //        MatchScore = confidencePercent, // % shown in UI
    //    //        AIExplanation =
    //    //         $"Based on your skills ({skillText}), the AI predicts " +
    //    //        $"you are best suited for {prediction.JobRole} with " +
    //    //        $"{confidencePercent}% confidence."

    //    //    });
    //    //     return View(recommendations
    //    //            .OrderByDescending(r => r.MatchScore)
    //    //            .ToList


    //        var userId = _userManager.GetUserId(User);

    //        var userSkills = _context.Skills
    //            .Where(s => s.UserId == userId)
    //            .Select(s => s.Name)
    //            .ToList();

    //        string skillText = string.Join(",", userSkills);
    //        var engine = _mlContext.Model
    //    .CreatePredictionEngine<JobSkillInput, JobSkillOutput>(_model);

    //    var prediction = engine.Predict(new JobSkillInput
    //    {
    //        Skills = skillText
    //    });

    //    // 🔥 get labels SAFELY
    //    var schema = engine.OutputSchema;
    //    var scoreColumn = schema["Score"];

    //    var labelBuffer = new VBuffer<ReadOnlyMemory<char>>();
    //    scoreColumn.GetSlotNames(ref labelBuffer);

    //    var labels = labelBuffer.DenseValues()
    //        .Select(x => x.ToString())
    //        .ToArray();

    //    var recommendations = new List<JobRecommendationViewModel>();

    //    for (int i = 0; i < labels.Length; i++)
    //    {
    //        int percent = (int)(prediction.Score[i] * 100);
    //        if (percent < 15) continue;

    //        recommendations.Add(new JobRecommendationViewModel
    //        {
    //            JobTitle = labels[i],   // ✅ REAL JOB ROLE
    //            MatchScore = percent,
    //            AIExplanation =
    //                $"Based on your skills ({skillText}), AI predicts " +
    //                $"{labels[i]} with {percent}% confidence."
    //        });
    //    }
    //    return View(
    //        recommendations
    //            .OrderByDescending(r => r.MatchScore)
    //            .ToList()
    //    );

    //    //    var userSkills = _context.Skills
    //    //        .Where(s => s.UserId == userId)
    //    //        .Select(s => s.Name)
    //    //        .ToList();

    //    //    var jobs = _context.Jobs.ToList();
    //    //    var recommendations = new List<JobRecommendationViewModel>();

    //    //    foreach (var job in jobs)
    //    //    {
    //    //        var requiredSkills = _context.JobSkills
    //    //            .Where(js => js.JobId == job.Id)
    //    //            .Select(js => js.SkillName)
    //    //            .ToList();

    //    //        if (!requiredSkills.Any())
    //    //            continue;

    //    //        var matchedSkills = requiredSkills
    //    //            .Where(rs => userSkills.Contains(rs))
    //    //            .ToList();

    //    //        if (!matchedSkills.Any())
    //    //            continue;

    //    //        // int score = (matchedSkills.Count * 100) / requiredSkills.Count;//manually calucation percent 
    //    //        var prediction = _mlService.Predict(userSkills.Count,requiredSkills.Count, matchedSkills.Count);
    //    //        int score = (int)Math.Min(Math.Abs(prediction.Score * 100), 100);
    //    //        // 🔥 AI Explanation
    //    //        //string aiExplanation = GenerateAiExplanation(
    //    //        //    job.Title,
    //    //        //    matchedSkills,
    //    //        //    requiredSkills,
    //    //        //    score);
    //    //        string aiExplanation = prediction.IsMatch
    //    //        ? $"ML model predicts a strong match ({score}%) based on your skills: {string.Join(", ", matchedSkills)}."
    //    //        : $"ML model predicts a weak match. Improve skills like {string.Join(", ", requiredSkills.Except(matchedSkills))}.";

    //    //        string confidence = score >= 80
    //    //            ? "High Fit 🔥"
    //    //            : score >= 50
    //    //                ? "Good Fit 👍"
    //    //                : "Partial Fit ⚠";

    //    //        recommendations.Add(new JobRecommendationViewModel
    //    //        {
    //    //            JobId = job.Id,
    //    //            JobTitle = job.Title,
    //    //            MatchScore = score,
    //    //            MatchedSkills = matchedSkills,
    //    //            AIExplanation = aiExplanation,
    //    //            ConfidenceLevel = confidence
    //    //        });
    //    //    }

    //    //    return View(recommendations
    //    //        .OrderByDescending(r => r.MatchScore)
    //    //        .ToList());
    //    //}

    //    //// 🤖 AI LOGIC
    //    //private string GenerateAiExplanation(
    //    //    string jobTitle,
    //    //    List<string> matchedSkills,
    //    //    List<string> requiredSkills,
    //    //    int score)
    //    //{
    //    //    var missingSkills = requiredSkills
    //    //        .Except(matchedSkills)
    //    //        .ToList();

    //    //    if (score >= 80)
    //    //    {
    //    //        return $"Excellent match for the role of {jobTitle}. " +
    //    //               $"You already have strong skills in {string.Join(", ", matchedSkills)}.";
    //    //    }

    //    //    if (score >= 50)
    //    //    {
    //    //        return $"Good match for {jobTitle}. " +
    //    //               $"Your skills in {string.Join(", ", matchedSkills)} align well. " +
    //    //               $"Consider improving {string.Join(", ", missingSkills)}.";
    //    //    }

    //    //    return $"Partial match for {jobTitle}. " +
    //    //           $"You match some skills like {string.Join(", ", matchedSkills)}. " +
    //    //           $"Learning {string.Join(", ", missingSkills)} will boost your chances.";
    //}
    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User);
        var userSkills = _context.Skills
            .Where(s => s.UserId == userId)
            .Select(s => s.Name.ToLower().Trim()).ToList();

        string skillText = string.Join(",", userSkills);

        // 1. ML Prediction
        var engine = _mlContext.Model.CreatePredictionEngine<JobSkillInput, JobSkillOutput>(_model);
        var prediction = engine.Predict(new JobSkillInput { Skills = skillText });

        // 2. Get Labels from Model
        var labelBuffer = new VBuffer<ReadOnlyMemory<char>>();
        engine.OutputSchema["Score"].GetSlotNames(ref labelBuffer);
        var labels = labelBuffer.DenseValues().Select(x => x.ToString()).ToArray();

        // 3. Load CSV Data to find matching skills
        var dataPath = Path.Combine(AppContext.BaseDirectory, "Data", "job_skill_training.csv");
        var csvLines = System.IO.File.ReadAllLines(dataPath).Skip(1); // Header skip karein

        var recommendations = new List<JobRecommendationViewModel>();

        for (int i = 0; i < labels.Length; i++)
        {
            int percent = (int)(prediction.Score[i] * 100);
            if (percent < 10) continue;

            string currentRole = labels[i];

            // 🔥 CSV se is Role ki ideal skills dhundho
            // Current role ki skills nikaalne ka sahi tarika
            var roleSkills = csvLines
                .Select(line => line.Split("\",\"")) // Quote + Comma se split karo
                .Where(parts => parts.Length > 1 && parts[1].Replace("\"", "").Trim().Equals(currentRole, StringComparison.OrdinalIgnoreCase))
                .SelectMany(parts => parts[0].Replace("\"", "").ToLower().Split(','))
                .Select(s => s.Trim())
                .Distinct().ToList();


            var matched = userSkills.Intersect(roleSkills).ToList();
            var lacking = roleSkills.Except(userSkills).Take(3).ToList();

            // 🤖 Dynamic Explanation
            string explanation = $"You match {matched.Count} key skills for this role. ";
            if (lacking.Any())
            {
                explanation += $"To reach 100%, consider learning: {string.Join(", ", lacking)}.";
            }
            else
            {
                explanation += "You have all the core skills required for this role!";
            }

            recommendations.Add(new JobRecommendationViewModel
            {
                JobTitle = currentRole,
                MatchScore = percent,
                MatchedSkills = matched,
                AIExplanation = explanation
            });
        }

        return View(recommendations.OrderByDescending(r => r.MatchScore).ToList());
    }

}
