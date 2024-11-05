using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DetectCollision myCollisionDetector;


    private void OnEnable()
    {
        myCollisionDetector.OnDetectedCollision += CollisionChecker;
    }

    private void OnDisable()
    {
        myCollisionDetector.OnDetectedCollision -= CollisionChecker;
    }

    void CollisionChecker(GameObject who)
    {
        if(who.GetComponent<Punch>() != null)
        {
            Debug.Log("ACERTOU PUNCH");
            gameObject.SetActive(false);
        }

    }
}

    
