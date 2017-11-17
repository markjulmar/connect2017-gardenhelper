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
            var t1 = GetImageDescriptionAsync(picture.Invoke());
            var t2 = PredictImageAsync(picture.Invoke());
            await Task.WhenAll(t1, t2).ConfigureAwait(false);

            var description = t1.Result;
            if (description != null)
            {
                if (!description.Description.Tags.Contains("plant")
                 && !description.Description.Tags.Contains("flower"))
                {
                    throw new Exception("Your picture does not appear to contain a plant. Please try again.");
                }

                var tag = t2.Result.Predictions.OrderByDescending(t => t.Probability).FirstOrDefault();
                if (tag?.Probability >= .25)
                    return tag.Tag;
            }

            return null;
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
                VisualFeature[] features = {
                    VisualFeature.Tags,
                    VisualFeature.Categories,
                    VisualFeature.Description
                };

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
