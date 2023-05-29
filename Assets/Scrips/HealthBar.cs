using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{   
    private Health enemy; 
    public Transform bar1;
    public Transform bar2;
    private float healthFactor;
    public float maxSize = 10;
    public Vector3 offset = new Vector3(0, 1, 0);
    private float yScale = 1;
    private float zScale = 1;

    void Awake()
    {   
        enemy = transform.parent.GetComponent<Health>();
        enemy.OnHealthChanged += UpdateHealthBar;
    }

    void Start()
    {
        maxSize = bar1.localScale.x + bar2.localScale.x;
        yScale = bar1.localScale.y;
        zScale = bar1.localScale.z;
    }

    public void UpdateHealthBar(float healthFactor) 
    {   
        float x = maxSize * healthFactor;
        Vector3 position1 = new Vector3(-(maxSize - x) / 2, 0, 0);
        Vector3 position2 = new Vector3(x / 2, 0, 0);
        Vector3 scale1 = new Vector3(x, yScale, zScale);
        Vector3 scale2 = new Vector3(maxSize - x, yScale, zScale);

        bar1.localScale = scale1;
        bar1.localPosition = position1;
        bar2.localPosition = position2;
        bar2.localScale = scale2;
    }
}
