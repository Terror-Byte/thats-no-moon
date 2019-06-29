using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public GameObject explosionPrefab;
    public GameObject debrisPrefab;
    public GameObject parent;

    public float accelerationForce = 10f;
    public float rotationForce = 5f;
    float acceleration = 1f;
    float rotation = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode()
    {
        // Spawn debris

        Debug.Log("Boom!");
        Die();
    }

    public void Die()
    {
        if (explosionPrefab)
        {
            Instantiate(explosionPrefab, parent.transform.position, parent.transform.rotation);
        }
        else
        {
            Debug.LogError("Mine Explosion Prefab not set.");
        }

        Destroy(parent);
    }
}
