using UnityEngine;

[RequireComponent(typeof(CharacterLightZone))]
public class CharacterManager : MonoBehaviour, IService
{
    [Header("Movemnt Propeties")]
    [SerializeField] private float _movementSpeed;

    [Header("Light Zone Components")]
    [SerializeField] private Light _lightSource;
    [SerializeField] private CharacterLightZone _characterLightZone;

    [Header("Health Components")]
    [SerializeField] private CharacterHealth _characterHealth;

    private EventManager _eventManager;
    private CharacterMover _characterMover;

    private void Awake()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        _eventManager = ServiceLocator.Current.Get<EventManager>();
        ServiceLocator.Current.Register<CharacterManager>(this);
        _characterMover = new(_eventManager, transform, _movementSpeed);
        _characterMover.Init();
        _characterLightZone.Init(_lightSource, _eventManager);
        _characterHealth.Init(_eventManager);
    }


    private void Update()
    {
        _characterMover.Move();
        _characterLightZone.UpdateLightRange();
    }
}