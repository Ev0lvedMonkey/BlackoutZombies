using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ObjectPool<T> where T : MonoBehaviour
{
    private readonly List<T> _prefabs;
    private readonly List<T> _objects;

    private readonly int _maxCount; 
    private Transform _parent;

    internal ObjectPool(T initialPrefab, int prewarmObjects, int maxCount, Transform parent = null)
    {
        _prefabs = new List<T> { initialPrefab };
        _objects = new List<T>();
        _parent = parent;
        _maxCount = maxCount;

        for (int i = 0; i < prewarmObjects; i++)
        {
            var obj = GameObject.Instantiate(initialPrefab, _parent);
            obj.gameObject.SetActive(false);
            _objects.Add(obj);
        }
    }

    internal void AddPrefab(T prefab)
    {
        if (!_prefabs.Contains(prefab))
        {
            _prefabs.Add(prefab);
        }
    }

    internal bool Contains(T prefab) => _prefabs.Contains(prefab);

    internal T Get()
    {
        var obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

        if (obj == null)
        {
            if (_objects.Count < _maxCount)
            {
                var randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];
                obj = Create(randomPrefab);
            }
            else
            {
                Debug.LogWarning("ObjectPool: Maximum object limit reached.");
                return null; 
            }
        }

        obj.gameObject.SetActive(true);
        return obj;
    }

    private T Create(T prefab)
    {
        var obj = GameObject.Instantiate(prefab, _parent);
        _objects.Add(obj);
        return obj;
    }

    internal void Realease(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}
