using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    public UnityEvent triggerEvent;
    

    // El objeto que lleve el detector debe ser static o kinematic porque ball es trigger
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
          triggerEvent.Invoke();
          
        }

       
    }


}
