using UnityEngine;

public class CharacterLightZone : MonoBehaviour
{
    private float CurrentLightRange
    {
        get => _currentLightRange;
        set
        {
            _currentLightRange = Mathf.Clamp(value, MinLightRange, ExtraLightRange);
        }
    }

    private Light _lightSource;
    private float _currentLightRange;
    private bool _isNeedToUpdateLight;
    private EventManager _eventManager;

    private const float BatteryExtinctionStep = 0.007f;
    private const float ExtraLightRange = 50;
    private const float MaxLightRange = 17;
    private const float OnStartLightRange = 11;
    private const float MinLightRange = 4;


    public void Init(Light lightSource, EventManager eventManager)
    {
        _lightSource = lightSource;
        _eventManager = eventManager;
        _eventManager.OnStartGame += StartLight;
        _eventManager.OnStopGame += LastLightBattery;
    }

    public void ReloadBattery()
    {
        CurrentLightRange = MaxLightRange;
    }

    public void UpdateLightRange()
    {
        if (!_isNeedToUpdateLight)
            return;
        CurrentLightRange -= BatteryExtinctionStep;
        _lightSource.range = CurrentLightRange;
    }

    private void StartLight()
    {
        _isNeedToUpdateLight = true;
        CurrentLightRange = OnStartLightRange;
        _lightSource.range = CurrentLightRange;
    }

    private void LastLightBattery()
    {
        CurrentLightRange = ExtraLightRange;
        _lightSource.intensity = 1.5f;
    }

}
