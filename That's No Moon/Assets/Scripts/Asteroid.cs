using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    // Used by Large and Medium asteroids. When the asteroid is destroyed, it chooses from these prefabs to spawn a number of smaller asteroids.
    public GameObject[] onDeathSpawnPrefabs;
    // Used by Small asteroids. On death they merely spawn an explosion effect instead of producing smaller asteroids.
    public GameObject onDeathExplosion;

    public enum AsteroidType { Large, Medium, Small };
    public AsteroidType asteroidType;

    // Start is called before the first frame update
    void Start()
    {
        switch (asteroidType)
        {
            case AsteroidType.Small:
                Health = 25;
                break;

            case AsteroidType.Medium:
                Health = 50;
                break;

            case AsteroidType.Large:
                Health = 75;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void HealthZero()
    {
        if ((asteroidType == AsteroidType.Large || asteroidType == AsteroidType.Medium) && onDeathSpawnPrefabs.Length > 0)
        {
            // float noToSpawn = Random.Range(2, 3);
            float noToSpawn = 2;

            float nudgePower = 0;
            if (asteroidType == AsteroidType.Large)
                nudgePower = 5000;
            else if (asteroidType == AsteroidType.Medium)
                nudgePower = 1000;

            // Spawn them either end of the right vector and give them a "push".
            Vector3 right = transform.right;
            Vector3 posOne = new Vector3(rb.transform.position.x + (-right.x * 0.5f), rb.transform.position.y + right.y, rb.transform.position.z + right.z);
            Vector3 posTwo = new Vector3(rb.transform.position.x + (right.x * 0.5f), rb.transform.position.y + right.y, rb.transform.position.z + right.z);

            for (int i = 0; i < noToSpawn; i++)
            {
                Vector3 pos = new Vector3();

                if (i == 0)
                    pos = posOne;
                else if (i == 1)
                    pos = posTwo;

                if (i == 1)
                    right = new Vector3(-right.x, right.y, right.z);

                int toSpawnIndex = Random.Range(0, onDeathSpawnPrefabs.Length);

                Quaternion newRotation = Quaternion.Euler(rb.transform.rotation.eulerAngles.x, rb.transform.rotation.eulerAngles.y, Random.Range(0, 360));
                GameObject go = Instantiate(onDeathSpawnPrefabs[toSpawnIndex], pos, newRotation);

                Vector3 dir = go.transform.position - rb.transform.position;
                go.GetComponent<Rigidbody2D>().AddForce(dir * nudgePower);
            }
        }
        else
        {
            if (onDeathExplosion)
            {
                Instantiate(onDeathExplosion, rb.transform.position, rb.transform.rotation);
                Destroy(parent);
            }
            else
            {
                Debug.LogError("OnDeathExplosion prefab not set.");
            }
        }

        // Find a new sound!
        // AudioManager.instance.Play("Explosion");
        Destroy(parent);
    }
}
