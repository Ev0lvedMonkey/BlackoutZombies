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
        _eventManager.OnStopGame += AssignCurrentCameraPointAsTarget;
    }

    private void FindTarget()
    {
        Transform targetTransform = ServiceLocator.Current.Get<CharacterManager>().transform;
        _camera.Follow = targetTransform;
        _camera.LookAt = targetTransform;
    }

    public void AssignCurrentCameraPointAsTarget()
    {
        if (_camera.Follow == null || _camera.LookAt == null)
            return;
        GameObject focusPoint = new("CameraFocusPoint");
        focusPoint.transform.position = _camera.transform.position + _camera.transform.forward; 
        focusPoint.transform.rotation = Quaternion.LookRotation(_camera.transform.forward);

        _camera.Follow = focusPoint.transform;
        _camera.LookAt = focusPoint.transform;
    }
}
