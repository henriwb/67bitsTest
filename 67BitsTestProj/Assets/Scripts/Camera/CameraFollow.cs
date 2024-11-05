using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 0.1f;

    private Vector3 initialOffset;

    void Start()
    {
        initialOffset = transform.position - new Vector3(target.position.x, transform.position.y, target.position.z);
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z) + initialOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed);
    }
}
