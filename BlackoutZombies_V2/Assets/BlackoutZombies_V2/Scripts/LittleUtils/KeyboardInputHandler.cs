using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour
{
    private EventManager _eventManager;
    private bool _isActive;

    private void Update()
    {
        if (_isActive == false)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
            _eventManager.OnPauseGame.Invoke();
    }

    public void Init(EventManager eventManager)
    {
        _eventManager = eventManager;
        _isActive = true;
    }
}
