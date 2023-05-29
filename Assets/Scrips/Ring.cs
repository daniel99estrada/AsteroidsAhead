using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{    
    void OnBecameInvisible() 
    {   
        Destroy(gameObject);
    }
}


