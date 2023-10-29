using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the object continuously
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
