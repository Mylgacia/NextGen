using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EstadosMovimiento
    {
        Esperando,
        Siguiendo,
        Volviendo,
        Batalla, //Llamado desde el manager del jugador
        Posesion, //Llamado desde el manager del jugador
        Ocupado, //Llamado si ya hay un enemy combatiendo
        Venganza, //Ya te regatearon, paquete! NO USADO
        EnCola, //No se superpongan dos enfrentamientos o llegue al área chica o tire
    }
    //PONER BOOL PARA DESACTIVAR CUANDO EMPIECE LA BATALLA CON EL PLAYER Y ACTIVAR CON UN WAITFORSECONDS O QUE TENGA LA POSESION DE PELOTA

public class SeguirJugadorArea : MonoBehaviour
{
    public float radioBusqueda;
    public LayerMask capaJugador;
    public Transform transformJugador;
    public float velocidadMovimiento;
    public float distanciaMaxima;
    public Vector3 puntoInicial;
    public bool mirandoDerecha;
    public float distanciaBatalla;
    public EstadosMovimiento estadoActual;
    public Animator animator;
    private Collider2D col2D;

    [Header("Batalla")]

    public GameObject stunned;
    public Rival_SO enemySO;
   
    [Header("Posesion")] 
    
    [SerializeField] private Transform targetPoint;
    [SerializeField] private GameObject enemyToPass;
    [SerializeField] private string tag = "Enemy"; //Cada enemigo tiene un tag diferente, sistema de deteccion en el futuro
   
    [Header ("Pelota")]
    
    [SerializeField] private Animator ballAnimator;
    [SerializeField] private ball ballScript;
    [SerializeField] private Vector3 enemyMovement;
    [SerializeField] private GameObject ball;
    
    public bool isNoBall = true; //No pelota disponible
    
    public float tiempoReaccion = 1f;
   

    private void Awake()
    {
       col2D = GetComponent<Collider2D>();
       
    }

    private void Start()
    {
        puntoInicial = transform.position;
        
    }

    private void Update()
    {
        switch (estadoActual)
        {
            case EstadosMovimiento.Esperando:
                EstadoEsperando();
                break;
            case EstadosMovimiento.Siguiendo:
                EstadoSiguiendo();
                break;
            case EstadosMovimiento.Volviendo:
                EstadoVolviendo();
                break;
            case EstadosMovimiento.Batalla:
                EstadoBatalla();
                break;
            case EstadosMovimiento.Ocupado:
                EstadoOcupado();
                break;
            case EstadosMovimiento.Venganza:
                EstadoVenganza();
                break;
            case EstadosMovimiento.Posesion:
                EstadoPosesion();
                break;
            case EstadosMovimiento.EnCola:
                EstadoEnCola();
                break;
                
        }
    }

    private void EstadoEsperando()
    {
        
        StopAllCoroutines();
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);

