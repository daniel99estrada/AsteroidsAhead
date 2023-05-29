using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 direction = new Vector3(0,0,-1);
    public float speed;
    private float currentSpeed;

    void Update()
    {
        MoveObject();
    }

    public void MoveObject()
    {   
        currentSpeed = SpeedManager.speed + speed;
        this.transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);
    }
    
    public void SetRandomSpeed(float min, float max)
    {
        speed = Random.Range(min, max);
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
