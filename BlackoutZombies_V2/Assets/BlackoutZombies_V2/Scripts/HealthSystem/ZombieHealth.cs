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
        ZombiesObjectPool zombiesObjectPool = ServiceLocator.Current.Get<ZombiesObjectPool>();
        zombiesObjectPool.Dispose(_zombieMover);
    }
}
