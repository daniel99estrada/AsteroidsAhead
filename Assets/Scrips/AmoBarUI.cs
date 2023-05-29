using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmoBarUI : HealthBar
{
    void Awake()
    {
        ShipManager ship = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>();

        ship.OnAmoUpdate += UpdateHealthBar;
        ship.OnAmoUpdate += EnableBar;
    }

    private float lastCallTime;

    private void Update()
    {
        if (Time.time - lastCallTime > 1f)
        {
            bar1.gameObject.SetActive(false);
            bar2.gameObject.SetActive(false);
        }
    }

    public void EnableBar(float f)
    {   
        lastCallTime = Time.time;
        bar1.gameObject.SetActive(true);
        bar2.gameObject.SetActive(true);
    }
}
