using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private float lifetime;

    public void SetLifeTime(float _lifetime)
    {
        lifetime = _lifetime;
    }

    public void StartDeactivationTimer()
    {
        StartCoroutine(SetInactive());
    }

    private IEnumerator SetInactive()
    {   
        yield return new WaitForSeconds(lifetime);
        this.gameObject.SetActive(false);
    }
}
