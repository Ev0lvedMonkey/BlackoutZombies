using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameUI : ResourcePrefab
{
    [SerializeField] private TextMeshProUGUI _roundScoreTextValue;
    [SerializeField] private TextMeshProUGUI _bestRoundScoreTextValue;
    [SerializeField] private TextMeshProUGUI _roundKilledZombies;
    [SerializeField] private TextMeshProUGUI _bestRoundKilledZombies;

    private EventManager _eventManager;
    private ZombieKillStatistics _statistics;

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneLoader.LoadScene(Scenes.GameScene);
        if (Input.GetKeyDown(KeyCode.Mouse0))
            SceneLoader.LoadScene(Scenes.MenuScene);
    }

    private void Init()
    {
        _eventManager = ServiceLocator.Current.Get<EventManager>();
        _statistics = ServiceLocator.Current.Get<ZombieKillStatistics>();
        _roundScoreTextValue.text = $"{_statistics.RoundScore}";
        _bestRoundScoreTextValue.text = $"{_statistics.BestScore}";
        _bestRoundKilledZombies.text = $"{_statistics.BestDeathZombiesCountInRound}";
        _roundKilledZombies.text = $"{_statistics.RoundDeathZombiesCount}";
    }

}
