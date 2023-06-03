using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{   
    private PlayerControls playerInput;

    [Header ("Joystick")]
    public float horizontal;
    public float vertical;


    [Header ("Acceleration Settings")]
    public float speed;
    public float minSpeed;
    public float maxSpeed;
    public AnimationCurve SpeedCurve;

    [Header ("Speed Manager Boost Settings")]
    public float minBoostSpeed;
    public float maxBoostSpeed;
    public float riseTime;
    public float fallTime;
    
    [Header ("Rotation Settings")]
    public float yawAmount;
    public float rollAmount;
    public float pitchAmount;
    public float resetSpeed;
    public float minYaw = -15f;
    public float maxYaw = 15f;
    public float minPitch = -15f;
    public float maxPitch = 15f;
    public float minRoll = -15;
    public float maxRoll = 15;

    private float yaw;
    private float pitch;
    private float roll;

    public bool canMove = true;
    private float elapsedTime = 0;
    private float elapsedFallTime = 0;

    public float t;
    public float baseSpeed;

    public float magnitude;

    public InputActionReference moveAction;

    void Start()
    {   
        // playerInput = new PlayerControls();
        // playerInput.Player.Enable();
        transform.position = new Vector3(0,2,0);
    }
    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }
    void Update()
    {   

        // Vector2 inputValue = playerInput.Player.Move.ReadValue<Vector2>();
        Vector2 inputValue = moveAction.action.ReadValue<Vector2>();
        magnitude = Mathf.Abs(inputValue.magnitude);
        horizontal = inputValue.x;
        vertical = inputValue.y;
        
        if (magnitude > 0.9f) 
        {   
            elapsedTime += Time.deltaTime;
            elapsedFallTime = 0;
            t = Mathf.Clamp01(elapsedTime / riseTime);
            SpeedManager.speed = Mathf.Lerp(SpeedManager.speed, maxBoostSpeed, t);
        }
        else 
        {
            elapsedTime = 0;
            elapsedFallTime = Time.deltaTime;
            t = Mathf.Clamp01(elapsedFallTime / fallTime);
            SpeedManager.speed = Mathf.Lerp(SpeedManager.speed, minBoostSpeed, t);
        }

        magnitude = SpeedCurve.Evaluate(magnitude);
        speed = Mathf.Lerp(minSpeed, maxSpeed, magnitude);

        speed += SpeedManager.speed;

        if (canMove) Move();
    }

    void Move()
    {   

        Vector3 movement = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        transform.position = newPosition;
        
        if (horizontal == 0)
        {
            yaw = Mathf.Lerp(yaw, 0, resetSpeed * Time.deltaTime);
            roll = Mathf.Lerp(roll, 0, resetSpeed * Time.deltaTime);
        }
        else
        {
            yaw += horizontal * yawAmount * Time.deltaTime;
            yaw = Mathf.Clamp(yaw, minYaw, maxYaw);
            roll += vertical * rollAmount * Time.deltaTime;
            roll = Mathf.Clamp(roll, minRoll, maxRoll);
        }

        if (vertical == 0)
        {
            pitch = Mathf.Lerp(pitch, 0, resetSpeed * Time.deltaTime);

        }
        else
        {
            pitch += vertical * pitchAmount * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        }

        transform.localRotation = Quaternion.Euler(-pitch, yaw, -roll);
    }

    public void DisableMovement()
    {
        canMove = false;
        Invoke("EnableMovement", 1.0f);
    }

    public void EnableMovement()
    {
        canMove = true;
    }   
}
