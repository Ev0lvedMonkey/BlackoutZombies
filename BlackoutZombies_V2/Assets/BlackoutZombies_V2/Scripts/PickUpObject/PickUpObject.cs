using System.Data.Common;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PickUpObject : MonoBehaviour
{
    [SerializeField] private Collider2D _spawnArea;

    private void OnValidate()
    {
        if (transform.parent == null)
            return;
        if(transform.parent.TryGetComponent(out Collider2D collider))
            _spawnArea = collider;
    }

    private void OnEnable()
    {
        RespawnObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterManager character))
        {
            ReactToCollision(collision);
            RespawnObject();
        }
    }

    protected abstract void ReactToCollision(Collider2D collider);
    
    private void RespawnObject()
    {
        transform.position =
            new Vector3(Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x),
            Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y), 0);
    }

}
