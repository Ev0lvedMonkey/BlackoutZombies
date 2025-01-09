using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : ResourcePrefab, IService
{
    [Header("Components")]
    [SerializeField] private Image _healthImage;
    [SerializeField] private Image _gunImage;
    [SerializeField] private TextMeshProUGUI _bulletsCountText;
    [SerializeField] private TextMeshProUGUI _roundScoreTextValue;
    [SerializeField] private TextMeshProUGUI _newBestScoreText;

    [Header("Health Sprites")]
    [SerializeField] private Sprite _healthSprite1;
    [SerializeField] private Sprite _healthSprite2;
    [SerializeField] private Sprite _healthSprite3;

    private void OnEnable()
    {
        HideNewBestScoreText();
        HideRoundScoreTextValue();
        HideBulletsCountText();
        HideGunImage();
        HideHealthImage();
    }


    public void UpdateHealthImage(int health)
    {
        if (health == 3)
            _healthImage.sprite = _healthSprite1;
        if (health == 2)
            _healthImage.sprite = _healthSprite2;
        if (health == 1)
            _healthImage.sprite = _healthSprite3;
    }

    public void UpdateGunImage(Sprite gunImage)
    {
        _gunImage.sprite = gunImage;
    }

    public void UpdateBulletsCount(int bullets)
    {
        _bulletsCountText.text = $"{bullets}";
    }
    
    public void UpdateRoundScore(int score)
    {
        _roundScoreTextValue.text = $"{score}";
    }

    public void HideNewBestScoreText() =>
        _newBestScoreText.gameObject.SetActive(false);

    public void ShowNewBestScoreText() =>
        _newBestScoreText.gameObject.SetActive(true);

    public void HideRoundScoreTextValue() =>
        _roundScoreTextValue.gameObject.SetActive(false);

    public void ShowRoundScoreTextValue() =>
        _roundScoreTextValue.gameObject.SetActive(true);

    public void HideBulletsCountText() =>
        _bulletsCountText.gameObject.SetActive(false);

    public void ShowBulletsCountText() =>
        _bulletsCountText.gameObject.SetActive(true);

    public void HideHealthImage() =>
        _healthImage.gameObject.SetActive(false);

    public void ShowHealthImage() =>
        _healthImage.gameObject.SetActive(true);

    public void HideGunImage() =>
        _gunImage.gameObject.SetActive(false);

    public void ShowGunImage() =>
        _gunImage.gameObject.SetActive(true);

}
