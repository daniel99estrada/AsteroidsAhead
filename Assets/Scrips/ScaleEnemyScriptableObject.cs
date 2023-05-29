using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "ScaleEnemy", menuName = "ScriptableObjects/Scale Enemy Settings", order = 1)]
public class ScaleEnemyScriptableObject : ScriptableObject
{
    public AnimationCurve fireRate;
    public AnimationCurve burstDelay;
    public AnimationCurve burstCount;
    public AnimationCurve maxHealth;
    public AnimationCurve laserDamage;
}
