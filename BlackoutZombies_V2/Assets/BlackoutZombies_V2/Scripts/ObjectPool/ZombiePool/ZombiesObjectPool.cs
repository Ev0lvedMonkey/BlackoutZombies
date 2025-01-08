using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombiesObjectPool : MonoBehaviour, IService
{
    [SerializeField] private List<ZombieMover> _prefabs;
    [SerializeField] private List<SpawnDot> _spawnPoints = new();

    private ObjectPool<ZombieMover> _pool;

    public void Init()
    {
        if (_prefabs == null || _prefabs.Count == 0)
        {
            Debug.LogError("Prefabs list is null");
            return;
        }
        _pool = new ObjectPool<ZombieMover>(_prefabs[0], 7, 25, gameObject.transform);
    }

    public void Spawn()
    {
        List<SpawnDot> activeSpawnPoints = _spawnPoints.Where(sp => sp.IsActive).ToList();
        if (activeSpawnPoints == null || activeSpawnPoints.Count == 0)
        {
            Debug.LogWarning("No active spawn points available.");
            return;
        }

        var randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];

        if (!_pool.Contains(randomPrefab))
        {
            _pool.AddPrefab(randomPrefab);
        }

        var item = _pool.Get();
        if (item == null)
        {
            Debug.LogWarning("No available objects in the pool.");
            return;
        }

        var randomSpawnPoint = activeSpawnPoints[Random.Range(0, activeSpawnPoints.Count)];
        item.transform.SetPositionAndRotation(randomSpawnPoint.transform.position, Quaternion.identity);
    }

    public void Dispose(ZombieMover obj)
    {
        _pool.Realease(obj);
    }
}
