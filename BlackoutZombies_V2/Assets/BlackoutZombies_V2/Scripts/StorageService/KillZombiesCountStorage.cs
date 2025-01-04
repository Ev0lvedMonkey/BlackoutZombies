using UnityEngine;

public class KillZombiesCountStorage : MonoBehaviour
{
    private ZombieKillStatistics _storage;
    private JSonToFileStorageService _storageService;

    public void Init()
    {
        _storage = ServiceLocator.Current.Get<ZombieKillStatistics>();
        Debug.LogWarning($"Getted");
        Debug.LogWarning($"_storage.DeathZombiesCount {_storage.DeathZombiesCount}");
        Debug.LogWarning($"_storage.RoundDeathZombiesCount {_storage.RoundDeathZombiesCount}");
        Debug.LogWarning($"_storage.RoundScore{_storage.RoundScore}");
        Debug.LogWarning($"_storage.BestScore {_storage.BestScore}");
        _storageService = ServiceLocator.Current.Get<JSonToFileStorageService>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _storage.DeathZombiesCount+=10;
            _storageService.Save(ConstantsService.StorageKey, _storage);
            Debug.Log($"Saved");
            Debug.Log($"_storage.DeathZombiesCount {_storage.DeathZombiesCount}");
            Debug.Log($"_storage.RoundDeathZombiesCount {_storage.RoundDeathZombiesCount}");
            Debug.Log($"_storage.RoundScore{_storage.RoundScore}");
            Debug.Log($"_storage.BestScore {_storage.BestScore}");
        }

    }
}
