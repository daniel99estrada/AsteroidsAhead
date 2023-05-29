using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance
    private static ScoreManager instance;

    public int amount = 1;
    public float waitTime = 0.6f;

    private Coroutine incrementScoreCoroutine; 

    // Event for score updates
    public event Action<int> OnScoreUpdate;
    private int _score;

    public int score
    {
        get 
        {
            return _score;
        }
        set
        {
            _score = value;
            OnScoreUpdate?.Invoke(score);
        }
    }

    // Get singleton instance
    public static ScoreManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        // Ensure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Start()
    {
        GameManager.Instance.OnGameOver += StopIncrementingScore;
        PauseManager.Instance.OnPaused += StopIncrementingScore;
        PauseManager.Instance.OnUnpaused += StartIncrementingScoreOverTime;

        score = 0;
        StartIncrementingScoreOverTime();  // Start the coroutine and store the reference
    }

    public void StopIncrementingScore()
    {
        if (incrementScoreCoroutine != null)
        {
            StopCoroutine(incrementScoreCoroutine);
            incrementScoreCoroutine = null;  // Clear the reference
        }
    }

    public void StartIncrementingScoreOverTime()
    {
        if (incrementScoreCoroutine == null)
        {
            incrementScoreCoroutine = StartCoroutine(IncrementScoreOverTime());
        }
    }

    public IEnumerator IncrementScoreOverTime()
    {
        while (true)
        {
            score += amount;
            yield return new WaitForSeconds(waitTime);
        }
    }

    // Method to increment the score
    public void IncrementScore(int value)
    {
        score += value;
        OnScoreUpdate?.Invoke(score);
    }

    // Method to decrement the score
    public void DecrementScore(int value)
    {
        score -= value;
        OnScoreUpdate?.Invoke(score);
    }

    // Method to retrieve the current score
    public int GetScore()
    {
        return score;
    }
}
