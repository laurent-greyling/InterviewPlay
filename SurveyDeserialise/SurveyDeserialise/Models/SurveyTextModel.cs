
using Newtonsoft.Json;

namespace SurveyDeserialise.Models
{
    public class SurveyTextModel
    {
        [JsonProperty("nl-NL")]
        public string NlNl { get; set; }

        [JsonProperty("en-US")]
        public string EnUs { get; set; }
    }
}
