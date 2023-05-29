// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FastAsteroid : MonoBehaviour, IActor
// {
//     public float minScale = 1f;
//     public float maxScale = 2;

//     void Start ()
//     {   
//         float scale = Random.Range(minScale, maxScale);
//         transform.localScale = new Vector3(scale, scale, scale);
//     }
    
//     void OnBecameInvisible() 
//     {   
//         Destroy(gameObject);
//     }

//     public void OnContact(int damageAmount)
//     {       
//         GameObject vfx = VFX.Instance.SpawnVFX("explosion", transform.position);
//         Destroy(this.gameObject);
//         Destroy(vfx, 3.0f);
//     }
// }

