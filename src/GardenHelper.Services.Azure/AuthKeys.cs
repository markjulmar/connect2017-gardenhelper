using System;
using System.Collections.Generic;
using System.Text;

namespace GardenHelper.Services.Azure
{
    static class AuthKeys
    {
        // Bing Entity API
        internal const string EntityApiKey = "TODO";
        internal const string BingEntitiesUrl = "TODO";

        // Custom Vision Prediction API
        internal const string VisionPredictionKey = "TODO";
        internal static Guid GardenCenterProjectId = new Guid("PROJECT_GUID_FROM_URL");
        internal const string VisionPredictionUrl = "TODO";

        // Vision API
        internal const string VisionSubscriptionKey = "TODO";
        internal const string VisionUrl = "TODO";

        // Azure Pricing Mobile Service
        internal const string GardenCenterMobileUrl = "https://gardenhelper.azurewebsites.net/";
    }
}
