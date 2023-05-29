using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{   
    [Header("Ray Settings")]
    public int damageAmount;
    private GameObject spawner;
    private string targetTag;

    public void SetSpawner(GameObject _spawner)
    {
        spawner = _spawner;
    }
    public void SetDamageAmount(int _damageAmount)
    {
        damageAmount = _damageAmount;
    }
    public void SetTargetTag(string _targetTag)
    {
        targetTag = _targetTag;
    }

    void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag != targetTag) return;
        
        IActor actor = collision.gameObject.GetComponent<IActor>();
        
        if (actor != null) 
        {   
            actor.OnContact(damageAmount);
            this.gameObject.SetActive(false);
        }  
    }

    void OnBecameInvisible() 
    {   
        this.gameObject.SetActive(false);
    }
}
