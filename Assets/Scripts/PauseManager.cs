using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public bool isInputPaused;
    public bool isGamePaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Pause() //Pausa los inputs
    {
        if (isInputPaused)
        {
            isInputPaused = false;
        }
        else if (isInputPaused == false)
        {
            isInputPaused = true;
        }
    }

    public void StopGame() //Pausa el juego entero
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isGamePaused = true;
        }
        
    }
}
