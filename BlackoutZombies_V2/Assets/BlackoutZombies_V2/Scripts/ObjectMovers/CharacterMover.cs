using UnityEngine;

public class CharacterMover
{
    private Transform _transform;
    private Vector2 _moveVector;
    private Vector2 _mousePosition;
    private float _movementSpeed;
    private bool _canMove;
    private EventManager _eventManager;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public CharacterMover(EventManager eventManager, Transform transform, float movementSpeed)
    {
        _eventManager = eventManager;
        _transform = transform;
        _movementSpeed = movementSpeed;
    }

    public void Init()
    {
        _eventManager.OnStartGame += OpenMove;
        _eventManager.OnResumeGame += OpenMove;
        _eventManager.OnStopGame += BlockMove;
        _eventManager.OnPauseGame += BlockMove;
    }


    public void Move()
    {
        if (_canMove == false)
            return;
        _moveVector.x = Input.GetAxisRaw(Horizontal);
        _moveVector.y = Input.GetAxisRaw(Vertical);
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = (Vector3)_mousePosition - _transform.position;
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        _transform.localRotation = Quaternion.Euler(0, 0, angle - 90);
        _transform.position += (Vector3)_moveVector.normalized * _movementSpeed * Time.deltaTime;
    }

    private void BlockMove() =>
        _canMove = false;

    private void OpenMove() =>
        _canMove = true;
}
