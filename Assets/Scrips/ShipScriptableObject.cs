using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "ScriptableObject/Ship")]
public class ShipScriptableObject : ScriptableObject
{
    public GameObject ship;
    public string ammoTag;
    public Transform laserSpawnPoint;
    public int laserDamage;
    public float defenseStat;
    public float fireRate;
    public string tag;
    public float burstCount;
    public float burstDelay;
}
