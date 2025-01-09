using TMPro;
using UnityEngine;

public class MenuUI : ResourcePrefab
{
    [SerializeField] private TextMeshProUGUI _bestRoundScoreTextValue;
    [SerializeField] private TextMeshProUGUI _bestRoundKilledZombies;

    private ZombieKillStatistics _storage;

    public void Init(ZombieKillStatistics storage)
    {
        _storage = storage;
        _bestRoundScoreTextValue.text = $"{_storage.BestScore}";
        _bestRoundKilledZombies.text = $"{_storage.BestDeathZombiesCountInRound}"; 
    }

    private void Update()
    {
        if (Input.anyKeyDown)
            SceneLoader.LoadScene(Scenes.GameScene);
    }
}
