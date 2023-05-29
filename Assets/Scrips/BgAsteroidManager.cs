using System.Collections;
using UnityEngine;
using Random=UnityEngine.Random;
using System;

public class BgAsteroidManager : Grid
{   
    public float spawnInterval;
    public event Action OnAsteroidShowerEnd;
    
    [Header("Asteroid Scale")]
    public float minScale;
    public float maxScale;

    [Header("Asteroid Speed")]
    public float minSpeed;
    public float maxSpeed; 

    [Header("Asteroid Shower Settings")]
    public float showerMinSpeed;
    public float showerMaxSpeed; 
    public float asteroidShowerTime;
    public float showerRowPercentage;
    public float showerColPercentage;

    public static BgAsteroidManager Instance;

    private void OnEnable()
    {
        if (Instance == null) Instance = this;
    }
    void Start ()
    {
        StartCoroutine(SpawnActorsCoroutine());
    }

    IEnumerator SpawnActorsCoroutine()
    {
        while (true)
        {
            SpawnActors();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public override void InstantiateObject(Vector3 position)
    {
        GameObject obj = GetComponent<BackgroundAsteroidPool>().SpawnFromPool(position, Quaternion.identity);
        BackGroundAsteroids asteroid = obj.GetComponent<BackGroundAsteroids>();
        asteroid.SetRandomSpeed(minSpeed, maxSpeed);
        asteroid.SetRandomScale(minScale, maxScale);
    }

    public IEnumerator StartAsteroidShower()
    {
        float originalMinSpeed = minSpeed;
        float originalMaxSpeed = maxSpeed;
        float originalRowPercentage = rowPercentage;
        float originalColPercentage = colPercentage;

        minSpeed = showerMinSpeed;
        maxSpeed = showerMaxSpeed;
        rowPercentage = showerRowPercentage;
        colPercentage = showerColPercentage;

        asteroidShowerTime += Random.Range(0,3);

        yield return new WaitForSeconds(asteroidShowerTime);

        minSpeed = originalMinSpeed;
        maxSpeed = originalMaxSpeed;
        rowPercentage = originalRowPercentage;
        colPercentage = originalColPercentage;

        yield return new WaitForSeconds(asteroidShowerTime);

        OnAsteroidShowerEnd?.Invoke();
    }
}