        if (jugadorCollider & BattleManager.BattleInstance.isBall)
        {
            transformJugador = jugadorCollider.transform;

            estadoActual = EstadosMovimiento.Siguiendo;
            
            
        }
        
        
    }

    private void EstadoSiguiendo()
    {
        col2D.enabled = true;
        if (transformJugador == null || BattleManager.BattleInstance.isBall == false )
        {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }

        if (BattleManager.BattleInstance.isEnemyFar == false || BattleManager.BattleInstance.states == BattleState.BattleAnimation)
        {
            estadoActual = EstadosMovimiento.EnCola;
            
        }else transform.position = Vector2.MoveTowards(transform.position, transformJugador.position, velocidadMovimiento * Time.deltaTime);
        

        GirarAObjetivo(transformJugador.position);

        if ((Vector2.Distance(transform.position, puntoInicial) > distanciaMaxima) || (Vector2.Distance(transform.position, transformJugador.position) > distanciaMaxima))
        {
            estadoActual = EstadosMovimiento.Volviendo;
            //transformJugador = null;
        }
        
         
        if (Vector2.Distance(transform.position, transformJugador.position) < distanciaBatalla & !ballScript.isFree)
        {
          if(BattleManager.BattleInstance.states == BattleState.BattleAnimation)
            {
                estadoActual = EstadosMovimiento.Esperando;
                return;
            }
           estadoActual = EstadosMovimiento.Batalla;

          
           

            // transformJugador = null;
        }
        
        
    }

    private void EstadoVolviendo()
    {
        col2D.enabled = false;
        transform.position = Vector2.MoveTowards(transform.position, puntoInicial, velocidadMovimiento * Time.deltaTime);

        GirarAObjetivo(puntoInicial);
        
        
        /*if (transformJugador != null)
        {
            EstadoSiguiendo();
            return;
        }*/
        
        
        if (Vector2.Distance(transform.position, puntoInicial) < 0.1f)
        {
            estadoActual = EstadosMovimiento.Esperando;
            Girar();

        }
        
        
    }
    
    private void EstadoBatalla()
    {
       if(!ballScript.isLosed) { estadoActual = EstadosMovimiento.Volviendo; return; }
       
        BattleManager.BattleInstance.isBattle = true;
        BattleManager.BattleInstance.EstadoPreBatalla();
        BattleManager.BattleInstance.rivalSO = enemySO;
        BattleManager.BattleInstance.comTarget = this.gameObject;
        
        estadoActual = EstadosMovimiento.Ocupado;
        
        


    }

    private void EstadoOcupado() //Estado de reposo SIN ANIMACION
    {
        if (ballScript.isFree & ballScript.isLosed & BattleManager.BattleInstance.states != BattleState.BattleAnimation)
        {
            estadoActual = EstadosMovimiento.Esperando;

        }


    }

    private void EstadoEnCola() //Estado de espera con animacion isWaiting
    {
        col2D.enabled = false;
        if (BattleManager.BattleInstance.isEnemyFar & !ballScript.isFree)
        {
            estadoActual = EstadosMovimiento.Siguiendo;
            
        }
    }

    private void EstadoPosesion() //Cuando recupere la pelota, manejará la pelota
    {
        
        if (!isNoBall) //SI TIENE LA PELOTA
        {
            //Primero tendrá que ir al estado idle o run se supone
                   if (BattleManager.BattleInstance.isBattle & Vector2.Distance(transform.position, transformJugador.position) < distanciaBatalla)
                   {
                       StopAllCoroutines();
                       estadoActual = EstadosMovimiento.Batalla;
                       // transformJugador = null;
                   }
                   //Si el enemy se mueve pasa de Idle a Rival_Run
                   transform.position = Vector2.MoveTowards(transform.position, targetPoint.position,
                       velocidadMovimiento * Time.deltaTime);
                   
                  EnemyMove();

        }
        else //Movimientos sin pelota pero con posesion el equipoCom
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position,
                velocidadMovimiento * Time.deltaTime);
           
        }
       
        
        
       
    }

    private void EstadoVenganza() //NO USADO
    {
       
        //SEGUIR HASTA LA MUERTE!! QUE NO ESCAPE
        GirarAObjetivo(transformJugador.position);
       
        if (Vector2.Distance(transform.position, transformJugador.position) > 0.3f)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, transformJugador.position, velocidadMovimiento * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, transformJugador.position) < 0.3f)
        {
            //animator.SetTrigger("Tackling");
           /* if (BattleManager.BattleInstance.states != BattleState.NoBattle)
            {
                estadoActual = EstadosMovimiento.EnCola;
                return;
            } */
           
            estadoActual = EstadosMovimiento.Batalla;
            

            //Y DEPENDIENDO DE LA TIRADA, probabilidad 75%  (estado Ocupado y StartCoroutine("Stunned"));, 15% quitar la pelota (estado Posesion), 10% hacer falta (NO IMPLEMENTADA) 


        }
        


    }
    

    private void GirarAObjetivo(Vector3 objetivo)
    {
        if (objetivo.x > transform.position.x && !mirandoDerecha)
        {
            Girar();
        }
        else if (objetivo.x < transform.position.x && mirandoDerecha)
        {
            Girar();
        }
    }

    public void CheckLooking() 
    {
        if (mirandoDerecha)
        {
            Girar();
        }
    
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
        Gizmos.DrawWireSphere(transform.position, distanciaBatalla);
    }
    
    private void LateUpdate() //aquí se ejecutan mejor las animaciones sin cálculos, estos SetBools en concreto funcionan como TriggersActions
    {
        
       
        animator.SetBool("isFollowing", estadoActual == EstadosMovimiento.Siguiendo || estadoActual == EstadosMovimiento.Volviendo || estadoActual == EstadosMovimiento.Venganza) ;
        animator.SetBool("isWaiting", estadoActual == EstadosMovimiento.Esperando || estadoActual == EstadosMovimiento.EnCola);
        animator.SetBool("isBattling", estadoActual == EstadosMovimiento.Batalla);
        animator.SetBool("isPossessing", estadoActual == EstadosMovimiento.Posesion);

    }
    
    public IEnumerator Tackling()
    {
        yield return new WaitForSeconds(0.5f);
        ballScript.isCatchable = true;
        yield return new WaitForSeconds(1f);
        GirarAObjetivo(transformJugador.position);
        
        animator.SetTrigger("Tackling");
        
        yield return new WaitForSeconds(1.2f);
      
        ballScript.isCatchable = false;

        if (!isNoBall)
        {
            Posesion();
            
        }
        /*if (ballScript._enemyScript == null)
        {
            StartCoroutine("Stunned");
        }*/

    }

    public IEnumerator Stunned()
    {
        
        if (ballScript.EnemyScript != null) //Si robó la pelota no se irá a Stunned e ira al EstadoPosesion
        { 
            BattleManager.BattleInstance.isEnemyFar = true;
            yield break;
        }
        animator.SetBool("isDefeated",true);
        stunned.SetActive(true);
        BattleManager.BattleInstance.isEnemyFar = true;
        
        yield return new WaitForSeconds(2f);
        
        animator.SetBool("isDefeated",false);
        stunned.SetActive(false);

        animator.SetBool("isBattling", false);
        estadoActual = EstadosMovimiento.Volviendo;
        


    }
    

    public void Posesion() //Se llama a sí misma cada cierto tiempo, MAGIA!!!
    {
        if (BattleManager.BattleInstance.BallScript.isFree & BattleManager.BattleInstance.BallScript.isLosed)
        {
            isNoBall = true;
        }
        
        if (!isNoBall)//osea si tiene la pelota
        {
           int movimiento = Random.Range(1, 5); //TO DO: opcion de tirar a puerta si cerca portería
           Debug.Log("Movimiento del enemigo"+movimiento);
                    if (movimiento == 1)
                    {
                        
                        ballAnimator.SetBool("IsMoving", false);
                        estadoActual = EstadosMovimiento.Ocupado; //Los cambios de estado primero
                        GirarAObjetivo(ball.transform.position);
                        animator.SetBool("isWaiting",true);
                       
                    }
                    if (movimiento == 2)
                    {
                        estadoActual = EstadosMovimiento.Posesion;
                        
                        GirarAObjetivo(targetPoint.transform.position);
                        BallEnemyMove();
                        
                        
                       
                        
                    }
                    
                    if (movimiento == 3)
                    { 
                        estadoActual = EstadosMovimiento.Posesion;
                       
                        GirarAObjetivo(targetPoint.transform.position);
                        BallEnemyMove();
                        
                        
                    }
                    if (movimiento == 4 & !isNoBall)
                    {   
                        ballAnimator.SetBool("IsMoving", false);
                        estadoActual = EstadosMovimiento.Ocupado;
                        GirarAObjetivo(ball.transform.position);
                         
                        
                        animator.SetBool("isWaiting",true);
                        
                        StartCoroutine("Pass");
                    }
                    
                   
                    Invoke("Posesion", tiempoReaccion);
        }
        else
        {
            Invoke("Restart",2f);
            estadoActual = EstadosMovimiento.Volviendo;
            
        }
        

    }
    private IEnumerator Pass() //EN FUTURO SERA UNA ORDEN GLOBAL Y DEVOLVERÁ ESTADO ATACANDO
    {
        enemyToPass = GameObject.FindGameObjectWithTag(tag);
        
        ball.transform.SetParent( null);
        animator.SetTrigger("Passing");
        
        
        yield return null;
    }

    public void pass()
    { 
        BattleAnimation.BattleAnim.BallJump(enemyToPass.transform);
         isNoBall = true; //El enemigo actual deja de tener la pelota, ya sea por pérdida o por pasar o tirar
         
         ballAnimator.SetTrigger("NormalPass");
    }

    public void GetBallAnim() //Cogemos el animator de la pelota
    {
        ballAnimator = ballScript.ballAnimator;
    }

    private void EnemyMove()
    {
        enemyMovement = Vector3.zero;
        enemyMovement.x = transform.position.x; //PARA VER DIRECCION DEL ENEMY Y ACTUAR EN CONSECUENCIA LA PELOTA
        enemyMovement.x = transform.position.y;
        

    }

    private void BallEnemyMove()
    {
        ballAnimator.SetFloat("moveX", enemyMovement.x);
        ballAnimator.SetFloat("moveY", enemyMovement.y);
        ballAnimator.SetBool("IsMoving", true);
    }

    public void BallPassPosition() //En este momento temporal ya está sin el parent enemy
    {
        ball.transform.localPosition = ball.transform.position + new Vector3(0.25f, 0.0f, 0f);
        /*if (ball.transform.position.x < enemyToPass.transform.position.x)
        {
            ball.transform.localPosition = ball.transform.position + new Vector3(0.25f, 0.0f, 0f);
        }
        else
        {
            ball.transform.localPosition = ball.transform.position + new Vector3(-0.1f, 0.0f, 0f);
        }*/
       
    }

    private void BallParentEnemy() //NO USADO
    {
        if (enemyMovement.x > 0)
        {
            ball.transform.localPosition = new Vector3(0.13f, -0.11f, 0f) ;
        }
        if (enemyMovement.x < 0)
        {
            ball.transform.localPosition = new Vector3(-0.09f, -0.11f, 0f);
        }
        if (enemyMovement.y > 0 & enemyMovement.x == 0)
        {
            ball.transform.localPosition = new Vector3(0.06f, -0.05f, 0f);
        }
        if (enemyMovement.y < 0 & enemyMovement.x == 0)
        {
            ball.transform.localPosition = new Vector3(0f, -0.2f, 0f);
        }
        
    }

    public void TacklingSound()
    {
        AudioManager.instance.PlayMatchSounds("TacklingGrave");
    }

    public void Restart()
    {
       
        BattleManager.BattleInstance.StartCoroutine("RestartGame");
    }
}
