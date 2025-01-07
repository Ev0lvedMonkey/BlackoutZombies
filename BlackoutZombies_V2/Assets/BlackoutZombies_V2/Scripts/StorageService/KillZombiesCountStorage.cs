using UnityEngine;

public class KillZombiesCountStorage : MonoBehaviour
{
    private ZombieKillStatistics _storage;
    private JSonToFileStorageService _storageService;
    private EventManager _eventManager;

    public void Init()
    {
        _storage = ServiceLocator.Current.Get<ZombieKillStatistics>();
        _storageService = ServiceLocator.Current.Get<JSonToFileStorageService>();
        _eventManager = ServiceLocator.Current.Get<EventManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _storage.BestDeathZombiesCountInRound+=10;
            _storageService.Save(ConstantsService.StorageKey, _storage);
            Debug.Log($"Saved");
            Debug.Log($"_storage.DeathZombiesCount {_storage.BestDeathZombiesCountInRound}");
            Debug.Log($"_storage.RoundDeathZombiesCount {_storage.RoundDeathZombiesCount}");
            Debug.Log($"_storage.RoundScore{_storage.RoundScore}");
            Debug.Log($"_storage.BestScore {_storage.BestScore}");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _eventManager.OnStartGame.Invoke();
            Debug.Log($"Number 1 OnStartGame");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _eventManager.OnStopGame.Invoke();
            Debug.Log($"Number 2 OnStopGame");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _eventManager.OnResumeGame.Invoke();
            Debug.Log($"Number 3 OnResumeGame");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _eventManager.OnPauseGame.Invoke();
            Debug.Log($"Number 4 OnPauseGame");
        }

    }
}
