using UnityEngine;

public class ObjectPointer : MonoBehaviour
{
    [SerializeField] private Transform target3D;      // Objeto de referência
    [SerializeField] private Transform pointerObject; // Objeto que aponta
    [SerializeField] private float maxDistance = 10f; // Distância máxima para apontar

    void Update()
    {
        if (target3D == null || pointerObject == null) return;

        float distance = Vector3.Distance(gameObject.transform.position, target3D.position);

        if (distance > maxDistance)
        {
            Vector3 direction = (target3D.position - pointerObject.position).normalized;
            pointerObject.rotation = Quaternion.LookRotation(direction);
            pointerObject.gameObject.SetActive(true);
        }
        else
        {
            pointerObject.gameObject.SetActive(false);
        }
    }
}
