using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{   
    public event Action OnPaused;
    public event Action OnUnpaused;

    public static PauseManager Instance;

    public bool isPaused {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PauseGame()
    {   
        OnPaused?.Invoke();
        isPaused = true;
        Time.timeScale = 0f;

    }

    public void UnpauseGame()
    {   
        OnUnpaused?.Invoke();  
        isPaused = false;
        Time.timeScale = 1f;  

    }
}
