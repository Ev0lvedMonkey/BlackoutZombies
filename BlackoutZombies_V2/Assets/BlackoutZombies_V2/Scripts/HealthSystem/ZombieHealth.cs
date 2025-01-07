using UnityEngine;

[RequireComponent(typeof(ZombieMover))]
public class ZombieHealht : AliveObject
{

    [SerializeField] private ZombieMover _zombieMover;

    private void OnValidate()
    {
        _zombieMover = GetComponent<ZombieMover>();
    }

    protected override void Die()
    {
        Instantiate(_aliveObjectConfig.DeadBodyPrefab, transform.position, transform.rotation);
        _eventManager.OnScoreIncremented?.Invoke();
        _eventManager.OnKilledZombiesIncremented?.Invoke();
        ZombiesObjectPool zombiesObjectPool = ServiceLocator.Current.Get<ZombiesObjectPool>();
        zombiesObjectPool.Dispose(_zombieMover);
    }
}
