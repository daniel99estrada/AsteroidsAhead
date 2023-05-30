using UnityEngine;
using System;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject inputUI;
    public GameObject gameOverUI;
    public GameObject levelCompletedUI;
    public GameObject pauseUI;
    public GameObject startUI; // Reference to the start UI game object
    public GameObject settingsUI;

    private bool isStartUIActive = true;

    private void Awake()
    {
        if (PauseManager.Instance == null)
        {
            Debug.LogError("PauseManager is not set up!");
            return;
        }
    }

    void Start()
    {   
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameManager.Instance.OnGameOver += GameOverUI;
        EnemySpawner.Instance.OnLevelEnded += LevelCompleted;
        PauseManager.Instance.OnPaused += HandlePaused;
        PauseManager.Instance.OnUnpaused += HandleUnpaused;
    }

    private void OnDestroy()
    {
        if (PauseManager.Instance == null)
        {
            return;
        }

        PauseManager.Instance.OnPaused -= HandlePaused;
        PauseManager.Instance.OnUnpaused -= HandleUnpaused;
    }

    public void ToggleUIState()
    {
        isStartUIActive = !isStartUIActive; 

        startUI.SetActive(isStartUIActive);
        settingsUI.SetActive(!isStartUIActive);
    }

    private void HandlePaused()
    {   
        pauseUI.SetActive(true);
        inputUI.SetActive(false);
        SoundManager.Instance.PlaySound(SoundManager.Sound.UI);
    }

    private void HandleUnpaused()
    {   
        pauseUI.SetActive(false);
        inputUI.SetActive(true);
        SoundManager.Instance.PlaySound(SoundManager.Sound.UI);
    }

    public void GameOverUI()
    {
        gameOverUI.SetActive(true);
        inputUI.SetActive(false);
        pauseUI.SetActive(false);
    }
    
    public void LevelCompleted(int i)
    {   
        inputUI.SetActive(false);
        levelCompletedUI.SetActive(true);
        pauseUI.SetActive(false);

    }
}

