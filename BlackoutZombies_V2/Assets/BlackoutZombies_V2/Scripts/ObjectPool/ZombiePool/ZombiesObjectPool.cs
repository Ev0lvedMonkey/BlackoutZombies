using System.Collections.Generic;
using UnityEngine;

public class ZombiesObjectPool : MonoBehaviour, IService
{
    [SerializeField] private List<ZombieMover> _prefabs;
    [SerializeField] private List<Transform> _spawnPoints;

    private ObjectPool<ZombieMover> _pool;

    public void Init()
    {
        if (_prefabs == null || _prefabs.Count == 0)
        {
            Debug.LogError("Prefabs list is null");
            return;
        }
       _pool = new ObjectPool<ZombieMover>(_prefabs[0], 5, 15, gameObject.transform);
    }

    public void Spawn()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0)
        {
            Debug.LogError("Spawn list is null");
            return;
        }

        var randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];

        if (!_pool.Contains(randomPrefab))
        {
            _pool.AddPrefab(randomPrefab);
        }

        var item = _pool.Get();

        var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count - 1)];

        item?.transform.SetPositionAndRotation(randomSpawnPoint.position, Quaternion.identity);
    }

    public void Dispose(ZombieMover obj)
    {
        _pool.Realease(obj);
    }
}
