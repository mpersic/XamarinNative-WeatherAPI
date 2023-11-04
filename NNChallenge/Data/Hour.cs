using Newtonsoft.Json;

namespace NNChallenge.Data
{
    public class Hour
    {
        public string Time { get; set; }

        [JsonProperty("temp_c")]
        public float TemperatureCelsius { get; set; }

        [JsonProperty("temp_f")]
        public float TemperatureFahrenheit { get; set; }

        [JsonProperty("Condition")]
        public Condition Condition { get; set; }
    }

}
