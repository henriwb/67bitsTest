using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterAnimation myAnimation;
    [SerializeField] private Button punchButton;
    [SerializeField] private Punch myPunch;
    [SerializeField] private StackObjects myStackObjects;
    [SerializeField] private DetectCollision myDetectCollision;
    [SerializeField]private ParticleSystem LevelUpFx;
    public CharacterMovement myMovement;
    private bool isPunching; //not using

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
        
        isPunching = true;
        myAnimation.Punch();
        myAnimation.animator.speed = 2f;
        SoundManager.Instance.PlaySound("punch");
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

    public void LevelUpAnimation()
    {
        LevelUpFx.gameObject.SetActive(true);
    }
}
