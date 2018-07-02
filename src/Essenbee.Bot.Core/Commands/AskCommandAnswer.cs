using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Essenbee.Bot.Core.Commands
{
    public partial class Answer
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("queryContext")]
        public QueryContext QueryContext { get; set; }

        [JsonProperty("webPages")]
        public WebPages WebPages { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("facts")]
        public Facts Facts { get; set; }

        [JsonProperty("rankingResponse")]
        public RankingResponse RankingResponse { get; set; }
    }

    public partial class Entities
    {
        [JsonProperty("value")]
        public EntitiesValue[] Value { get; set; }
    }

    public partial class EntitiesValue
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("contractualRules")]
        public ContractualRule[] ContractualRules { get; set; }

        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("entityPresentationInfo")]
        public EntityPresentationInfo EntityPresentationInfo { get; set; }

        [JsonProperty("bingId")]
        public string BingId { get; set; }
    }

    public partial class ContractualRule
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("targetPropertyName")]
        public string TargetPropertyName { get; set; }

        [JsonProperty("mustBeCloseToContent")]
        public bool MustBeCloseToContent { get; set; }

        [JsonProperty("license", NullValueHandling = NullValueHandling.Ignore)]
        public License License { get; set; }

        [JsonProperty("licenseNotice", NullValueHandling = NullValueHandling.Ignore)]
        public string LicenseNotice { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
    }

    public partial class License
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class EntityPresentationInfo
    {
        [JsonProperty("entityScenario")]
        public string EntityScenario { get; set; }

        [JsonProperty("entityTypeHints")]
        public string[] EntityTypeHints { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("provider")]
        public Provider[] Provider { get; set; }

        [JsonProperty("hostPageUrl")]
        public string HostPageUrl { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("sourceWidth")]
        public long SourceWidth { get; set; }

        [JsonProperty("sourceHeight")]
        public long SourceHeight { get; set; }
    }

    public partial class Provider
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class Facts
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("contractualRules")]
        public PurpleContractualRule[] ContractualRules { get; set; }

        [JsonProperty("attributions")]
        public Attribution[] Attributions { get; set; }

        [JsonProperty("value")]
        public FactsValue[] Value { get; set; }
    }

    public partial class Attribution
    {
        [JsonProperty("providerDisplayName")]
        public string ProviderDisplayName { get; set; }

        [JsonProperty("seeMoreUrl")]
        public string SeeMoreUrl { get; set; }
    }

    public partial class PurpleContractualRule
    {
        [JsonProperty("_type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
    }

    public partial class FactsValue
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("subjectName")]
        public string SubjectName { get; set; }
    }

    public partial class QueryContext
    {
        [JsonProperty("originalQuery")]
        public string OriginalQuery { get; set; }
    }

    public partial class RankingResponse
    {
        [JsonProperty("mainline")]
        public Mainline Mainline { get; set; }

        [JsonProperty("sidebar")]
        public Mainline Sidebar { get; set; }
    }

    public partial class Mainline
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("answerType")]
        public AnswerType AnswerType { get; set; }

        [JsonProperty("value")]
        public ItemValue Value { get; set; }

        [JsonProperty("resultIndex", NullValueHandling = NullValueHandling.Ignore)]
        public long? ResultIndex { get; set; }
    }

    public partial class ItemValue
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class WebPages
    {
        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }

        [JsonProperty("totalEstimatedMatches")]
        public long TotalEstimatedMatches { get; set; }

        [JsonProperty("value")]
        public WebPagesValue[] Value { get; set; }
    }

    public partial class WebPagesValue
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("isFamilyFriendly")]
        public bool IsFamilyFriendly { get; set; }

        [JsonProperty("displayUrl")]
        public string DisplayUrl { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }

        [JsonProperty("dateLastCrawled")]
        public DateTimeOffset DateLastCrawled { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }

        [JsonProperty("about", NullValueHandling = NullValueHandling.Ignore)]
        public About[] About { get; set; }

        [JsonProperty("snippetAttribution", NullValueHandling = NullValueHandling.Ignore)]
        public SnippetAttribution SnippetAttribution { get; set; }

        [JsonProperty("richCaption", NullValueHandling = NullValueHandling.Ignore)]
        public RichCaption RichCaption { get; set; }
    }

    public partial class About
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class RichCaption
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("sections")]
        public Section[] Sections { get; set; }
    }

    public partial class Section
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("siteName")]
        public string SiteName { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("tabularData", NullValueHandling = NullValueHandling.Ignore)]
        public TabularData TabularData { get; set; }

        [JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
        public ListDatum[] ListData { get; set; }
    }

    public partial class ListDatum
    {
        [JsonProperty("listItems")]
        public PurpleContractualRule[] ListItems { get; set; }
    }

    public partial class TabularData
    {
        [JsonProperty("rows")]
        public Row[] Rows { get; set; }
    }

    public partial class Row
    {
        [JsonProperty("cells")]
        public PurpleContractualRule[] Cells { get; set; }
    }

    public partial class SnippetAttribution
    {
        [JsonProperty("license")]
        public License License { get; set; }

        [JsonProperty("licenseNotice")]
        public string LicenseNotice { get; set; }
    }

    public enum AnswerType { Entities, Facts, WebPages };

    public enum Language { En };

    public partial class Answer
    {
        public static Answer FromJson(string json) => JsonConvert.DeserializeObject<Answer>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Answer self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                AnswerTypeConverter.Singleton,
                LanguageConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class AnswerTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AnswerType) || t == typeof(AnswerType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Entities":
                    return AnswerType.Entities;
                case "Facts":
                    return AnswerType.Facts;
                case "WebPages":
                    return AnswerType.WebPages;
            }
            throw new Exception("Cannot unmarshal type AnswerType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AnswerType)untypedValue;
            switch (value)
            {
                case AnswerType.Entities:
                    serializer.Serialize(writer, "Entities");
                    return;
                case AnswerType.Facts:
                    serializer.Serialize(writer, "Facts");
                    return;
                case AnswerType.WebPages:
                    serializer.Serialize(writer, "WebPages");
                    return;
            }
            throw new Exception("Cannot marshal type AnswerType");
        }

        public static readonly AnswerTypeConverter Singleton = new AnswerTypeConverter();
    }

    internal class LanguageConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Language) || t == typeof(Language?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "en")
            {
                return Language.En;
            }
            throw new Exception("Cannot unmarshal type Language");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Language)untypedValue;
            if (value == Language.En)
            {
                serializer.Serialize(writer, "en");
                return;
            }
            throw new Exception("Cannot marshal type Language");
        }

        public static readonly LanguageConverter Singleton = new LanguageConverter();
    }
}
