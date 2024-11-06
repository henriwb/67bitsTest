using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DetectCollision myCollisionDetector;
    public CharacterMovement CharacterMovement;
    public Collider myCollider;
    public Animator myAnimator;
    public static Action<NPC> OnNPCDefeat;
    public ParticleSystem myParticlehit;

    private bool Defeated;

    private void OnEnable()
    {
        Defeated = false;
        CharacterMovement.PauseMovement(false);
        myCollisionDetector.OnDetectedCollision += CollisionChecker;
    }

    private void OnDisable()
    {
        myCollisionDetector.OnDetectedCollision -= CollisionChecker;
    }

    void CollisionChecker(GameObject who)
    {
        if(who.GetComponent<Punch>() != null && !Defeated)
        {
            //Debug.Log("ACERTOU PUNCH");
            //gameObject.SetActive(false);
            myParticlehit.gameObject.SetActive(true);
            transform.localScale *= 0.5f;
            myCollider.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            myAnimator.SetTrigger("fall");
            CharacterMovement.StopAllCoroutines();
            OnNPCDefeat?.Invoke(this);
            Defeated = true;
            CharacterMovement.PauseMovement(true);
        }

    }
}

    
