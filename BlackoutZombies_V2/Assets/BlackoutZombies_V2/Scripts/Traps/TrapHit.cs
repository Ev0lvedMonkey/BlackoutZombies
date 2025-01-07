using UnityEngine;

   [RequireComponent(typeof(Collider2D))]
public class TrapHit : MonoBehaviour
{
    private enum TrapType
    {
        Zombie = 1,
        InstanllyFatal = 100
    }

    [SerializeField] private TrapType _type;

    private int _trapDamage;

    private void OnEnable()
    {
        _trapDamage = (int)_type;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out CharacterHealth character))
        {
            character.InvulnerabilityOn();
            character.TakeDamage(_trapDamage);
        }

    }
}
