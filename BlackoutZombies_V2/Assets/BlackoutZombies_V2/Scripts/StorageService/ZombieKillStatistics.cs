using Newtonsoft.Json;

public class ZombieKillStatistics : IService
{
    [JsonProperty(PropertyName = "DZC")]
    public int BestDeathZombiesCountInRound { get; set; }
    [JsonProperty(PropertyName = "RDZC")]
    public int RoundDeathZombiesCount { get; set; }
    [JsonProperty(PropertyName = "RC")]
    public int RoundScore { get; set; }
    [JsonProperty(PropertyName = "BC")]
    public int BestScore { get; set; }
}
