using UnityEngine;

[RequireComponent(typeof(ZombieMover))]
public class ZombieHealht : AliveObject
{

    [SerializeField] private ZombieMover _zombieMover;

    private ZombiesObjectPool _zombiesObjectPool;

    private void OnValidate()
    {
        _zombieMover = GetComponent<ZombieMover>();
    }

    protected override void Die()
    {
        _zombiesObjectPool = ServiceLocator.Current.Get<ZombiesObjectPool>();
        _zombiesObjectPool.Dispose(_zombieMover);
        Instantiate(_aliveObjectConfig.DeadBodyPrefab, transform.position, transform.rotation);
        _eventManager.OnScoreIncremented?.Invoke();
        _eventManager.OnKilledZombiesIncremented?.Invoke();
    }
}
