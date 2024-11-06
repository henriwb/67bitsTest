using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterAnimation myAnimation;
    public Button punchButton;
    public Punch myPunch;
    public CharacterMovement myMovement;
    public StackObjects myStackObjects;

    private bool isPunching;

    void Start()
    {
        punchButton.onClick.RemoveAllListeners();
        punchButton.onClick.AddListener(PunchAction);
    }

    private void OnEnable()
    {
        NPC.OnNPCDefeat += DefeatedNPC;
    }

    private void OnDisable()
    {
        NPC.OnNPCDefeat -= DefeatedNPC;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PunchAction();
        }
    }

    private void DefeatedNPC(NPC who)
    {
        if (StageController.instance.CanAddStack())
        {
            myStackObjects.AddToStack(who.gameObject);
            int currentStaclk = myStackObjects.GetCurrentStack();
            StageController.OnStackNumberChanged(currentStaclk);
        }
        else
        {
            StageController.instance.ShowMaxStackMessage();

        }
    }


    private void PunchAction()
    {
        if(isPunching)
        {
            return;
        }
        isPunching = true;
        myAnimation.Punch();
        StartCoroutine(PunchAnimation());
    }

    IEnumerator PunchAnimation()
    {
        myMovement.PauseMovement(true);
        yield return new WaitForSeconds(0.1f);
        myPunch.myCollider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        isPunching = false;
        myPunch.myCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        myMovement.PauseMovement(false);
    }
}
