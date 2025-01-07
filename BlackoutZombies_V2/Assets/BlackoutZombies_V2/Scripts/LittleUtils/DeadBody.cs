using UnityEngine;

public class DeadBody : MonoBehaviour
{
    [SerializeField] private bool _isCharacter;

    private const float DisapperanceTime = 12f;

    private void Start()
    {
        if (_isCharacter)
            return;
        Destroy(this.gameObject, DisapperanceTime);    
    }
}
