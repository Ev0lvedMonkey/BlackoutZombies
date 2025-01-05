using UnityEngine;

public class ZombieMover : MonoBehaviour, IMoverBlocker
{
    [SerializeField] private Transform _playerePosition;

    private float _rotationSpeed = 0.25f;
    private float _movementSpeed;
    private bool _isMoving;
    private EventManager _eventManager;

    private const float MinMovementSpeed = 3.4f;
    private const float MaxMovementSpeed = 4.6f;

    private void Awake()
    {
        _eventManager = ServiceLocator.Current.Get<EventManager>();
        _eventManager.OnStartGame += OpenMove;
        _eventManager.OnResumeGame += OpenMove;
        _eventManager.OnStopGame += BlockMove;
        _eventManager.OnStopGame += BlockMove;
    }


    private void OnEnable()
    {
        OpenMove();
        _playerePosition = ServiceLocator.Current.Get<CharacterMover>().transform;
        _movementSpeed = Random.Range(MinMovementSpeed, MaxMovementSpeed);
    }

    private void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (_isMoving == false)
            return;
        Vector2 targetDirection = _playerePosition.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, angle), _rotationSpeed);
        transform.position = Vector3.MoveTowards(transform.position, _playerePosition.position, _movementSpeed * Time.deltaTime);
    }

    public void BlockMove() =>
        _isMoving = false;

    public void OpenMove() =>
        _isMoving = true;
}
