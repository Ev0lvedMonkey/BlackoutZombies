using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    private float RotationSpeed = 2.3f;

    private void Update() =>
        transform.Rotate(0, 0, RotationSpeed);

}
