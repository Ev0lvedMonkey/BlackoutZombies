using UnityEngine;

public class KillZombiesCountStorage : MonoBehaviour
{
    private KillZombiesCount _storage;
    private JSonToFileStorageService _storageService;

    private const string Key1 = "Key";
    public void Init()
    {
        _storage = ServiceLocator.Current.Get<KillZombiesCount>();
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
            _storage.DeathZombiesCount++;
            _storage.RoundDeathZombiesCount += 2;
            _storage.RoundScore += 3;
            _storage.BestScore += 4;
            _storageService.Save(Key1, _storage);
            Debug.Log($"Saved");
            Debug.Log($"_storage.DeathZombiesCount {_storage.DeathZombiesCount}");
            Debug.Log($"_storage.RoundDeathZombiesCount {_storage.RoundDeathZombiesCount}");
            Debug.Log($"_storage.RoundScore{_storage.RoundScore}");
            Debug.Log($"_storage.BestScore {_storage.BestScore}");
        }

    }
}
