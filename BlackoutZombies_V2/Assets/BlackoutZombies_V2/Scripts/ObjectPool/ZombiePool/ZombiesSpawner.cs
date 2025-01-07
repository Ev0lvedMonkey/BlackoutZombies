using System.Collections;
using UnityEngine;

public class ZombiesSpawner : MonoBehaviour
{
    private EventManager _eventManager;
    private ZombiesObjectPool _zombiesObjectPool;
    private Coroutine _coroutine;
    private const int MinSpawnZombiesCount = 2;
    private const int MaxSpawnZombiesCount = 6;

    public void Init(EventManager eventManager, ZombiesObjectPool zombiesObjectPool)
    {
        _zombiesObjectPool = zombiesObjectPool;
        _eventManager = eventManager;
        _eventManager.OnStartGame += StartSpawn;
        _eventManager.OnResumeGame += StartSpawn;
        _eventManager.OnStopGame += StopSpawn;
        _eventManager.OnPauseGame += StopSpawn;
    }

    public void StartSpawn()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CircleSpawn());
        }
        else
            Debug.Log($"Coroutine DONT started");
    }

    public void StopSpawn()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
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
                _zombiesObjectPool.Spawn();
            yield return new WaitForSeconds(6f);
        }
    }
}
