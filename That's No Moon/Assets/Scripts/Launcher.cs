using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject prefabToLaunch;
    public GameObject launchPoint;
    public GameObject parent;
    public GameObject explosionPrefab;

    public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        Instantiate(prefabToLaunch, launchPoint.transform.position, parent.transform.rotation);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
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
            Debug.LogError("Launcher Explosion prefab is not set.");
        }

        if (parent)
        {
            Destroy(parent);
        }
        else
        {
            Debug.LogError("Launcher Parent gameobject not set.");
        }
    }
}
