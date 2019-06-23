using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual float Health { get; protected set; }
    public GameObject parent;
    public Rigidbody2D rb;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            HealthZero();
        }
        Debug.Log(Health);
    }

    protected virtual void HealthZero()
    {
        // In a normal enemy, this will cause death. In the boss, it may cause it to change stages, or die.

        // TODO: Spawn death effect and destroy self
        if (parent)
            Destroy(parent);
    }
}
