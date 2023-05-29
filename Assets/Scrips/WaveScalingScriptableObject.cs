using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "Scale Wave Configuration", menuName = "ScriptableObject/Scale Wave Configuration")]
public class WaveScalingScriptableObject : ScriptableObject
{
    public AnimationCurve totalEnemies;
    public MinMaxCurve spawnRate;
    public MinMaxCurve waveIndex;
    public MinMaxCurve waitTime;
    public MinMaxCurve bossWave;
    public AnimationCurve bossWaveIndex; 
}
