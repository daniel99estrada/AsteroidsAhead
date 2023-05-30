using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreText; 

    private void Start()
    {   
        scoreText = GetComponent<TextMeshProUGUI>();
        ScoreManager.Instance.OnScoreUpdate += UpdateScoreUI;
    }

    public void OnEnable()
    {   
        Debug.Log("Setting Score UI");
        scoreText.text = ScoreManager.Instance.GetScore().ToString();
    }

    private void UpdateScoreUI(int score)
    {
        scoreText.text = score.ToString();
    }
}
