using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private ResourceLoaderService _resourceLoader;
    [SerializeField] private CanvasService _canvasService;
    [SerializeField] private KillZombiesCountStorage _exampleStorage;

    private SelectGunUI _selectGunUI;
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
        _storage = new();
        _storageService = new JSonToFileStorageService();

        _storageService.Load<ZombieKillStatistics>(ConstantsService.StorageKey, (storage) => { _storage = storage;
            Debug.Log($"LOADED");});

        ServiceLocator.Inizialize();
        ServiceLocator.Current.Register(_resourceLoader);
        ServiceLocator.Current.Register(_canvasService);
        ServiceLocator.Current.Register(_storage);
        ServiceLocator.Current.Register((JSonToFileStorageService)_storageService);
    }

    private void InitResources()
    {
        _resourceLoader.InitializePrefabsDictionary();
        _selectGunUI = ServiceLocator.Current.Get<ResourceLoaderService>().LoadResource<SelectGunUI>(_canvasService.transform);
    }

    private void InitializeComponents()
    {
        _selectGunUI.Init();
        _exampleStorage.Init();
    }

}
