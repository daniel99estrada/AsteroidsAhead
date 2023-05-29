using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawner : MonoBehaviour
{ 
    public GameObject prefab;
    public MoveFastScriptableObject settings;
    public float startingPoint;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float spawnInterval;

    Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnEnable ()
    {
        InvokeRepeating("SpawnActors", spawnInterval, spawnInterval);
    }

    public void SpawnActors() 
    {   
        GameObject parent = new GameObject("Ring Pattern");
        parent.transform.SetParent(this.transform);

        int[] dir = {0, 0};
        int[] newDir = {0, 0};

        float x =  player.position.x;
        float y = player.position.y;
        float z = startingPoint + player.position.z;

        int totalStrips = Random.Range(2, 3);

        for(int i = 0 ; i < totalStrips; i++) 
        {   
            int strip = Random.Range(2, 3);

            for (int j = 0; j < strip; j++) 
            {
                x += (dir[0] * xOffset);
                y += (dir[1] * yOffset);
                z += zOffset; 
                Vector3 position = new Vector3(x, y, z);
                GameObject obj = Instantiate(prefab, position, prefab.transform.rotation, parent.transform);
                obj.AddComponent<Ring>();
            }
            
            do
            {
                newDir[0] = Random.Range(-1,1); 
                newDir[1] = Random.Range(-1,1);
            }
            while (dir[0] == newDir[0] && dir[1] == newDir[0]);

            dir[0] = newDir[0];
            dir[1] = newDir[1];
        }
        MoveFast movement = parent.AddComponent<MoveFast>();
        movement.SetValues(settings);
    }
}
