using UnityEngine;

public class ZombieMover : MonoBehaviour
{
    [SerializeField] private Transform _playerePosition;

    private float _movementSpeed;
    private bool _canMove;
    private EventManager _eventManager;

    private const float RotationSpeed = 0.25f;
    private const float MinMovementSpeed = 3.4f;
    private const float MaxMovementSpeed = 4.6f;

    private void Awake()
    {
        _eventManager = ServiceLocator.Current.Get<EventManager>();
        _eventManager.OnStartGame += OpenMove;
        _eventManager.OnResumeGame += OpenMove;
        _eventManager.OnStopGame += BlockMove;
        _eventManager.OnPauseGame += BlockMove;
    }


    private void OnEnable()
    {
        OpenMove();
        _playerePosition = ServiceLocator.Current.Get<CharacterManager>().transform;
        _movementSpeed = Random.Range(MinMovementSpeed, MaxMovementSpeed);
    }

    private void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (_canMove == false)
            return;
        Vector2 targetDirection = _playerePosition.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, angle), RotationSpeed);
        transform.position = Vector3.MoveTowards(transform.position, _playerePosition.position, _movementSpeed * Time.deltaTime);
    }

    public void BlockMove() =>
        _canMove = false;

    public void OpenMove() =>
        _canMove = true;
}
