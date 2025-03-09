using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EstadosPortero
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
public class Gk_Catch : MonoBehaviour
{
    [SerializeField] private GkMove gk;
    public float radioBusqueda;
    public LayerMask capaJugador;
    public Transform transformJugador;
    public float velocidadMovimiento;
    public float distanciaMaxima;
    public Vector3 puntoInicial;
    public bool mirandoDerecha;
    public float distanciaBatalla;

    public EstadosPortero estadoActual;

    [Header("Event")]
    [SerializeField] private GameObject gkEventCatch;
    [SerializeField] private GameObject ballGhost;
    private void Awake()
    {
        puntoInicial = transform.position;
        gk = GetComponent<GkMove>();
    }

    private void Start()
    {
        EstadoEsperando();   
    }

    private void Update()
    {
        switch (estadoActual)
        {
            case EstadosPortero.Esperando:
                EstadoEsperando();
                break;
            case EstadosPortero.Siguiendo:
                EstadoSiguiendo();
                break;
            case EstadosPortero.Volviendo:
                EstadoVolviendo();
                break;
            /*case EstadosPortero.Batalla:
                EstadoBatalla();
                break;*/
            case EstadosPortero.Ocupado:
                EstadoOcupado();
                break;
            case EstadosPortero.Posesion:
                EstadoPosesion();
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
            transformJugador = transformJugador.GetChild(6).GetChild(0);
            estadoActual = EstadosPortero.Siguiendo;


        }


    }

    private void EstadoSiguiendo()
    {
        PauseManager.Instance.isInputPaused = true;
        gk.GKAnim.applyRootMotion = true;
        BattleManager.BattleInstance.isBall = false;
        BattleManager.BattleInstance.PlayerMove._animator.SetBool("moving", false);
        BattleManager.BattleInstance.BallScript.ballAnimator.SetBool("IsMoving", false);
        BattleManager.BattleInstance.isEnemyFar = false;
        transform.position = Vector2.MoveTowards(transform.position, transformJugador.position , velocidadMovimiento * Time.deltaTime);

        if (Vector2.Distance(transform.position, transformJugador.position) < distanciaBatalla) //Se tira a sus pies
        {
            StartCoroutine(Catch());
            
            BattleManager.BattleInstance.isEnemyFar = false;
            estadoActual = EstadosPortero.Ocupado;
            
        }
    }

    private void EstadoBatalla()
    {
    
    }

    private void EstadoVolviendo()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntoInicial, velocidadMovimiento * Time.deltaTime);
        if (Vector2.Distance(transform.position, puntoInicial) < 0.01f)
        {
            
            estadoActual = EstadosPortero.Esperando;
            
        }

    }

    private void EstadoOcupado()
    {
        //gk.GKAnim.SetBool("isFollowing", false);
    }

    private void EstadoPosesion()
    {
        
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
        gk.GKAnim.SetBool("isFollowing", estadoActual == EstadosPortero.Siguiendo || estadoActual == EstadosPortero.Volviendo);


    }

    private IEnumerator Catch() 
    {
        gk.GKAnim.SetTrigger("catch");
        //gk.Catching();
        yield return new WaitForSeconds(0.2f);
        gkEventCatch.SetActive(true);
        yield return new WaitForSeconds(4f);
        gk.Ball.SetActive(false);
        gkEventCatch.SetActive(false);
        gk.GKAnim.SetTrigger("kickOut");
        ballGhost.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        gkEventCatch.SetActive(false);
        ballGhost.SetActive(false);
        BattleManager.BattleInstance.StartCoroutine("RestartGame");
        yield return new WaitForSeconds(1f);
        estadoActual = EstadosPortero.Volviendo;
    }

  
}
