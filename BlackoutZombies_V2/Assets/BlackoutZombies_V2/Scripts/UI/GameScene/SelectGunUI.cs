using System;
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

    public void Init()
    {
        _selectGunScriptableObject.InitializeCharacterDictionary();
        _rightButton.onClick.AddListener(() => SwitchTheGun(true));
        _leftButton.onClick.AddListener(() => SwitchTheGun(false));
        _playButton.onClick.AddListener(() => SelectGun());
        _gunIcon.sprite = _selectGunScriptableObject.SelectedGunImage;
    }

    public void Show() =>
        gameObject.SetActive(true);

    public void Hide() =>
        gameObject.SetActive(false);

    private void SelectGun()
    {
        _selectGunScriptableObject.SelectCurrentGun();
        Debug.Log($"Select gun");
        Hide();
    }

    private void SwitchTheGun(bool isSwitchNextGun)
    {
        if (isSwitchNextGun)
            _selectGunScriptableObject.SwitchToNextSprite();
        else
            _selectGunScriptableObject.SwitchToPreviousSprite();
        _gunIcon.sprite = _selectGunScriptableObject.SelectedGunImage;
    }
}
