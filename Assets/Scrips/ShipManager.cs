using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using static UnityEngine.ParticleSystem;

public class ShipManager : Health, IActor
{   
    public int currentIndex = 0;
    public float baseSpeed;

    private ShipScriptableObject activeShipSO;
    private GameObject activeShipGO;
    public List<ShipScriptableObject> shipSOs = new List<ShipScriptableObject>();
    public List<GameObject> shipGOs = new List<GameObject>();
    public Dictionary<string, GameObject> shipPool;

    public Transform laserSpawnPoint;

    private PlayerControls playerInput;

    public static ShipManager Instance;
    private bool canShoot = true;
    
    public int maxAmo;
    public int currentAmo;
    public event Action<float> OnAmoUpdate;

    public float speed = 0.5f;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {   
        playerInput = new PlayerControls();
        playerInput.Player.Enable();
        playerInput.Player.Shoot.performed += Shoot;

        OnDeath += DeactivateShip;
        health = maxHealth;
        currentAmo = maxAmo;

        shipPool = new Dictionary<string, GameObject>();

        for(int i = 0; i < shipSOs.Count; i++)
        {
            GameObject shipGO = Instantiate(shipSOs[i].ship, transform.position, transform.rotation, transform);
            shipPool.Add(shipSOs[i].tag, shipGO);
            shipGO.SetActive(false);
        }

        activeShipSO = shipSOs[0];
        SwitchShip(shipSOs[0]);   
    }

    private void SwitchShip(ShipScriptableObject newShipSO)
    {   
        shipPool[activeShipSO.tag].SetActive(false);
        activeShipSO = newShipSO;
        shipPool[activeShipSO.tag].SetActive(true);
    }

    void OnTriggerEnter(Collider collider)
    {   
        if(collider.gameObject.tag == "Ring")
        {   
            Heal(50);
            SoundManager.Instance.PlaySound(SoundManager.Sound.HealthRing);
        }

        if(collider.gameObject.tag == "AmoRing")
        {   
            currentAmo += 20;
            currentAmo = Mathf.Clamp(currentAmo, 0, maxAmo);
            OnAmoUpdate?.Invoke((float) currentAmo/maxAmo);

            SoundManager.Instance.PlaySound(SoundManager.Sound.AmmoRing);
        }

        else if(collider.gameObject.tag == "Asteroid")
        {
            TakeDamage(50);
        }
        else if(collider.gameObject.tag == "PowerUp")
        {   
            GameObject vfx = ObjectPool.Instance.SpawnFromPool("YellowFirework", transform.position, transform.rotation);
            ShipScriptableObject newShipSO = collider.gameObject.GetComponent<ShipSphere>().GetShip();
            SwitchShip(newShipSO);

            SoundManager.Instance.PlaySound(SoundManager.Sound.PowerUp);
        }
    }

    public void OnContact(int damage)
    {   
        TakeDamage(Mathf.FloorToInt(damage - activeShipSO.defenseStat * damage));
    }

    public void Shoot(InputAction.CallbackContext context)
    {   
        // StartCoroutine(MoveForward());
        if (!canShoot || currentAmo <= 0) return;
        StartCoroutine(BurstFire());
    }

    public IEnumerator MoveForward()
    {
        float time = 0;
        Vector3 newPosition = new Vector3(transform.position.x,transform.position.x, transform.position.z + 20);
        while (time < 1)
        {   
            newPosition = new Vector3(transform.position.x,transform.position.x, newPosition.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, time);
            time += Time.deltaTime * speed;
            yield return null;
        }
    }

    public IEnumerator BurstFire()
    {   
        canShoot = false;
        for (int i = 0; i < activeShipSO.burstCount; i++)
        {
            DoShoot();
            yield return new WaitForSeconds(activeShipSO.burstDelay);
        }

        currentAmo--;
        currentAmo = Mathf.Clamp(currentAmo, 0, maxAmo);
        OnAmoUpdate?.Invoke((float)currentAmo/maxAmo);

        yield return new WaitForSeconds(activeShipSO.fireRate);
        canShoot = true;
    }

    public void DoShoot()
    {
        GameObject laser = ObjectPool.Instance.SpawnFromPool(activeShipSO.ammoTag, laserSpawnPoint.position, laserSpawnPoint.rotation);

        // Vector3 localDirection = Vector3.forward; 
        // Vector3 worldDirection = laserSpawnPoint.TransformDirection(localDirection);
        // laser.GetComponent<Move>().SetDirection(transform.forward);
        laser.GetComponent<Laser>().SetDamageAmount(activeShipSO.laserDamage);
        laser.GetComponent<Laser>().SetTargetTag("Enemy");  

        SoundManager.Instance.PlaySound(SoundManager.Sound.Laser);
    }

    public void DeactivateShip()
    {   
        ObjectPool.Instance.SpawnFromPool("Explosion", transform.position, transform.rotation);
        OnDeath -= DeactivateShip;
        Debug.Log("Deactivate");
        this.gameObject.SetActive(false);

    }
}
