using Unity.VisualScripting;
using UnityEngine;

public class CharacterMover : MonoBehaviour, IMoverBlocker, IService
{
    [SerializeField] private float _movementSpeed;

    private Vector2 _moveVector;
    private Vector2 _mousePosition;
    private bool _isMoving;
    private EventManager _eventManager;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private void Awake()
    {
        ServiceLocator.Current.Register<CharacterMover>(this);
        _eventManager = ServiceLocator.Current.Get<EventManager>();
        _eventManager.OnStartGame += OpenMove;
        _eventManager.OnResumeGame += OpenMove;
        _eventManager.OnStopGame += BlockMove;
        _eventManager.OnStopGame += BlockMove;
    }

    private void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (_isMoving == false)
            return;
        _moveVector.x = Input.GetAxisRaw(Horizontal);
        _moveVector.y = Input.GetAxisRaw(Vertical);
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = (Vector3)_mousePosition - transform.position;
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, angle - 90);
        transform.position += (Vector3)_moveVector.normalized * _movementSpeed * Time.deltaTime;
    }

    public void BlockMove() =>
        _isMoving = false;

    public void OpenMove() =>
        _isMoving = true;
}
