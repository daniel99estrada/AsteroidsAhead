using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{   
    public GameObject shield;
    
    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    
    // public float nextSpawnTime;
    // public float spawnRate;

    // [Header("Switch States")]
    // public float switchStateTime;
    // public float switchStateRate;

    // public Vector3 spawnPosition;
    // public Vector3 attackPosition;

    // public new enum State {
    //     Chasing,
    //     Attacking,
    //     Spawning,
    // }
    
    // public new State currentState = State.Chasing;

    // void Update()
    // {   
    //     switch(currentState) 
    //     {
    //         default:
    //         case State.Chasing:
    //             Debug.DrawLine(transform.position, target.position, Color.white);
    //             if (Vector3.Distance(target.position, transform.position) < shootingRange) {
    //                 currentState = State.Attacking;
    //             }
    //         break;
    //         case State.Attacking:
    //             rb.constraints = RigidbodyConstraints.FreezePositionZ;
    //             if (Time.time > nextShootTime) {
    //                 offset = attackPosition;
    //                 StartCoroutine(BurstFire());
    //                 nextShootTime = Time.time + fireRate;
    //                 // offset += attackPosition;
    //                 shield.SetActive(false);
    //             }
    //             if (Vector3.Distance(target.position, transform.position) > shootingRange) {
    //                 currentState = State.Chasing;
    //             }
    //         break;
    //         case State.Spawning:
    //             if (Time.time > nextSpawnTime) 
    //             {   
    //                 offset = spawnPosition;

    //                 Vector3 position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    //                 EnemySpawner.Instance.SpawnEnemy(0, position);
                    
    //                 nextSpawnTime = Time.time + spawnRate;
    //                 shield.SetActive(true);
    //             }
    //         break;
    //     }

    //     if (currentState != State.Chasing) {
    //         if (Time.time >= switchStateTime) {
    //              currentState = currentState == State.Attacking ? State.Chasing: State.Attacking;
    //             switchStateTime = Time.time + switchStateRate;
    //         }
    //     }
    // } 

    IEnumerator AttackPattern ()
    {   
        canMove = true;
        yield return StartCoroutine(Evade());
        yield return new WaitForSeconds(evasionTime);
        canMove = false;
        yield return StartCoroutine(BurstFire());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Spawn());
        StartCoroutine(AttackPattern());
    }

    IEnumerator Spawn()
    {   
        Vector3 position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        EnemySpawner.Instance.SpawnEnemy(0, position);  
        yield return new WaitForSeconds(3); 
    }

    public void HandleDeath2()
    {   
        // StartCoroutine(EnemySpawner.Instance.StartNewLevel());   
        base.HandleDeath();
    }
}
