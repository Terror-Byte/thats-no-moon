﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Ship player;
    public BossAsteroid boss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If player presses escape, go back to main menu.
        if (Input.GetButtonDown("Cancel"))
            SceneManager.LoadScene("MainMenu");
    }
}
