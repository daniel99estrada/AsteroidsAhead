using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSphere : MonoBehaviour
{
    private ShipScriptableObject ship;
    public float rotationSpeed = 30f;
    

    void Update()
    {
        // Rotate the game object on the Y axis over time
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    public void SetShip(ShipScriptableObject _ship)
    {
        ship = _ship;
    }

    public ShipScriptableObject GetShip()
    {
        return ship;
    } 
}
