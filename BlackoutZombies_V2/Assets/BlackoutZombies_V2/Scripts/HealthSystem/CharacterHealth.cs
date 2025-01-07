using System.Collections;
using UnityEngine;

public class CharacterHealth : AliveObject
{
    private Collider2D _collider;

    private void OnEnable()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out ZombieHealht zombie))
        {
            InvulnerabilityOn();
            TakeDamage(1);
        }

    }

    public void TakeHeal(int healPoint)
    {
        Health += healPoint;
        Debug.Log($"{gameObject.name} take heal {healPoint}, bros health now {Health}");
    }


    protected override void Die()
    {
        Instantiate(_aliveObjectConfig.DeadBodyPrefab, transform.position, transform.rotation);
        _eventManager.OnStopGame?.Invoke();
        Destroy(gameObject);
    }

    private void InvulnerabilityOn()
    {
        StartCoroutine(ActivateInvulnerability());
    }

    private IEnumerator ActivateInvulnerability()
    {
        _collider.isTrigger = true;
        yield return new WaitForSeconds(2f);
        _collider.isTrigger = false;
        yield return null;
    }
}
