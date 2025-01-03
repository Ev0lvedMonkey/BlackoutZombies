using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectGunSO", menuName = "ScriptableObjects/SelectGunScriptableObject")]
public class SelectGunScriptableObject : ScriptableObject
{
    public Sprite SelectedGunImage
    {
        get
        {
            if (_selectedGunImage == null)
                return _gunSprites[0];
            else
                return _selectedGunImage;
        }
        private set => _selectedGunImage = value;
    }

    [SerializeField] private List<Sprite> _gunSprites = new();

    private int _selectedGunIndex = 0;
    private Sprite _selectedGunImage = null;

    private Dictionary<int, Action> _characterDictionary;

    private int SelectedGunIndex
    {
        get
        {
            if (_selectedGunIndex > _gunSprites.Count - 1)
                _selectedGunIndex = 0;
            if (_selectedGunIndex < 0)
                _selectedGunIndex = _gunSprites.Count - 1;
            return _selectedGunIndex;
        }
        set => _selectedGunIndex = value;
    }

    public void SwitchToNextSprite()
    {
        SelectedGunIndex++;
        Debug.Log($"{SelectedGunIndex}");
        SelectedGunImage = _gunSprites[SelectedGunIndex];
    }

    public void SwitchToPreviousSprite()
    {
        SelectedGunIndex--;
        Debug.Log($"{SelectedGunIndex}");
        SelectedGunImage = _gunSprites[SelectedGunIndex];
    }

    public void SelectCurrentGun()
    {
        if (_characterDictionary.TryGetValue(SelectedGunIndex, out var action))
            action.Invoke();
        else
            Debug.LogWarning($"No action found for key {SelectedGunIndex}.");
    }

    public void InitializeCharacterDictionary()
    {
        ResourceLoaderService resourceLoaderService = ServiceLocator.Current.Get<ResourceLoaderService>();
        _characterDictionary = new Dictionary<int, Action>
        {
            {0, () => resourceLoaderService.LoadResource<PistolShooting>()},
            {1, () => resourceLoaderService.LoadResource<UziShooting>()},
            {2, () => resourceLoaderService.LoadResource<ShotgunShooting>()},
            {3, () => resourceLoaderService.LoadResource <AKShooting>()}
        };
    }
}
