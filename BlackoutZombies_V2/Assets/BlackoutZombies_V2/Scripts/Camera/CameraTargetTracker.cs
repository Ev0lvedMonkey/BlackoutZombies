using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraTargetTracker : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    private EventManager _eventManager;

    private void OnValidate()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }
 
    public void Init()
    {
        _eventManager = ServiceLocator.Current.Get<EventManager>();
        _eventManager.OnStartGame += FindTarget;
    }

    private void FindTarget()
    {
        Transform targetTransform = ServiceLocator.Current.Get<CharacterManager>().transform;
        _camera.Follow = targetTransform;
        _camera.LookAt = targetTransform;
    }
}
