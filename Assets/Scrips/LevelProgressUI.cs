using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUI : MonoBehaviour
{
    private Image imageComponent;
    public float updateSpeed;
    
    void Start()
    {   
        imageComponent = GetComponent<Image>();
        imageComponent.fillAmount = 0;
        EnemySpawner.Instance.OnProgressUpdate += UpdateImage;
        EnemySpawner.Instance.OnNewLevelStarted += ResetProgressUI;
    }

    private void UpdateImage(float progress)
    {   
        float end = imageComponent.fillAmount + progress;
        float start = imageComponent.fillAmount;
        float time = 0;

        while (time < 1)
        {
            imageComponent.fillAmount = Mathf.Lerp(start, end, time);
            time += Time.deltaTime * updateSpeed;
        }
    }
    
    private void ResetProgressUI(int level)
    {
        imageComponent.fillAmount = 0;
    }
}

