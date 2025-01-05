using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZombiesObjectPool : MonoBehaviour, IService
{
    [SerializeField] private List<ZombieMover> _prefabs;
    [SerializeField] private List<Transform> _spawnPoints;

    private ObjectPool<ZombieMover> _pool;
    private EventManager _eventManager;

    //TODO вынести переменный спавн 
    private Coroutine _coroutine;
    private const int MinSpawnZombiesCount = 2;
    private const int MaxSpawnZombiesCount = 6;

    public void Init()
    {
        if (_prefabs == null || _prefabs.Count == 0)
        {
            Debug.LogError("");
            return;
        }
        _eventManager = ServiceLocator.Current.Get<EventManager>();
        _pool = new ObjectPool<ZombieMover>(_prefabs[0], 5, 15, gameObject.transform);
        _eventManager.OnStartGame += StartSpawn;
        _eventManager.OnResumeGame += StartSpawn;
        _eventManager.OnStopGame += StopSpawn;
        _eventManager.OnPauseGame += StopSpawn;
        Debug.Log($"Inited");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            StartSpawn();
        if (Input.GetKeyDown(KeyCode.Y))
            StopSpawn();
    }


    public void StartSpawn()
    {
        Debug.Log($"Coroutine ");
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CircleSpawn());
            Debug.Log($"Coroutine started");
        }
        else
            Debug.Log($"Coroutine DONT started");
    }

    public void StopSpawn()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            Debug.Log($"Coroutine stoped");
            _coroutine = null;
        }
        else
            Debug.Log($"Coroutine cant stoped, his none");
    }


    private IEnumerator CircleSpawn()
    {
        while (true)
        {
            int randomZombiesCount = Random.Range(MinSpawnZombiesCount, MaxSpawnZombiesCount);
            for (int i = 0; i < randomZombiesCount; i++)
                Spawn();
            yield return new WaitForSeconds(6f);
        }
    }

    private void Spawn()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0)
        {
            Debug.LogError("Список точек спавна пуст. Добавьте хотя бы одну точку.");
            return;
        }

        var randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];

        if (!_pool.Contains(randomPrefab))
        {
            _pool.AddPrefab(randomPrefab);
        }

        var item = _pool.Get();

        var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count - 1)];

        item.transform.SetPositionAndRotation(randomSpawnPoint.position, Quaternion.identity);
        Debug.Log($"Spawned {item.name} at {randomSpawnPoint.position}");
    }

    public void Dispose(ZombieMover obj)
    {
        _pool.Realease(obj);
    }
}
