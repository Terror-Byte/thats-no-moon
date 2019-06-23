using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Animator shipAnimator;
    public Animator shieldAnimator;
    public GameObject bulletOrigin; // Origin point where bullets are spawned
    public GameObject bulletPrefab;
    public Rigidbody2D rb;
    // public GameController gc;

    public float Health { get; private set; } = 100f;
    private bool isAlive = true;

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
        // TODO: Decide whether to play the music from the main menu or start it when the scene starts
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
            return;

        if (rb == null)
        {
            Debug.LogError("Ship script holds no reference to the ship's rigidbody component.");
            return;
        }

        UpdateInput();
    }

    private void FixedUpdate()
    {
        rb.AddTorque((-rotation * rotationForce) * Time.fixedDeltaTime);

        // Only accelerate if the acceleration is positive (aka the player is pressing forward).
        if (acceleration > 0)
            rb.AddForce((rb.transform.up * acceleration * accelerationForce) * Time.fixedDeltaTime);
    }

    private void UpdateInput()
    {
        float accelerationOld = acceleration;

        rotation = Input.GetAxisRaw("Horizontal");
        acceleration = Input.GetAxisRaw("Vertical");


        // If player was still but is now accelerating
        if (accelerationOld == 0 && acceleration > 0)
        {
            shipAnimator.SetBool("IsAccelerating", true);
            AudioManager.instance.Play("Acceleration");
        }
        else if (accelerationOld > 0 && acceleration == 0)
        {
            shipAnimator.SetBool("IsAccelerating", false);
            AudioManager.instance.Stop("Acceleration");
        }

        //if (acceleration > 0)
        //{
        //    // If player is accelerating, transition to burn animation state
        //    shipAnimator.SetBool("IsAccelerating", true);
        //}
        //else
        //{
        //    shipAnimator.SetBool("IsAccelerating", false);
        //}

        if (Input.GetButtonDown("Fire1") && canFire)
        {
            Shoot();
        }

        if (!canFire)
            FireCooldown(Time.deltaTime);
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
        AudioManager.instance.Play("Laser");
    }

    // Return true if player still alive, return false if they died.
    public bool TakeDamage(float damage)
    {
        Health -= damage;

        Debug.Log("Ouch! Health is now: " + Health);

        if (Health <= 0)
        {
            Die();
            return false;
        }        

        shieldAnimator.SetTrigger("IsDamaged");
        return true;
    }

    // Take damage and bump the player away from the object dealing damage
    public void TakeDamage(float damage, Vector2 enemyPos)
    {
        if (TakeDamage(damage))
        {
            // A->B = B-A
            // Enemy->Player = Player-Enemy
            float bumpPower = 100f;
            Vector2 dir = ((Vector2)rb.transform.position - enemyPos).normalized;
            rb.AddForce(dir * bumpPower);
        }
    }

    private void Die()
    {
        // TODO: Implement whatever happens when the player dies
        // Destroy gameobject
        // Spawn destruction animation
        AudioManager.instance.Play("Explosion");
        shipAnimator.SetTrigger("HasDied");
        isAlive = false;
        rotation = 0;
        acceleration = 0;
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0f;
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
