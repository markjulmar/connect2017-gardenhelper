using GardenHelper.Interfaces;
using Microsoft.Cognitive.CustomVision;
using Microsoft.Cognitive.CustomVision.Models;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GardenHelper.Services.Azure
{
    public class AzureVisionService : IIdentifyPicture
    {
        public async Task<string> IdentifyAsync(Func<Stream> picture)
        {
            var description = await GetImageDescriptionAsync(picture.Invoke());
            if (description?.Tags.Any(t => t.Name == "plant" || t.Name == "flower") != true)
            {
                throw new Exception("Your picture does not appear to contain a plant. Please try again.");
            }

            var hint = description.Tags.FirstOrDefault(t => t.Hint == "flower");
            if (hint != null)
                return hint.Name;

        	var result = await PredictImageAsync(picture.Invoke());
            var tag = result.Predictions.OrderByDescending(t => t.Probability).FirstOrDefault();
            if (tag?.Probability < .25)
            {
                throw new Exception("I'm sorry, I don't recognize that plant.");
            }

            return tag?.Tag;
        }

        private async Task<ImagePredictionResultModel> PredictImageAsync(Stream stream)
        {
            var ep = new PredictionEndpoint(new PredictionEndpointCredentials(AuthKeys.VisionPredictionKey)) { BaseUri = new Uri(AuthKeys.VisionPredictionUrl) };
            var response = await ep.PredictImageWithHttpMessagesAsync(AuthKeys.GardenCenterProjectId, stream).ConfigureAwait(false);
            return response.Body;
        }

        private Task<AnalysisResult> GetImageDescriptionAsync(Stream imageStream)
        {
            try
            {
                VisualFeature[] features = { VisualFeature.Tags };
                IVisionServiceClient visionClient = new VisionServiceClient(AuthKeys.VisionSubscriptionKey, AuthKeys.VisionUrl);

                return visionClient.AnalyzeImageAsync(imageStream, features.ToList(), null);
            }
            catch (ClientException ex)
            {
                throw new Exception(ex.Error.Message, ex);
            }
        }
    }
}
