﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controls : Singletone<Controls>
{
    public void Update() {
        if (HoverManager.instance.hoveredCreatureView != null) {
            if (Input.GetMouseButtonDown(0)) {
                HoverManager.instance.hoveredCreatureView.Eat();
            }
            if (Input.GetKeyDown(KeyCode.X)) {
                HoverManager.instance.hoveredCreatureView.Die();
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                HoverManager.instance.hoveredCreatureView.LevelUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Pause)) {
            TimeManager.instance.Paused ^= true;
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            Time.timeScale *= 2;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Time.timeScale /= 2;
        }
    }
}