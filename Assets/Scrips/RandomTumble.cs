using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTumble : MonoBehaviour
{
    public float amplitudeY = 30;
    public float timeY = 4;
    public float amplitudeZ = 30;
    public float timeZ = 4;
    public float rotationX = 30; 

    void Update()
    {
        timeY += Time.deltaTime;
        timeZ += Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.x, Mathf.Sin(timeY) * amplitudeY, Mathf.Sin(timeZ) * amplitudeZ);
    }
}
