using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private ResourceLoaderService _resourceLoader;
    [SerializeField] private CanvasService _canvasService;

    private SelectGunUI _selectGunUI;

    private void Awake()
    {
        _resourceLoader.InitializePrefabsDictionary();
        RegisterServices();
        LoadResources();
        InitializeComponents();
    }

    private void LoadResources()
    {
        _selectGunUI = ServiceLocator.Current.Get<ResourceLoaderService>().LoadResource<SelectGunUI>(_canvasService.transform);
    }

    private void InitializeComponents()
    {
        _selectGunUI.Init();
    }

    private void RegisterServices()
    {
        ServiceLocator.Inizialize();
        ServiceLocator.Current.Register(_resourceLoader);
        ServiceLocator.Current.Register(_canvasService);
    }
}
