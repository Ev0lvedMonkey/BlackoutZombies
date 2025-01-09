using UnityEngine;

public class StatisticsManager 
{
    private ZombieKillStatistics _statistics;
    private IStorageService _storageService;
    private EventManager _eventManager;
    private HUD _hud;

    private int _scoreIncrementCalls;

    public StatisticsManager(ZombieKillStatistics statistics, IStorageService storageService, EventManager eventManager)
    {
        _statistics = statistics;
        _storageService = storageService;
        _eventManager = eventManager;
        _hud = ServiceLocator.Current.Get<HUD>();

        _eventManager.OnStartGame += ResetRoundData;
        _eventManager.OnStopGame += SaveStatistics;
        _eventManager.OnPauseGame += SaveStatistics;
        _eventManager.OnScoreIncremented += IncrementRoundScore;
        _eventManager.OnKilledZombiesIncremented += IncrementRoundDeathZombiesCount;
    }

    public void ResetAllStatistics()
    {
        _statistics.BestDeathZombiesCountInRound = 0;
        _statistics.BestScore = 0;
        ResetRoundData();
        SaveStatistics();
        Debug.Log("All statistics reset to default!");
    }

    private void IncrementRoundDeathZombiesCount()
    {
        _statistics.RoundDeathZombiesCount++;
        CheckAndUpdateBestDeaths();
        Debug.Log("IncrementRoundDeathZombiesCount");
    }

    private void IncrementRoundScore()
    {
        _scoreIncrementCalls++;

        int pointsToAdd = CalculatePoints();
        _statistics.RoundScore += pointsToAdd;
        _hud.UpdateRoundScore(_statistics.RoundScore);
        _hud.ShowRoundScoreTextValue();
        CheckAndUpdateBestScore();
        Debug.Log("IncrementRoundScore");
    }

    private int CalculatePoints()
    {
        if (_scoreIncrementCalls <= 20 && _scoreIncrementCalls > 10)
        {
            Debug.Log($"+200");
            return 200;
        }
        else if (_scoreIncrementCalls <= 10)
        {
            Debug.Log($"+100");
            return 100;
        }
        else
        {
            Debug.Log($"+300");
            return 300;
        }
    }

    private void CheckAndUpdateBestDeaths()
    {
        if (_statistics.RoundDeathZombiesCount > _statistics.BestDeathZombiesCountInRound)
        {
            _statistics.BestDeathZombiesCountInRound = _statistics.RoundDeathZombiesCount;
            SaveStatistics();
        }
    }

    private void CheckAndUpdateBestScore()
    {
        if (_statistics.RoundScore > _statistics.BestScore)
        {
            _statistics.BestScore = _statistics.RoundScore;
            SaveStatistics();
            _hud.ShowNewBestScoreText();
        }
    }

    private void SaveStatistics()
    {
        _storageService.Save(ConstantsService.StorageKey, _statistics);
        Debug.Log("Statistics Saved!");
    }

    private void ResetRoundData()
    {
        _statistics.RoundDeathZombiesCount = 0;
        _statistics.RoundScore = 0;
        _scoreIncrementCalls = 0;
    }

}
