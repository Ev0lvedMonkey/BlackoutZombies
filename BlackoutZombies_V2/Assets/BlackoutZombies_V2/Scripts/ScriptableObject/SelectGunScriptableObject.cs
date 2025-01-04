using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectGunSO", menuName = "ScriptableObjects/SelectGunScriptableObject")]
public class SelectGunScriptableObject : ScriptableObject
{
    [SerializeField] private List<Sprite> _gunSprites = new();
    [SerializeField] private List<int> _minScores = new(); 

    public IReadOnlyList<Sprite> GunSprites => _gunSprites;

    public int GetMinScoreForGun(int index)
    {
        if (index < 0 || index >= _minScores.Count)
        {
            Debug.LogWarning($"Invalid index {index}. Returning default score 0.");
            return 0;
        }

        return _minScores[index];
    }
}
