using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ResourceLoaderService _resourceLoader;
    [SerializeField] private CanvasService _canvasService;
    [SerializeField] private KillZombiesCountStorage _exampleStorage;
    [SerializeField] private ZombiesSpawner _zombiesSpawner;
    [SerializeField] private CameraTargetTracker _cameraTargetTracker;

    [Header("Object Pools")]
    [SerializeField] private ZombiesObjectPool _zombieObjectPool;

    [Header("Light Mode")]
    [SerializeField] private bool _isLightMode;

    private SelectGunUI _selectGunUI;
    private StatisticsManager _statisticsManager;
    private EventManager _eventManager;
    private ZombieKillStatistics _storage;
    private IStorageService _storageService;

    private void Awake()
    {
        RegisterServices();
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
        _statisticsManager = new(_storage, _storageService, _eventManager);

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
        if (_isLightMode)
            _resourceLoader.LoadResource<LightMaterialFloorResource>(null);
        else
            _resourceLoader.LoadResource<BaseFloorResource>(null);
        _selectGunUI = ServiceLocator.Current.Get<ResourceLoaderService>().LoadResource<SelectGunUI>(_canvasService.transform);
    }

    private void InitializeComponents()
    {
        _selectGunUI.Init();
        _exampleStorage.Init();
        _eventManager.OnStartGame += _zombieObjectPool.Init;
        _zombiesSpawner.Init(_eventManager, _zombieObjectPool);
        _cameraTargetTracker.Init();
    }
}
