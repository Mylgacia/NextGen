using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerRecords : MonoBehaviour
{
    [SerializeField] private SO_Counter SOcounter;
    [SerializeField] private TextMeshProUGUI goals;
    [SerializeField] private int goalsInt = 0; //Iniciar con valor guardado durante la temporada, ahora solo se iguala a O como ejemplo de funcionalidad

    [SerializeField] private TextMeshProUGUI goalAssists;
    [SerializeField] private int goalAssitsInt = 0;

    [SerializeField] private TextMeshProUGUI dribbles;
    [SerializeField] private int dribblesInt = 0;
    
   
    private void Awake()
    {
        goalsInt = int.Parse(goals.text);
        goalAssitsInt = int.Parse(goalAssists.text);
        dribblesInt = int.Parse(dribbles.text);
        

    }
    void Start()
    {
        goalsInt += SOcounter.Goals;
        goalAssitsInt += SOcounter.GoalAssists;
        dribblesInt += SOcounter.Dribbles;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
