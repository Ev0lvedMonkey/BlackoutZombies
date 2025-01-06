using UnityEngine;

[CreateAssetMenu(fileName = "AliveObjectConfig", menuName = "ScriptableObjects/AliveObjectConfig")]
public class AliveObjectConfig : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField] private Sprite _aliveSprite;

    [Header("Health Properties")]
    [SerializeField, Range(1,3)] private int _maxHealthPoint;

    [Header("Dead body prefab")]
    [SerializeField] private DeadBody _deadBodyPrefab;

    public Sprite AliveSprite => _aliveSprite;

    public int MaxHealthPoint => _maxHealthPoint;

    public DeadBody DeadBodyPrefab => _deadBodyPrefab;
}
