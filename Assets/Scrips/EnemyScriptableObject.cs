using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/Enemy Settings", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{   
    [Header("Ship Settings")]
    public GameObject ship;
    public int levelToSpawn;

    [Header("Movement Settings")]
    public float chasingSpeed;
    public float attackingSpeed;
    public float evasionTime;

    [Header("Weapon Settings")]
    public string ammoTag;
    public float laserSpeed;
    public int laserDamage;
    public float shootingRange;
    public float fireRate;
    public float burstDelay;
    public float burstCount;

    [Header("Weight")]
    [Range(0,1)]
    public float minWeight;
    [Range(0,1)]
    public float maxWeight;

    public float GetWeight()
    {
        return Random.Range(minWeight, maxWeight);
    }
    

    [Header("Health Settings")]
    public int maxHealth;

    public EnemyScriptableObject ScaleUpForLevel(ScaleEnemyScriptableObject scaling, int level)
    {   
        EnemyScriptableObject scaledUpEnemy = CreateInstance<EnemyScriptableObject>();

        scaledUpEnemy.ship = ship;
        scaledUpEnemy.ammoTag = ammoTag; 
        scaledUpEnemy.laserSpeed = laserSpeed; 
        scaledUpEnemy.laserDamage = Mathf.FloorToInt(laserDamage * scaling.laserDamage.Evaluate(level)); 
        scaledUpEnemy.shootingRange = shootingRange; 
        scaledUpEnemy.fireRate = fireRate;
        scaledUpEnemy.burstDelay = burstDelay;
        scaledUpEnemy.burstCount = burstCount;
        scaledUpEnemy.maxHealth = Mathf.FloorToInt(maxHealth * scaling.maxHealth.Evaluate(level)); 
        scaledUpEnemy.chasingSpeed = chasingSpeed;
        scaledUpEnemy.attackingSpeed = attackingSpeed;
        scaledUpEnemy.levelToSpawn = levelToSpawn;
        scaledUpEnemy.evasionTime = evasionTime;
        scaledUpEnemy.minWeight = minWeight;
        scaledUpEnemy.maxWeight = maxWeight;
        
        return scaledUpEnemy;
    } 
}
