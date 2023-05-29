using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Image imageComponent;
    private Health playerHealth;

    private void Awake()
    {   
        imageComponent = GetComponent<Image>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        playerHealth.OnHealthChanged += UpdateImage;
    }

    private void UpdateImage(float healthFactor)
    {   
        imageComponent.fillAmount = healthFactor;
    }
}
