using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public Action<GameObject> OnDetectedCollision;


    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            OnDetectedCollision?.Invoke(other.gameObject);
            //Debug.Log("COLLISION" + other.gameObject.name);
        }
    }
}
