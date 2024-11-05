using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
    private Vector3 moveDirection;
    public float moveSpeed = 5f;
    public GameObject playerModel;

    private bool PauseMove = false;

    public void PauseMovement(bool set)
    {
        PauseMove = set; 
    }

    protected virtual void Update()
    {

        if(PauseMove )
        {
            return;
        }
        // Movimento
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Rotação
        if (moveDirection.sqrMagnitude > 0.001f) // Evita zero magnitude
        {
            playerModel.transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    protected virtual void UpdateMoveDirection(Vector3 fromJoystick)
    {
        moveDirection = fromJoystick.normalized; // Assegura que a direção está normalizada
    }
}
