using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{   
    public event Action<float> OnProgressUpdate;
    public event Action<int> OnNewLevelStarted;
    public event Action<int> OnLevelEnded;
    public event Action OnWaveEnded;

    [Header("Spawn Settings")]
    public List<EnemyScriptableObject> enemies; 
    public Vector3 spawnLocation; 
    public EnemyWave wave;
    public GameObject boss;
    public Vector3 bossSpawnLocation;

    private List<EnemyScriptableObject> scaledEnemies  = new List<EnemyScriptableObject>(); 
    public ScaleEnemyScriptableObject enemyScaling;
    public float[] weights;

    [Header("Wave Status")]
    public int enemiesSpawned = 0;
    public int enemiesDestroyed = 0;
    public int waveIndex = 0;
    public int totalWaves = 4;
    public WaveScalingScriptableObject waveScaling;
    public AnimationCurve curve;
    public LevelScriptableObject levelSO;
    public int level = 0;


    public static EnemySpawner Instance;
    private Transform player; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("Player").transform;
        level = 0;
        //DELETE 
        totalWaves = 2;
        StartNewLevel();
    }
    
    public void StartNewLevel()
    {   
        
        Debug.Log("Starting a new level");

        if (levelSO.mode == "Endless")
        {
            level++;
        }
        else
        {
            level = levelSO.level;
        }

        scaledEnemies  = new List<EnemyScriptableObject>();

        for (int i = 0; i < enemies.Count; i++) 
        {   
            if (enemies[i].levelToSpawn < level)
            {
                scaledEnemies.Add(enemies[i].ScaleUpForLevel(enemyScaling, level));
            } 
        }

        weights = new float[scaledEnemies.Count];
        ResetWeights();

        OnNewLevelStarted?.Invoke(level);

        waveIndex = 0;
        StartCoroutine(SpawnWave());
        UpdateProgress(); 



    }

    private void ResetWeights()
    {
        float totalWeight = 0;

        for (int i = 0; i < scaledEnemies.Count; i++)
        {
            weights[i] = scaledEnemies[i].GetWeight();
            totalWeight += weights[i];
        }

        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i] / totalWeight;
        }
    }

    private void SpawnWeightedEnemy() 
    {
        float value = Random.value;

        for (int i = 0; i < weights.Length; i++)
        {
            if (value < weights[i])
            {   
                spawnLocation += player.position;
                SpawnEnemy(i, spawnLocation);
                return;
            }

            value -= weights[i];
        }
    }
    
    public void UpdateEnemiesDestroyed ()
    {
        enemiesDestroyed++;
        UpdateProgress();

        if (enemiesDestroyed == wave.totalEnemies && enemiesSpawned == wave.totalEnemies) 
        {   
            if (waveIndex >= totalWaves)
            {   
                if (levelSO.mode == "Endless")
                {
                    StartNewLevel();
                }
                else
                {   
                    levelSO.MarkLevelAsCompleted();
                    OnLevelEnded?.Invoke(level);
                }
            }
            else 
            {   
                OnWaveEnded?.Invoke();
                StartCoroutine(SpawnWave());
            }
        }
    }  

    public IEnumerator SpawnBoss() 
    {   
        yield return new WaitForSeconds(wave.waitTime);
        Instantiate(boss, bossSpawnLocation + player.position, Quaternion.identity, transform);
    }
    
    public IEnumerator SpawnWave() 
    {   
        waveIndex++;
        wave = new EnemyWave(waveScaling, waveIndex);
        enemiesDestroyed = 0;
        enemiesSpawned = 0;
        wave.scaleWave();
        Debug.Log("Spawning a new wave");

        yield return new WaitForSeconds(wave.waitTime);
        
        for (int i = 0; i < wave.totalEnemies; i++)
        {   
            SpawnWeightedEnemy();
            yield return new WaitForSeconds(wave.spawnRate);
        } 
        yield break;
    }

    public void SpawnEnemy(int index, Vector3 position)
    {   
        EnemyScriptableObject enemy = scaledEnemies[index];
        GameObject obj = Instantiate(enemy.ship, position + player.position, Quaternion.identity, transform);
        obj.AddComponent<ChangeMaterials>();
        Enemy enemyScript = obj.GetComponent<Enemy>();
        enemyScript.SetValues(enemy);

        enemiesSpawned++;

        if(!wave.bossWave) 
        {
            enemyScript.OnDeath += UpdateEnemiesDestroyed;
        }
    }
    
    public void UpdateProgress()
    {   
        float levelProgress = (1.0f / (float)totalWaves) * (float)enemiesDestroyed / wave.totalEnemies;
        OnProgressUpdate?.Invoke(levelProgress);
    }

    [System.Serializable]
    public class EnemyWave 
    {
        public int totalEnemies = 1;
        public float spawnRate = 1.5f;
        public float waitTime = 2;
        public bool bossWave = false;
        public int bossWaveIndex = 2; 
    
        public EnemyWave(WaveScalingScriptableObject waveScaling, int _waveIndex) 
        {   
            totalEnemies = Mathf.FloorToInt(waveScaling.totalEnemies.Evaluate(_waveIndex));
            waitTime = Mathf.FloorToInt(waveScaling.waitTime.Evaluate(_waveIndex));
            spawnRate = Mathf.FloorToInt(waveScaling.spawnRate.Evaluate(_waveIndex));
        }
        public void scaleWave()
        {
            // totalEnemies = totalEnemies + totalEnemies;
            // spawnRate = waveScaling.spawnRate.Evaluate(_waveIndex);
        }
    }
}

