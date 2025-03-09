using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//using Input = UnityEngine.Windows.Input;

public class GolesUI : MonoBehaviour //TRANSFORMAR EN SINGLETON O STATIC MANAGER
{
    public List<Image> goalsList;

    public GameObject goalPrefab;

    public int indexCount;
    
    public Sprite goal;

    

    private void Awake()
    {
        
        
    }

    // Start is called before the first frame update
    void Start()
    
    {
        if (indexCount != 0)
        {
            indexCount = 0;
            goalsList.Clear();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SumarGoles(int cantidadGoles)
    {
        for (int i = 0; i < cantidadGoles; i++)
        {
            GameObject goal = Instantiate(goalPrefab, transform);
            goalsList.Add(goal.GetComponent<Image>());
            
        }


        indexCount = goalsList.Count;
    }

    public void GoalScore()
    {
      SumarGoles(1);


    }
}
