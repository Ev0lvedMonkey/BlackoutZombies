using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameBootstrap : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ResourceLoaderService _resourceLoader;
    [SerializeField] private CanvasService _canvasService;
    [SerializeField] private ZombiesSpawner _zombiesSpawner;
    [SerializeField] private CameraTargetTracker _cameraTargetTracker;
    [SerializeField] private KeyboardInputHandler _keyboardInputHandler;

    [Header("Object Pools")]
    [SerializeField] private ZombiesObjectPool _zombieObjectPool;

    [Header("Light Mode")]
    [SerializeField] private bool _isLightMode;

    private SelectGunUI _selectGunUI;
    private HUD _hud;
    private StatisticsManager _statisticsManager;
    private EventManager _eventManager;
    private ZombieKillStatistics _storage;
    private IStorageService _storageService;

    private Dictionary<int, Action> _trapPositionsDictionary;

    private void Awake()
    {
        RegisterServices();
        _trapPositionsDictionary = new()
        {
            {1, ()=> _resourceLoader.LoadResource<TrapPositions1>() },
            {2, ()=> _resourceLoader.LoadResource<TrapPositions2>() },
            {3, ()=> _resourceLoader.LoadResource<TrapPositions3>() }
        };
        InitResources();
        InitializeComponents();
    }

    private void RegisterServices()
    {
        _eventManager = new();
        _storage = new();
        _storageService = new JSonToFileStorageService();

        _storageService.Load<ZombieKillStatistics>(ConstantsService.StorageKey, (storage) =>
            _storage = storage);

        ServiceLocator.Inizialize();
        ServiceLocator.Current.Register(_eventManager);
        ServiceLocator.Current.Register(_resourceLoader);
        ServiceLocator.Current.Register(_canvasService);
        ServiceLocator.Current.Register(_storage);
        ServiceLocator.Current.Register((JSonToFileStorageService)_storageService);
        ServiceLocator.Current.Register(_zombieObjectPool);
    }

    private void InitResources()
    {
        _resourceLoader.InitializePrefabsDictionary();
        _hud = _resourceLoader.LoadResource<HUD>(_canvasService.transform);
        ServiceLocator.Current.Register(_hud);
        _statisticsManager = new(_storage, _storageService, _eventManager);
        if (_isLightMode)
            _resourceLoader.LoadResource<LightMaterialFloorResource>();
        else
            _resourceLoader.LoadResource<BaseFloorResource>();
        _selectGunUI = _resourceLoader.LoadResource<SelectGunUI>(_canvasService.transform);
        _resourceLoader.LoadResource<PickUpObjectResource>();
        _resourceLoader.LoadResource<AroundTraps>();
        int randomTrapsPosition = Random.Range(1, _trapPositionsDictionary.Count + 1);
        _trapPositionsDictionary[randomTrapsPosition].Invoke();

    }

    private void InitializeComponents()
    {
        _selectGunUI.Init();
        _eventManager.OnStartGame += _zombieObjectPool.Init;
        _eventManager.OnStopGame += () => _resourceLoader.UnloadResource<HUD>();
        _eventManager.OnStopGame += () => _resourceLoader.LoadResource<RestartGameUI>(_canvasService.transform);
        _eventManager.OnStartGame += () => _keyboardInputHandler.Init(_eventManager);
        _eventManager.OnPauseGame += () => _resourceLoader.LoadResource<PauseUI>(_canvasService.transform);
        _zombiesSpawner.Init(_eventManager, _zombieObjectPool);
        _cameraTargetTracker.Init();
    }
}
