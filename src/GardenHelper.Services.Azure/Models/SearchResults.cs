using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GardenHelper.Models.Internal
{
    internal class SearchResponse
    {
        [JsonProperty("_type")]
        public string Type { get; set; }
        public QueryContext QueryContext { get; set; }
        public Computation Computation { get; set; }
        public Entities Entities { get; set; }
        public Images Images { get; set; }
        public WebAnswer WebPages { get; set; }
    }

    internal class QueryContext
    {
        public string OriginalQuery { get; set; }
        public string AlteredQuery { get; set; }
        public string AlterationOverrideQuery { get; set; }
    }

    internal class Computation
    {
        public string Expression { get; set; }
        public string Value { get; set; }
    }

    internal class Entities
    {
        public string QueryScenario { get; set; }
        public IList<Entity> Value { get; set; }
    }

    internal class Entity
    {
        public IList<ContractualRules> ContractualRules { get; set; }
        public string BingId { get; set; }
        public string WebSearchUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }
        public EntityPresentationInfo EntityPresentationInfo { get; set; }

    }

    internal class EntityPresentationInfo
    {
        public const string DominantEntity = "DominantEntity";

        public string EntityScenario { get; set; }
        public string[] EntityTypeHints { get; set; }
    }

    internal class ContractualRules
    {
        [JsonProperty("_type")]
        public string Type { get; set; }
        public string TargetPropertyName { get; set; }
        public bool MustBeCloseToContent { get; set; }
        public License License { get; set; }
        public string LicenseNotice { get; set; }
    }

    internal class License
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    internal class WebAnswer
    {
        public string Id { get; set; }
        public bool SomeResultsRemoved { get; set; }
        public int TotalEstimatedMatches { get; set; }
        public string WebSearchUrl { get; set; }
        public IList<WebPage> Value { get; set; }
    }

    internal class About
    {
        public string Name { get; set; }
    }

    internal class WebPage
    {
        public string Id { get; set; }
        public IList<WebPage> DeepLinks { get; set; }
        public DateTime DateLastCrawled { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string DisplayUrl { get; set; }
        public string Snippet { get; set; }
        public IList<MetaTag> SearchTags { get; set; }
    }

    internal class MetaTag
    {
        public string Content { get; set; }
        public string Name { get; set; }
    }

    internal class Thumbnail
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    internal class Image
    {
        public string Name { get; set; }
        public DateTime DatePublished { get; set; }
        public string HostPageUrl { get; set; }
        public string ContentSize { get; set; }
        public string HostPageDisplayUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public string WebSearchUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        [JsonProperty("provider")]
        public MediaProvider[] Providers { get; set; }
        public string EncodingFormat { get; set; }
        public string ContentUrl { get; set; }
    }

    internal class MediaProvider
    {
        [JsonProperty("_type")]
        public string Type { get; set; }
        public string Url { get; set; }
    }

    internal class Images
    {
        public string Id { get; set; }
        public string ReadLink { get; set; }
        public string WebSearchUrl { get; set; }
        public bool IsFamilyFriendly { get; set; }
        public IList<Image> Value { get; set; }
        public bool DisplayShoppingSourcesBadges { get; set; }
        public bool DisplayRecipeSourcesBadges { get; set; }
    }

}
