using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Vector2 _moveVector;
    private Vector2 _mousePosition;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        _moveVector.x = Input.GetAxisRaw(Horizontal);
        _moveVector.y = Input.GetAxisRaw(Vertical);
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = (Vector3)_mousePosition - transform.position;
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, angle-90);
        transform.position += (Vector3)_moveVector.normalized * _movementSpeed * Time.deltaTime;
    }
}
