using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFast : Move
{
    [Header("Deacceleration Settings")]
    public float startValue;
    public float endValue;
    public float duration;
    public float deaccelerationPoint;
    private float elapsedTime; 

    public void SetValues(MoveFastScriptableObject settings)
    {   
        speed = settings.startValue;
        startValue = settings.startValue;
        endValue = settings.endValue;
        duration = settings.duration;
        deaccelerationPoint = settings.deaccelerationPoint; 
        direction = new Vector3(0,0,-1);
    }

    void Update()
    {
        MoveObject();
        SlowDown();
    }

    void SlowDown()
    {   
        if (transform.position.z < deaccelerationPoint)
        {   
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            speed = Mathf.Lerp(startValue, endValue, t);
        }
    }
}