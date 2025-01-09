using System;
using UnityEngine;

public class MenuBootstrap : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ResourceLoaderService _resourceLoader;

    private MenuUI _menuUI;
    private RoadMover _roadMover;
    private ZombieKillStatistics _storage;
    private IStorageService _storageService;

    private void Awake()
    {
        RegisterResources();
        LoadRerouses();
        InitResources();
    }


    private void RegisterResources()
    {
        _resourceLoader.InitializePrefabsDictionary();
        _storage = new();
        _storageService = new JSonToFileStorageService();

        _storageService.Load<ZombieKillStatistics>(ConstantsService.StorageKey, (storage) =>
            _storage = storage);
    }
    private void LoadRerouses()
    {
        _resourceLoader.LoadResource<LightMaterialFloorResource>();
        RoadPath path = _resourceLoader.LoadResource<RoadPath>();
        _roadMover = _resourceLoader.LoadResource<RoadMover>();
        _roadMover.SetRoadPath(path);
        _menuUI = _resourceLoader.LoadResource<MenuUI>();
    }
    private void InitResources()
    {
        _menuUI.Init(_storage);
    }
}
