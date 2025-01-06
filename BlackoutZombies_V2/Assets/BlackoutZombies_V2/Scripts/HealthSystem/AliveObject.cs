using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class AliveObject : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] protected AliveObjectConfig _aliveObjectConfig;

    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int _health;

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
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
        Debug.Log($"Bro {gameObject.name} literaly take damage {damage} and he has HP {Health}");
    }

    protected abstract void Die();

}
