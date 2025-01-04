using Newtonsoft.Json;

public class KillZombiesCount : IService
{
    [JsonProperty(PropertyName = "DZC")]
    public int DeathZombiesCount { get; set; }
    [JsonProperty(PropertyName = "RDZC")]
    public int RoundDeathZombiesCount { get; set; }
    [JsonProperty(PropertyName = "RC")]
    public int RoundScore { get; set; }
    [JsonProperty(PropertyName = "BC")]
    public int BestScore { get; set; }
}
