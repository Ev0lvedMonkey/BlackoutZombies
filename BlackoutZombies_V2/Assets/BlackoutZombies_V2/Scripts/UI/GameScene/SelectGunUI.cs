using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectGunUI : ResourcePrefab
{
    [Header("Configuration")]
    [SerializeField] private SelectGunScriptableObject _selectGunScriptableObject;

    [Header("Components")]
    [SerializeField] private Image _gunIcon;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _currentGunIndex = 0;
    private int _playerScore = 0;
    private ZombieKillStatistics _storage;
    private ResourceLoaderService _resourceLoaderService;
    private Dictionary<int, Action> _characterDictionary;

    public void Init()
    {
        UpdateGunIcon();
        _resourceLoaderService = ServiceLocator.Current.Get<ResourceLoaderService>();
        _storage = ServiceLocator.Current.Get<ZombieKillStatistics>();
        _characterDictionary = new Dictionary<int, Action>
        {
            {0, () => _resourceLoaderService.LoadResource<PistolShooting>()},
            {1, () => _resourceLoaderService.LoadResource<UziShooting>()},
            {2, () => _resourceLoaderService.LoadResource<ShotgunShooting>()},
            {3, () => _resourceLoaderService.LoadResource<AKShooting>()}
        };
        _rightButton.onClick.AddListener(() => SwitchGun(true));
        _leftButton.onClick.AddListener(() => SwitchGun(false));
        _playButton.onClick.AddListener(SelectGun);
        UpdatePlayButtonState();
    }

    private void SwitchGun(bool isNext)
    {
        _currentGunIndex = isNext
            ? (_currentGunIndex + 1) % _selectGunScriptableObject.GunSprites.Count
            : (_currentGunIndex - 1 + _selectGunScriptableObject.GunSprites.Count) % _selectGunScriptableObject.GunSprites.Count;

        UpdateGunIcon();
        UpdatePlayButtonState();
    }

    private void UpdateGunIcon()
    {
        _gunIcon.sprite = _selectGunScriptableObject.GunSprites[_currentGunIndex];
    }

    private void SelectGun()
    {
        Debug.Log($"Selected gun index: {_currentGunIndex}");
        _characterDictionary[_currentGunIndex].Invoke();
        Hide();
    }

    private void UpdatePlayButtonState()
    {
        int requiredScore = _selectGunScriptableObject.GetMinScoreForGun(_currentGunIndex);
        Debug.Log($"STORAGE.DeathZombiesCount  {_storage.DeathZombiesCount}");
        bool canPlay = _storage.DeathZombiesCount >= requiredScore;

        _playButton.gameObject.SetActive(canPlay);
        _scoreText.gameObject.SetActive(!canPlay);

        if (!canPlay)
        {
            _scoreText.text = $"Недостаточно очков для этого оружия ({_playerScore}/{requiredScore})!";
        }
    }

    public void Show() =>
        gameObject.SetActive(true);

    public void Hide() =>
        gameObject.SetActive(false);
}
