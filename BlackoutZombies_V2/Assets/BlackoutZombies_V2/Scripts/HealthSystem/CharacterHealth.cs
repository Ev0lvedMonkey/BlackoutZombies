using System.Collections;
using UnityEngine;

public class CharacterHealth : AliveObject
{
    private Collider2D _collider;

    private void OnEnable()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void TakeHeal(int healPoint)
    {
        Health += healPoint;
    }


    protected override void Die()
    {
        Instantiate(_aliveObjectConfig.DeadBodyPrefab, transform.position, transform.rotation);
        _eventManager.OnStopGame?.Invoke();
        Destroy(gameObject);
    }

    public void InvulnerabilityOn()
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
