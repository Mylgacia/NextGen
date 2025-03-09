
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.IO;
using Unity.VisualScripting;

public class Draft : MonoBehaviour //SISTEMA PARA ELEGIR ENEMIGOS AL AZAR
{
    //[SerializeField] private MainsEnemySO[] enemies;
    public MainsEnemySO[] myArray;
    public MainsEnemySO mySo;
    public EnemySelected enemy;
    //private bool firstTime=true;
    
    public int current;
    public int activeBox;
    public GameObject[] Enemies;
    [SerializeField] private List<int> enemiesID = new List<int>(); //para guardar los numeros u orden de los enemigos
    public List<int> EnemiesID => enemiesID;
    
    public int EnemysWanted;
    [SerializeField] private SceneController endScene;
    
    
    
    IEnumerator ChooseEnemy()
    {
        for (int i = 0; i < EnemysWanted; i++)
        {
            current = Random.Range(0,myArray.Length); 
            Check();
            
            yield return new WaitForSeconds(1); //AL PONERLO AQUI HACE QUE ESPERE X SEGUNDOS CADA TIRADA
        }

        endScene.AgentFinal();
    } 
    
    public void Check()
    {
        for (int j = 0; j < enemiesID.Count; j++) //Check si se repite mismo enemigo ID
        {
            if(current == enemiesID[j])
            {
                Debug.Log(current + "Entro en check");
               current = Random.Range(0,myArray.Length);
               ReCheck();
            }
           
            
        }
        
        enemiesID.Add(current);
        mySo = myArray[current];

        //Debug.Log("ID"+ enemiesID.Count);
            
        enemy.enemySO = mySo;
        Enemies[activeBox].SetActive(true);
        activeBox++;
        
        
    }

    public void ReCheck()
    {
        for (int j = 0; j < enemiesID.Count; j++) //Check si se repite mismo enemigo ID
        {
            if(current == enemiesID[j])
            {
                Debug.Log(current + "Entro en Recheck");
                current = Random.Range(0,myArray.Length);
                ReCheck();
            }
            
            
        }
    }
    


    
}
