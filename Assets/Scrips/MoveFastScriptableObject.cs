using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveFastSettings", menuName = "Settings/MoveFast")]
public class MoveFastScriptableObject : ScriptableObject
{
    [Header("Deacceleration Settings")]
    public float startValue;
    public float endValue;
    public float duration;
    public float deaccelerationPoint;
}