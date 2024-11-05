using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    void Start()
    {
        InputController.OnUsingJoystick += CheckJoystickChanged;
    }

    private void OnDisable()
    {
        InputController.OnUsingJoystick -= CheckJoystickChanged;
    }


    public void CheckJoystickChanged(bool usingJoystick)
    {
        animator.SetBool("running", usingJoystick);
    }

    public void Punch()
    {
        animator.SetTrigger("punch");
    }
}
