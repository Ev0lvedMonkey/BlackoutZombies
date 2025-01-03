using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoaderService : MonoBehaviour, IService
{
    [Header("Prefab List")]
    [SerializeField]
    private List<ResourcePrefab> prefabList = new();

    private readonly Dictionary<Type, ResourcePrefab> _prefabsDictionary = new();
    private readonly Dictionary<Type, MonoBehaviour> _loadedResources = new();

    public void InitializePrefabsDictionary()
    {
        _prefabsDictionary.Clear();

        foreach (var prefab in prefabList)
        {
            if (prefab == null)
            {
                Debug.LogWarning("One of the prefabs in the list is null. Skipping.");
                continue;
            }


            var type = prefab.GetType();
            if (!_prefabsDictionary.ContainsKey(type))
            {
                _prefabsDictionary[type] = prefab;
            }
            else
            {
                Debug.LogWarning($"Duplicate prefab for type {type} detected. Skipping.");
            }

        }
        Debug.Log("Prefabs Dictionary Contents:");
        foreach (var entry in _prefabsDictionary)
        {
            Debug.Log($"Type: {entry.Key}, Prefab: {entry.Value}");
        }
    }

    public T LoadResource<T>(Transform resourceParent = null) where T : ResourcePrefab
    {
        var type = typeof(T);

        if (_loadedResources.TryGetValue(type, out var existingResource))
        {
            Debug.LogWarning($"Resource of type {type} is already loaded.");
            return existingResource as T;
        }

        if (!_prefabsDictionary.TryGetValue(type, out var prefab))
        {
            Debug.LogError($"Prefab for type {type} not found in the dictionary.");
            return null;
        }

        var instance = GameObject.Instantiate((MonoBehaviour)prefab, resourceParent);
        var resourceComponent = instance.GetComponent<T>();
        if (resourceComponent == null)
        {
            Debug.LogError($"Loaded prefab does not contain the required component of type {type}.");
            return null;
        }

        _loadedResources[type] = resourceComponent;
        return resourceComponent;
    }

    public void UnloadResource<T>() where T : ResourcePrefab
    {
        var type = typeof(T);

        if (_loadedResources.TryGetValue(type, out var resource))
        {
            GameObject.Destroy(resource.gameObject);
            _loadedResources.Remove(type);
            Debug.Log($"Resource of type {type} has been unloaded.");
        }
        else
        {
            Debug.LogWarning($"No loaded resource found for type {type} to unload.");
        }
    }

    public bool IsResourceLoaded<T>() where T : ResourcePrefab
    {
        return _loadedResources.ContainsKey(typeof(T));
    }
}
