using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{   
    [System.Serializable]
    public class Pool 
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public float lifetime;
    }
    
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton

    public static ObjectPool Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    #endregion

    void Start () 
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {   
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                PooledObject objSettings = obj.AddComponent<PooledObject>();
                objSettings.SetLifeTime(pool.lifetime);

                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    
    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {   
        if (!poolDictionary.ContainsKey(tag)) 
        {
            Debug.LogWarning("pool wth tag " + tag + "doesn't exist"); 
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.GetComponent<PooledObject>().StartDeactivationTimer();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public GameObject SpawnFromRandomPool (Vector3 position, Quaternion rotation) 
    {
        string randomTag = pools[Random.Range(0, pools.Count)].tag;

        return SpawnFromPool(randomTag, position, rotation);
    }
}
