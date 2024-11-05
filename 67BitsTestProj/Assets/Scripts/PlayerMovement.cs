using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveDirection;
    public float moveSpeed = 5f;
    public GameObject playerModel;

    void Start()
    {
        InputController.OnDirectionChanged += UpdateMoveDirection;
    }

    private void OnDisable()
    {
        InputController.OnDirectionChanged -= UpdateMoveDirection;
    }

    void Update()
    {
        // Movimento
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Rotação
        if (moveDirection.sqrMagnitude > 0.001f) // Evita zero magnitude
        {
            playerModel.transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    public void UpdateMoveDirection(Vector3 fromJoystick)
    {
        moveDirection = fromJoystick.normalized; // Assegura que a direção está normalizada
    }
}
