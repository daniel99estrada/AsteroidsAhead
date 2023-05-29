using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Random=UnityEngine.Random;

public class GameManager : MonoBehaviour
{   
    public event Action OnGameOver;
    int level;
    public LevelScriptableObject levelSO;

    public AnimationCurve AsteroidProbability;

    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    #endregion
    void Start()
    {   
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            ShipManager player = playerObject.GetComponent<ShipManager>();
            player.OnDeath += TriggerGameOver;
            // EnemySpawner.Instance.OnLevelEnded += HandleWaveEnd;
            EnemySpawner.Instance.OnWaveEnded += HandleWaveEnd;
            BgAsteroidManager.Instance.OnAsteroidShowerEnd += StartNewWave;
        }
    }
    
    private void TriggerGameOver()
    {
        OnGameOver?.Invoke();
    }

    public void StartNewLevel()
    {   
        EnemySpawner.Instance.StartNewLevel();
    }

    public void StartNewWave()
    {   
        EnemySpawner.Instance.SpawnWave();
    }

    private void HandleWaveEnd()
    {
       float rand = Random.Range(0f, 1f);
       
       float waveProgress = EnemySpawner.Instance.waveIndex / EnemySpawner.Instance.totalWaves; 

       if (rand < AsteroidProbability.Evaluate(EnemySpawner.Instance.waveIndex))
       {    
            Debug.Log("Game manager start asteroid shower");
            StartCoroutine(BgAsteroidManager.Instance.StartAsteroidShower());
       }
       else
       {
            Debug.Log("Game manager start new wave");        
            StartNewWave();
       }
    }
}
