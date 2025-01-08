using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class AliveObject : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] protected AliveObjectConfig _aliveObjectConfig;

    [Header("Components")]
    [SerializeField] protected SpriteRenderer _spriteRenderer;

    private int _health;
    protected EventManager _eventManager;


    protected int Health
    {
        get => _health;
        set =>
            _health = Mathf.Clamp(value, 0, _aliveObjectConfig.MaxHealthPoint);
    }

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        _health = _aliveObjectConfig.MaxHealthPoint;
        _spriteRenderer.sprite = _aliveObjectConfig.AliveSprite;
        _eventManager = ServiceLocator.Current.Get<EventManager>();
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }

    protected abstract void Die();

}
