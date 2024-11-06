using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPoolManager Instance;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public Transform DefaultParent;

    private void Awake()
    {
        //LeanTween.reset();
        Instance = this;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                if(pool.prefab.transform.parent != null)
                {
                    GameObject obj = Instantiate(pool.prefab, pool.prefab.transform.position, pool.prefab.transform.rotation, pool.prefab.transform.parent);
                    obj.SetActive(false);
                    obj.transform.SetParent(pool.prefab.transform.parent.transform);
                    objectPool.Enqueue(obj);
                }
                else
                {
                    GameObject obj = Instantiate(pool.prefab, pool.prefab.transform.position, pool.prefab.transform.rotation, DefaultParent);
                    obj.SetActive(false);
                    obj.transform.SetParent(DefaultParent);
                    objectPool.Enqueue(obj);
                }
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return new GameObject();
        }

        GameObject objectToSpawn = null;

        // Procurar por um objeto inativo no pool
        foreach (GameObject pooledObject in poolDictionary[tag])
        {
            if (pooledObject!=null && !pooledObject.gameObject.activeSelf)
            {
                objectToSpawn = pooledObject;
                break;
            }
        }

        // Se nenhum objeto inativo foi encontrado, criar um novo
        if (objectToSpawn == null)
        {
            Pool pool = pools.Find(p => p.tag == tag);
            if (pool != null)
            {
                if(pool.prefab.transform.parent != null)
                    objectToSpawn = Instantiate(pool.prefab, pool.prefab.transform.position, pool.prefab.transform.rotation, pool.prefab.transform.parent);
                else
                {
                    objectToSpawn = Instantiate(pool.prefab, pool.prefab.transform.position, pool.prefab.transform.rotation, DefaultParent);
                }
                poolDictionary[tag].Enqueue(objectToSpawn); // Enfileirar o novo objeto criado
            }
            else
            {
                Debug.LogWarning("No pool found with tag: " + tag);
                return null;
            }
        }

        // Configurar o objeto para ser usado
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        //objectToSpawn.SetActive(true);

        return objectToSpawn;
    }
}

public class PooledObject : MonoBehaviour
{
    [HideInInspector]
    public Transform originalParent;

    private void Awake()
    {
        originalParent = transform.parent;
    }
}
