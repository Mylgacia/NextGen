using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GkMove : MonoBehaviour
{
    
    [SerializeField] private Rival_SO enemySO;
    [SerializeField] private ball ballScript;
    [SerializeField] private GameObject _ball;
    public GameObject Ball { get { return _ball; } }    
    
    private Collider2D col;
    private Animator gkAnim;
    public Animator GKAnim
    { 
        get => gkAnim; set => gkAnim = value;
    }
    
    [Header("SaveEvent")]
    [SerializeField] private GameObject gkSaveEvent;
    [SerializeField] private Animator gkDirection;
    [SerializeField] private Animator ballDirection;
    [SerializeField] private GameObject outBallText;
    [SerializeField] private GameObject catchingText;

    [Header("GoalScore")]
    [SerializeField] private GolesUI golesUI;


    private void Awake()
    {
        col = GetComponent<Collider2D>();
        gkAnim = GetComponent<Animator>();
        //gkDirection = GetComponent<Animator>(); //GUARDAR PARA PREFABS TEAMS
       // ballDirection = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball") & ballScript.isBallFlying )
        {
            //TO DO: Poner un if con % dependiente de la suerte del jugador y del accuary del movimiento especial para dar en el POSTE o la distancia a porteria
            
             //PauseManager.Instance.StopGame();
             
             

            if (ballScript.isElementalHit)
            { 
                BattleManager.BattleInstance.Gk_SuperResolution(enemySO); // Mando el SO del portero para calcular luego la efectividad del type respecto al movimiento del jugador TABLA ELEMENTAL
               
                switch (ballScript.FinalTarget)
                            {
                                
                                case 0: //toptarget
                                    var GkResult0 =
                                        enemySO.catched * (enemySO.cut * BattleManager.BattleInstance.GkType1) /1.1 +
                                        ballScript.Encounters * 10;
                                    Debug.Log("Portero saca" + GkResult0);
                                    if (BattleManager.BattleInstance.Result < GkResult0 ) //Si result es 0 o menos gana el portero y realiza parada, sino GOAL
                                    {
                                        _ball.SetActive(false);
                                        gkSaveEvent.SetActive(true);
                                        gkDirection.SetTrigger("left");
                                        ballDirection.SetTrigger("ballLeft");
                                        
                                        Catching();
                                        
                                    } else gkAnim.SetTrigger("failingTop");//LEFT
                                           
                                           break;
                                case 1: //downtarget
                                    var GkResult1 =
                                        enemySO.saved * (enemySO.tackle +
                                                         enemySO.cut * BattleManager.BattleInstance.GkType1)/1.7  +
                                        ballScript.Encounters * 10;
                                    Debug.Log("Portero saca" +GkResult1);
                                    if (BattleManager.BattleInstance.Result < GkResult1 ) //Si result es 0 o menos gana el portero y realiza parada, sino GOAL
                                    {
                                        _ball.SetActive(false);
                                         gkSaveEvent.SetActive(true);
                                        gkDirection.SetTrigger("right");
                                        ballDirection.SetTrigger("ballRight");
                                         AudioManager.instance.PlayMatchSounds("GkSave");
                                        Out();

                                    } else gkAnim.SetTrigger("failingDown");//RIGHT
                                           
                                          break;
                                case 2: //uptarget
                                    var GkResult2 =
                                        enemySO.saved * (enemySO.tackle + enemySO.cut * BattleManager.BattleInstance.GkType1) /1.8 +
                                        ballScript.Encounters * 20;
                                    Debug.Log("Portero saca" +GkResult2);
                                    if (BattleManager.BattleInstance.Result < GkResult2) //Si result es 0 o menos gana el portero y realiza parada, sino GOAL y se tira el portero fallando
                                    {
                                        _ball.SetActive(false);
                                        gkSaveEvent.SetActive(true);
                                        gkDirection.SetTrigger("up");
                                        ballDirection.SetTrigger("ballUp");
                                         AudioManager.instance.PlayMatchSounds("GkSave");
                                         Out();

                                    } else gkAnim.SetTrigger("failingUp");//UP
                                        
                                        break;
                            }
                
                
            }
            else
            {
                Debug.Log("entre");
                switch (ballScript.FinalTarget)
                {
                   
                    case 0: //toptarget
                        var GkResult0 = enemySO.cut * (enemySO.tackle + enemySO.cut) + ballScript.Encounters * 2;
                        Debug.Log("Portero saca" + GkResult0);
                        if (BattleManager.BattleInstance.Result < GkResult0) //Si result es 0 o menos gana el portero y realiza parada, sino GOAL
                        {
                            
                            gkSaveEvent.SetActive(true);   
                            gkDirection.SetTrigger("left");
                            ballDirection.SetTrigger("ballLeft");
                            _ball.SetActive(false);
                            Catching();
                            
                            

                        } else gkAnim.SetTrigger("failingTop");//LEFT
                               
                        break;
                    case 1: //downtarget
                        var GkResult1 = 2 * (enemySO.tackle) * (enemySO.cut) + ballScript.Encounters;
                        Debug.Log("Portero saca" + GkResult1);
                        if (BattleManager.BattleInstance.Result < GkResult1) //Si result es 0 o menos gana el portero y realiza parada, sino GOAL
                        {
                            
                            gkSaveEvent.SetActive(true);   
                            gkDirection.SetTrigger("right");
                            ballDirection.SetTrigger("ballRight");
                            _ball.SetActive(false);
                            AudioManager.instance.PlayMatchSounds("GkSave");
                            Out();
                            
                        } else gkAnim.SetTrigger("failingDown");//RIGHT
                               
                        break;
                    case 2: //uptarget
                        var GkResult2 = 2 * enemySO.tackle * (enemySO.tackle) + ballScript.Encounters * 2;
                        Debug.Log("Portero saca" + GkResult2);
                        if (BattleManager.BattleInstance.Result < GkResult2 ) //Si result es 0 o menos gana el portero y realiza parada, sino GOAL
                        {
                            
                            gkSaveEvent.SetActive(true);  
                            gkDirection.SetTrigger("up");
                            ballDirection.SetTrigger("ballUp");
                            _ball.SetActive(false);
                            AudioManager.instance.PlayMatchSounds("GkSave");
                            Out();
                            
                        } else gkAnim.SetTrigger("failingUp");//UP
                               
                        break;
                }
                
                
                
            }
            

           
        }
        
    }

    public void Catching()
    { 
       catchingText.SetActive(true);
    }

   
    
    public void Out()
    { 
        
        outBallText.SetActive(true);
    }

    public void TextReset()
    {
       
        catchingText.SetActive(false);
        outBallText.SetActive(false);
        
        
    }
    
}
