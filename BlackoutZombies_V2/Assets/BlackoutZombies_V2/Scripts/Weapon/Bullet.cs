using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    private const float BulletLifeTime = 1.5f;
    private float BulletSpeed = 20;
    private const int BulletDamage = 100;

    private void OnValidate()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, BulletLifeTime);
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.right * BulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ZombieHealht zombiesHealth))
        {
            Destroy(gameObject);
            zombiesHealth.TakeDamage(BulletDamage);
        }
    }
}
