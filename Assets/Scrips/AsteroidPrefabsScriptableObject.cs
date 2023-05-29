using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asteroid", menuName = "Container/asteroids")]
public class AsteroidPrefabsScriptableObject : ScriptableObject
{
    public List<GameObject> prefabs;
}