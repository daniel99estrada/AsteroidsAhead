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

    void OnEnable()
    {
        scoreText.text = ScoreManager.Instance.score.ToString();
    }

    private void UpdateScoreUI(int score)
    {
        scoreText.text = score.ToString();
    }
}
