using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{   
    public LevelScriptableObject levelSO;
    
    public void ResetScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
    }

    public void LoadLevel(int level)
    {   
        levelSO.level = level;
        levelSO.mode = "Mission";
        SoundManager.Instance.PlaySound(SoundManager.Sound.UI);
        SceneTransitioner.Instance.LoadScene("Game");
    }

    public void LoadMissionScene()
    {   
        SoundManager.Instance.PlaySound(SoundManager.Sound.UI);
        SceneTransitioner.Instance.LoadScene("Missions");
    }

    public void LoadEndless()
    {   
        levelSO.level = 1;
        levelSO.mode = "Endless";
        SoundManager.Instance.PlaySound(SoundManager.Sound.UI);
        SceneTransitioner.Instance.LoadScene("Game");
    }

    public void LoadHomeScene()
    {   
        SoundManager.Instance.PlaySound(SoundManager.Sound.UI);
        SceneTransitioner.Instance.LoadScene("Home");
    }
}
