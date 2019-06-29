using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Missile : MonoBehaviour
{
    public Transform target;
    public Animator missileAnimator;
    public Rigidbody2D rb;
    public GameObject explosionPrefab;

    float accelerationForce = 1000f;
    // float rotationForce = 5f;
    //float acceleration = 1f;
    //float rotation = 0.2f;

    // Pathfinding variables

    // Minimum distance to the next waypoint for the pathfinder to start moving towards next waypoint
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        //missileAnimator.SetBool("IsAccelerating", true);
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
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
        //if (acceleration > 0)
        //    rb.AddForce((rb.transform.up * acceleration * accelerationForce) * Time.fixedDeltaTime);

        if (path == null)
        {
            // If there's no path then just accelerate forward
            // rb.AddForce(acceleration * accelerationForce * Time.fixedDeltaTime);
            return;
        }


        // If our current waypoint is equal to or larger than the total waypoints in the path, we are at the end of the path.
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // Get direction to next waypoint
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * accelerationForce * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            // We are at the current waypoint, so increase
            currentWaypoint++;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
