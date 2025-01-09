using System.Collections;
using UnityEngine;

public class CharacterHealth : AliveObject
{
    private Collider2D _collider;
    private HUD _hud;


    private void OnEnable()
    {
        _collider = GetComponent<Collider2D>();
        _hud = ServiceLocator.Current.Get<HUD>();
        _hud.UpdateHealthImage(Health);
        _hud.ShowHealthImage();

    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _hud.UpdateHealthImage(Health);
    }

    public void TakeHeal(int healPoint)
    {
        Health += healPoint;
    }


    protected override void Die()
    {
        Destroy(gameObject);
        Instantiate(_aliveObjectConfig.DeadBodyPrefab, transform.position, transform.rotation);
        _eventManager.OnStopGame?.Invoke();
    }

    public void InvulnerabilityOn()
    {
        StartCoroutine(ActivateInvulnerability());
    }

    private IEnumerator ActivateInvulnerability()
    {
        _collider.isTrigger = true;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(2f);
        _collider.isTrigger = false;
        _spriteRenderer.color = Color.white;
    }
}
