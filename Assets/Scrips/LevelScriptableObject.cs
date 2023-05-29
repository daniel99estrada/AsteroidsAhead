using UnityEngine;

[CreateAssetMenu(fileName = "level Configuration", menuName = "ScriptableObject/Level Configuration")]
public class LevelScriptableObject : ScriptableObject
{
    public int level;
    public string mode;
    public int highestLevelCompleted;

    public void MarkLevelAsCompleted()
    {
        highestLevelCompleted = Mathf.Max(level, highestLevelCompleted);
    }
}
