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
    public DetectCollision myDetectCollision;

    private bool isPunching;

    void Start()
    {
        punchButton.onClick.RemoveAllListeners();
        punchButton.onClick.AddListener(PunchAction);
        myDetectCollision.OnDetectedCollision += CheckReceiverEnter;
    }


    private void CheckReceiverEnter(GameObject who)
    {

        if(who.GetComponent<ReceiverTask>()!=null)
        {
            who.GetComponent<ReceiverTask>().ReceiverInit(myStackObjects, this);
        }

        if (who.GetComponent<BuyLevelStation>() != null)
        {
            who.GetComponent<BuyLevelStation>().ReceiverInit(this);
        }

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
        // DEBUG FOR EDITOR TESTS
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
            //myAnimation.animator.Play("punchOk", 0, 0.3f);
            //StopAllCoroutines();
            //StartCoroutine(PunchAnimation());
            //return;

        }
        isPunching = true;
        myAnimation.Punch();
        myAnimation.animator.speed = 2f;
        //myAnimation.animator.Play("punchOk", 0, 0.3f);
        StopAllCoroutines();
        StartCoroutine(PunchAnimation());
    }

    IEnumerator PunchAnimation()
    {
        myMovement.PauseMovement(true);
        yield return new WaitForSeconds(0.1f);
        myPunch.myCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        isPunching = false;
        myPunch.myCollider.enabled = false;
        yield return new WaitForSeconds(0.2f);
        myAnimation.animator.speed = 1f;
        myMovement.PauseMovement(false);
    }
}
