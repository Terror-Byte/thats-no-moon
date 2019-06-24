using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject parent;

    // When the explosion animation ends, destroy the gameobject
    public void OnAnimationEnd()
    {
        if (parent)
            Destroy(parent);
        else
            Debug.LogError("Explosion parent gameobject not set.");
    }
}
