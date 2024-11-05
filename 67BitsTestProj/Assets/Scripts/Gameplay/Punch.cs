using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider myCollider;
    private void Start()
    {
        myCollider.enabled = false;
    }
}
