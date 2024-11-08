using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public string poolTag;
    public float interval;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(CreateNPCRoutine());
    }

    IEnumerator CreateNPCRoutine()
    {

        while (true)
        {
            yield return new WaitForSeconds( interval);
            GameObject created = ObjectPoolManager.Instance.SpawnFromPool(poolTag, gameObject.transform.position, Quaternion.identity);
            created.transform.localScale = Vector3.one;
            created.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
