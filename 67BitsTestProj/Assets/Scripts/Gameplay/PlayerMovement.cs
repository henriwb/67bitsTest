using System.Collections;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    

    void Start()
    {
        InputController.OnDirectionChanged += UpdateMoveDirection;
    }

    private void OnDisable()
    {
        InputController.OnDirectionChanged -= UpdateMoveDirection;
    }

    
}
