// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FastAsteroidManager : Grid
// {   
//     public float spawnInterval;
//     public AsteroidPrefabsScriptableObject asteroids;
//     public MoveFastScriptableObject settings;

//     void Start ()
//     {
//         StartCoroutine(SpawnActorsCoroutine());
//     }

//     IEnumerator SpawnActorsCoroutine()
//     {
//         while (true)
//         {
//             SpawnActors();
//             yield return new WaitForSeconds(spawnInterval);
//         }
//     }

//     public override void InstantiateObject(Vector3 position)
//     {   
//         int randomIndex = Random.Range(0, asteroids.prefabs.Count);
//         GameObject obj = Instantiate(asteroids.prefabs[randomIndex], position, Quaternion.identity, transform);

//         obj.AddComponent<FastAsteroid>();
//         MoveFast movement = obj.AddComponent<MoveFast>();
//         movement.SetValues(settings);
//     }
// }
