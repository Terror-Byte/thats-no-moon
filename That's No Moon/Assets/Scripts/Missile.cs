using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject player;
    public Animator missileAnimator;
    public Rigidbody2D rb;

    public float accelerationForce = 10f;
    public float rotationForce = 5f;
    float acceleration = 1f;
    float rotation = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        missileAnimator.SetBool("IsAccelerating", true);
    }

    // Update is called once per frame
    void Update()
    {
        // Turn to face the player
    }

    private void FixedUpdate()
    {
        // Apply acceleration towards player
        //rb.AddTorque((-rotation * rotationForce) * Time.fixedDeltaTime);

        // Only accelerate if the acceleration is positive (aka the player is pressing forward).
        if (acceleration > 0)
            rb.AddForce((rb.transform.up * acceleration * accelerationForce) * Time.fixedDeltaTime);
    }
}
