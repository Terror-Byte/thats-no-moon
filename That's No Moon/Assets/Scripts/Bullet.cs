using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private int damage = 25;
    private float perishTimer = 0f;
    private float perishThreshold = 5f;
    public GameObject impactEffect;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        perishTimer += 1 * Time.deltaTime;

        if (perishTimer > perishThreshold)
        {
            Destroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // TODO: Do something when the bullet hits and enemy. (Destroy self and damage enemy)

            /*
             Enemy enemy = collision.GetComponent<Enemy>();
             if (enemy)
             {
                enemy.TakeDamage(damage);
             }
             */

            if (impactEffect)
                Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy();
        }
    }

    private void Destroy()
    {
        if (parent)
            Destroy(parent.gameObject);
        else
            Debug.LogError("Bullet parent prefab reference not set.");
    }
}
