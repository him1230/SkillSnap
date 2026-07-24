using Microsoft.ML;
using SkillSnap.Models;

namespace SkillSnap.Services
{
    public class SkillMatchService
    {
        //    private readonly PredictionEngine<SkillData, SkillPrediction> _engine;

        public SkillMatchService()
        {
            //        var mlContext = new MLContext();

            //        var modelPath = Path.Combine(
            //            Directory.GetCurrentDirectory(),
            //            "wwwroot", "ml", "SkillMatchModel.zip"
            //        );

            //        var model = mlContext.Model.Load(modelPath, out _);
            //        _engine = mlContext.Model.CreatePredictionEngine<SkillData, SkillPrediction>(model);
            //    }

            //    public SkillPrediction Predict(
            //        int userSkillCount,
            //        int jobSkillCount,
            //        int matchedSkillCount)
            //    {
            //        float matchPercentage = jobSkillCount == 0
            //            ? 0
            //            : (matchedSkillCount * 100f) / jobSkillCount;

            //        var input = new SkillData
            //        {
            //            UserSkillCount = userSkillCount,
            //            JobSkillCount = jobSkillCount,
            //            MatchedSkillCount = matchedSkillCount,
            //            MatchPercentage = matchPercentage
            //        };

            //        return _engine.Predict(input);
            // }


        }
    }
}
