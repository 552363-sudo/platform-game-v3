using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The rolling object
    public Vector3 offset;    // Offset from the object

    void LateUpdate()
    {
        // Update camera position to follow the target plus offset
        transform.position = target.position + offset;

        // Optional: keep the camera looking at the target
        transform.LookAt(target);

        // This way, the camera doesn’t inherit target rotation, only position.
    }
}