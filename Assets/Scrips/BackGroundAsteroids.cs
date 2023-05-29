using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundAsteroids : Move, IActor 
{  
    public void Update()
    {
        MoveObject();
    }

    public void SetRandomScale(float minScale, float maxScale)
    {
        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

    public void OnContact(int takeDamage)
    {   
        gameObject.SetActive(true);
    }
    
    void OnBecameInvisible() 
    {   
        this.gameObject.SetActive(false);
    }
}