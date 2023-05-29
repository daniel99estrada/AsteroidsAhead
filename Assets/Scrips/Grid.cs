using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour
{   
    public int z;
    public int rows;
    public int cols;
    public float width;
    public float height;
    
    [Range(0f, 1f)]
    public float rowPercentage;
    [Range(0f, 1f)]
    public float colPercentage;

    private float YOFFSET;
    private float XOFFSET;
    Transform player;


    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnActors()
    {   
        XOFFSET = - (rows * width)/2 + player.position.x;
        YOFFSET = - (cols * height)/2 + player.position.y;
        float newZ = z + player.position.z;

        int n = Mathf.RoundToInt(rowPercentage * rows);
        int[] pickedrows = Enumerable.Range(0, rows).OrderBy(x => Random.value).Take(n).ToArray();

        foreach (int row in pickedrows)
        {   
            n = Mathf.RoundToInt(colPercentage * cols);
            int[] pickedColumns = Enumerable.Range(0, cols).OrderBy(x => Random.value).Take(n).ToArray();

            foreach (int col in pickedColumns)
            {     
                float x = (row * width) + XOFFSET;
                float y = (col * height) + YOFFSET;
                Vector3 position = new Vector3(x, y, newZ);
                
                InstantiateObject(position); 
            }
        }
    }

    public virtual void InstantiateObject(Vector3 position)
    {
        Debug.Log("Instantiate Objects not defined");
    }
}

