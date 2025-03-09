using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Playables;

public class EventsDetection : MonoBehaviour
{
    [SerializeField] private PlayableDirector ballPassBlock_playable;
    [SerializeField] private ball ballScript;
    private Collider2D col;
    private int count = 0; // no se repita en el tiempo o si hay varios enemigos se repita correctamente
    
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball") & ballScript.isBallFlying & count == 0) //Animacion de intentar cortar balón, TO DO: poner resto de animaciones en el switch
        {
            int contador = count+1;
            count=contador;
            Debug.Log(count);
            int target = Mathf.FloorToInt(Random.Range(0f, 3f));
            Debug.Log("ballPassBlock");

            switch (target)
            {
                    case 0:
                        ballPassBlock_playable.Play();
                        break;
                    case 1:
                        ballPassBlock_playable.Play();
                        break;
                    case 2:
                        ballPassBlock_playable.Play();
                        break;
            }

            PauseManager.Instance.StopGame();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ballScript.Encounters = count;
        count = 0;
       
    }

    
}
