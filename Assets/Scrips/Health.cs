using UnityEngine;
using System;

public class Health : MonoBehaviour {

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;
    
    [Header("Health")]
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private int minHealth = 0;
    public int _health;

    public int health 
    {
        get {
            return _health;
        }
        set {
            value = Mathf.Clamp(value, minHealth, maxHealth);
            _health = value;
            if (OnHealthChanged != null) 
            {
                OnHealthChanged?.Invoke((float)_health/maxHealth);
            }
            if (health <= minHealth)
            {
                OnDeath?.Invoke();
            }
        }
    }

    void Start()
    {   
        health = maxHealth;
    } 

    public void TakeDamage(int damageAmount) {
        health -= damageAmount;
    }

    public void Heal(int healAmount) {
        health += healAmount;
    }
}
