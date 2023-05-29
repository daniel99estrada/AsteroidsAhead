using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : Grid
{
    public float spawnInterval;
    public GameObject spherePrefab;
    public List<ShipScriptableObject> ships;
    public MoveFastScriptableObject settings;

    void OnEnable ()
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
        GameObject parent = new GameObject("parent");
        int randomIndex = Random.Range(0, ships.Count);
        GameObject sphere = Instantiate(spherePrefab, position, Quaternion.identity, parent.transform);
        GameObject ship = Instantiate(ships[randomIndex].ship, position, Quaternion.identity, parent.transform);
        sphere.GetComponent<ShipSphere>().SetShip(ships[randomIndex]);
        MoveFast movement = parent.AddComponent<MoveFast>();
        movement.SetValues(settings);
    }
}
