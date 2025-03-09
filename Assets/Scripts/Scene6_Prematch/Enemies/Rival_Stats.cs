using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rival_Stats : MonoBehaviour
{
    [SerializeField] private Rival_SO _selectedRival;

    public Rival_SO SelectedRival
    {
       get => _selectedRival;
       set => _selectedRival = value;  
        
    }
   
   
}
