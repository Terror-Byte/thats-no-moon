using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // public Animator shipAnimator; // Not needed yet, just here for when the ship is animated. 
    public GameObject bulletOrigin; // Origin point where bullets are spawned
    public GameObject bulletPrefab;
    public Rigidbody2D rb;
    // public GameController gc;

    public float Health { get; private set; } = 100f;

    public float fireRate = 1f;
    public float fireCooldown = 0f;
    private bool canFire = true;

    public float accelerationForce = 10f;
    public float rotationForce = 5f;
    float rotation;
    float acceleration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null)
        {
            Debug.LogError("Ship script holds no reference to the ship's rigidbody component.");
            return;
        }

        rotation = Input.GetAxisRaw("Horizontal");
        acceleration = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1") && canFire)
        {
            Shoot();
        }

        if (!canFire)
            FireCooldown(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.AddTorque((-rotation * rotationForce) * Time.fixedDeltaTime);

        // Only accelerate if the acceleration is positive (aka the player is pressing forward).
        if (acceleration > 0)
            rb.AddForce((rb.transform.up * acceleration * accelerationForce) * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        // TODO: Spawn bullet
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not set on ship script.");
        }
        Instantiate(bulletPrefab, bulletOrigin.transform.position, rb.transform.rotation);

        canFire = false;
        fireCooldown = 0;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // TODO: Implement whatever happens when the player dies
    }

    private void FireCooldown(float dt)
    {
        // fireCooldown += 1
        fireCooldown += 1 * dt;

        if (fireCooldown >= fireRate)
        {
            fireCooldown = 0;
            canFire = true;
        }
    }
}
