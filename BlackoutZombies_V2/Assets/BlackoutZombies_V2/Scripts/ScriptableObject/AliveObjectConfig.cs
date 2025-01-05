using UnityEngine;

[CreateAssetMenu(fileName = "AliveObjectConfig", menuName = "ScriptableObjects/AliveObjectConfig")]
public class AliveObjectConfig : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField] private Sprite _aliveSprite;
    [SerializeField] private Sprite _deadSprite;

    [Header("Health Properties")]
    [SerializeField, Range(1,3)] private int _maxHealthPoint;

    public Sprite AliveSprite => _aliveSprite;
    public Sprite DeadSprite => _deadSprite;
    public int MaxHealthPoint => _maxHealthPoint;
}
