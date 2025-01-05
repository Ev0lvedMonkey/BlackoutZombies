using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class AliveObject : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private AliveObjectConfig _aliveObjectConfig;

    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int _health;

    protected int Health
    {
        get
        {
            if (_health > _aliveObjectConfig.MaxHealthPoint)
                _health = _aliveObjectConfig.MaxHealthPoint;
            if (_health <= 0)
            {
                _health = 0;
                Die();
            }
            return _health;
        }
        set => _health = value;
    }

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        _health = _aliveObjectConfig.MaxHealthPoint;
        _spriteRenderer.sprite = _aliveObjectConfig.AliveSprite;
    }


    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{gameObject.name} take damage {damage}, bros health now {Health}");
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} die");
        _spriteRenderer.sprite = _aliveObjectConfig.DeadSprite;
    }

}
