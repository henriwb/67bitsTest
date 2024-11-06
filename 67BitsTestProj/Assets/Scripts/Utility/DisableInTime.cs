using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInTime : MonoBehaviour
{
    public float time;
    public bool DestroyMe;
    private void OnEnable()
    {
        StartCoroutine(Call());
    }

    public IEnumerator Call()
    {
        yield return new WaitForSeconds(time);
        if (DestroyMe)
        {
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }
}
