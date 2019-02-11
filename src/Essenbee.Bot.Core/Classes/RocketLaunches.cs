using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Essenbee.Bot.Core.Classes
{
    public partial class RocketLaunches
    {
        [JsonProperty("launches")]
        public Launch[] Launches { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Launch
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("windowstart")]
        public DateTimeOffset Windowstart { get; set; }

        [JsonProperty("windowend")]
        public DateTimeOffset Windowend { get; set; }

        [JsonProperty("net")]
        public string Net { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("tbdtime")]
        public long Tbdtime { get; set; }

        [JsonProperty("vidURLs", NullValueHandling = NullValueHandling.Ignore)]
        public Uri[] VidUrLs { get; set; }

        [JsonProperty("infoURLs", NullValueHandling = NullValueHandling.Ignore)]
        public Uri[] InfoUrLs { get; set; }

        [JsonProperty("tbddate")]
        public long Tbddate { get; set; }

        [JsonProperty("probability", NullValueHandling = NullValueHandling.Ignore)]
        public long? Probability { get; set; }

        [JsonProperty("hashtag", NullValueHandling = NullValueHandling.Ignore)]
        public string Hashtag { get; set; }

        [JsonProperty("changed")]
        public DateTimeOffset Changed { get; set; }

        [JsonProperty("lsp")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Lsp { get; set; }
    }

    public partial class RocketLaunches
    {
        public static RocketLaunches FromJson(string json) => JsonConvert.DeserializeObject<RocketLaunches>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this RocketLaunches self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
