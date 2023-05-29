using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health, IActor
{   
    [Header("Movement")]
    public float smoothSpeed;
    public float chasingSpeed;
    public float attackingSpeed;
    public Vector3 offset = new Vector3(0,0,10);
    private float offsetMagnitude = 10;
    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;
    

    [Header("Shooting")]
    public Transform laserSpawnPoint;
    public string ammoTag;
    public float laserSpeed;
    private int laserDamage;

    private bool isShooting;
    public float shootingRange = 3;

    [Header("Burst Fire")]
    [Range(0, 2)]
    public float fireRate;
    [Range(0, 1)]
    public float burstDelay;
    [Range(0, 10)]
    public float burstCount;
    public float nextShootTime;

    [Header("Evasion")]
    public float evasionTime = 0.2f;
    public float evadeRate = 3;
    public float evasionSpeed = 0.1f;

    public Transform target;
    public Rigidbody rb;

    public enum State {
        Chasing,
        Attacking,
        Evading,
    }
    public State state;
    
    public bool canMove = true;
    
    void OnEnable()
    {   
        target = GameObject.FindGameObjectWithTag("Player").transform;

        health = maxHealth;
        OnDeath += HandleDeath;

        state = State.Chasing;
        smoothSpeed = chasingSpeed;
        canMove = true;
    
        GetNewOffset();
    }

    void Update()
    {   
        switch(state) {
            default:
            case State.Chasing:

                Debug.DrawLine(transform.position, target.position, Color.white);

                if (Vector3.Distance(target.position, transform.position) <= offset.magnitude * 1.2) {
                    smoothSpeed = attackingSpeed;
                    state = State.Attacking;
                    StartCoroutine(AttackPattern());
                }
            break;
            case State.Attacking:
                    
                Debug.DrawLine(transform.position, target.position, Color.red);

                if (Vector3.Distance(target.position, transform.position) > shootingRange) {
                    StopAllCoroutines();
                    smoothSpeed = chasingSpeed;
                    state = State.Chasing;
                    canMove = true;
                    offset = new Vector3(0,0,10);
                }

            break;  
        }

        transform.LookAt(target);
    }

    private void LateUpdate()
    {   
        if(!canMove) return;

        desiredPosition = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

     void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Laser"))
        {
            GetNewOffset();
        }
    }
    
    void GetNewOffset() 
    {   
        float x = Random.Range(-5,5);
        float y = Random.Range(0, 5);
        float z = Mathf.Sqrt(offsetMagnitude * offsetMagnitude - x * x - y * y);
        Vector3 newOffset = new Vector3(x, y, z);
    }

    IEnumerator AttackPattern ()
    {   
        canMove = true;
        yield return StartCoroutine(Evade());
        yield return new WaitForSeconds(evasionTime);
        canMove = false;
        yield return StartCoroutine(BurstFire());
        yield return new WaitForSeconds(1f);
        StartCoroutine(AttackPattern());
    }

    public IEnumerator Evade() 
    {      
        float x = Random.Range(-5, 5);
        float y = Random.Range(-5, 5);
        float z = Random.Range(10, 15);
        offset = new Vector3(x, y, z);
        yield return null;
    }

    public IEnumerator BurstFire()
    {   
        for (int i = 0; i < burstCount; i++)
        {
            Shoot();
            yield return new WaitForSeconds(burstDelay);
        }
    }

    void Shoot()
    {   
        GameObject laser = ObjectPool.Instance.SpawnFromPool(ammoTag, laserSpawnPoint.position, laserSpawnPoint.rotation);
        Vector3 direction = (target.position - transform.position).normalized;
        laser.AddComponent<Laser>();
        laser.GetComponent<Laser>().SetDamageAmount(laserDamage);
        laser.GetComponent<Laser>().SetTargetTag("Player");
        laser.AddComponent<Move>();
        laser.GetComponent<Move>().SetDirection(direction);
        laser.GetComponent<Move>().SetSpeed(laserSpeed);

        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyLaser);


        // Laser laserScript = laser.AddComponent<Laser>();
        // laserScript.SetSpawner(this.gameObject);
        // laserScript.SetDamageAmount(laserDamage);

        // Vector3 direction = (target.position - transform.position).normalized;
        // Move laserMovement = laser.AddComponent<Move>();
        // laserMovement.SetDirection(direction);
        // laserMovement.SetSpeed(laserSpeed);

    }
    public void OnContact(int damage)
    {
        TakeDamage(damage);
    }

    public void HandleDeath()
    {   
        ObjectPool.Instance.SpawnFromPool("Explosion", transform.position, transform.rotation);
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDestroyed);
        Destroy(this.gameObject);
    }

    public void SetValues(EnemyScriptableObject settings)
    {
        ammoTag = settings.ammoTag;
        laserSpeed = settings.laserSpeed;
        fireRate = settings.fireRate;
        burstDelay = settings.burstDelay;
        burstCount = settings.burstCount;
        maxHealth = settings.maxHealth;
        health = maxHealth;
        laserDamage = settings.laserDamage;
        chasingSpeed = settings.chasingSpeed;
        attackingSpeed = settings.attackingSpeed;
        evasionTime = settings.evasionTime;

        smoothSpeed = state == State.Chasing ? chasingSpeed : attackingSpeed;
    }
}
