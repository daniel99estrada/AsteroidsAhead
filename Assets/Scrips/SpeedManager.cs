using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager: MonoBehaviour
{   
    public static float speed = 0;
    public float minSpeed;
    public float maxSpeed;
    public static bool accelerate = false;
    public static bool deaccelerate = false;

    private float elapsedTime;
    public float duration = 1;
    public float boostDuration;

    void Update()
    {
        if (accelerate) 
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            speed = Mathf.Lerp(minSpeed, maxSpeed, t);
        }

        if (speed >=maxSpeed) Invoke("Deaccelerate", boostDuration); 

        else
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            speed = Mathf.Lerp(maxSpeed, minSpeed, t);
        }   
    }

    public static void Accelerate()
    {   
        accelerate = true;
        CameraFollow.Instance.Move();
    }

    public void Deaccelerate ()
    {
        accelerate = false; 
        CameraFollow.Instance.Chase();
    }
}
