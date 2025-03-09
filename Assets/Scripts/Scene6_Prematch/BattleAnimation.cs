
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Cinemachine;


public class BattleAnimation: MonoBehaviour
{
    public static BattleAnimation BattleAnim { get; private set; }
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform comTransform;
    [SerializeField] private Transform offsetpivot;
    [SerializeField] private Transform ballTransform;
    [SerializeField] private Vector3 offsetCom = new Vector3(0f, 0f, 0f); //Establecer distancia en el inspector
    [SerializeField] private ball ballScript;
    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private MovsUI _movsUI;
    [SerializeField] private Effects effects;
        
    public MovsUI Movs 
    {
        get =>_movsUI;
        set => _movsUI = value;
    } 
     
    [Header ("Camera")]
    [SerializeField] private CinemachineVirtualCamera cm;
    [SerializeField] private GameObject UIPlayerPanel;

   

    private float zoom;
    private float zoomSpeed = 3f;
    private float finalZoom = 0.8f;
    private float zoomMultiplier = 4f;
    private float minZoom = 2f;
    private float maxZoom = 8f;
    private float velocity = 0.0f;
    private float smoothTime = 0.25f;

    private void Awake()          
    {
      if (BattleAnim == null)
      {
          BattleAnim = this;
      } else Destroy(gameObject);
      
    }

    private void Update()
    {
       
    }


    public IEnumerator AnimPositions(Transform com)
    {   
        //Guardamos primero posicion antigua
        comTransform = com.transform;
       // playerTransform = GetComponent<Transform>().transform;
       
        //Quitamos paneles, ajustamos camara y accionamos Time
       BattleManager.BattleInstance.battleBox.SetActive(false);
       Time.timeScale = 1f;
       playerMove.StopPosition();
       
        playerTransform.DOMove(offsetpivot.position, 1f);
      
       ballTransform.localPosition = new Vector3(0.16f, -0.11f, 0f); 
       comTransform.DOMove(offsetpivot.position + offsetCom, 1f);
       
       
       yield return new WaitForSeconds(1f);
       comTransform.GetComponent<SeguirJugadorArea>().CheckLooking();

    }

    public IEnumerator BackPositions()
    { 
        //Volvemos a antes de batalla
        ballScript.isCatchable = true;
        yield return new WaitForSeconds(0.8f);
        
        playerMove.Go();
        UIPlayerPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        ballScript.isCatchable = false;
    }

    public void Zoom() //USE EL CINEMACHINE EN LUGAR DE ESTO
    {
        if (cm.m_Lens.OrthographicSize < 1.2f)
        {
            cm.m_Lens.OrthographicSize = 1.2f;
            
        } else cm.m_Lens.OrthographicSize = 0.8f;
       // cm.m_Lens.OrthographicSize = Mathf.SmoothDamp(cm.m_Lens.OrthographicSize, finalZoom, ref velocity, smoothTime);
        //cm.m_Lens.OrthographicSize = Mathf.Lerp(cm.m_Lens.OrthographicSize, finalZoom, Time.deltaTime * zoomSpeed);
    }

    public void ShotWin(int result)
    {
        if(result > 0)
        {
            //Animacion tiro
            StartCoroutine("NormalShot");
            Debug.Log("Hacer tiro");
        } else
        {
            //Animacion de stun y pierde posesion
        }
        //yield return new WaitForSeconds(1);
    }
    
    public void DribleWin(int result)
    {
        
        if(result >= 0)
        { 
            //Animacion drible
            StartCoroutine("NormalDrible");
            comTransform.gameObject.GetComponent<SeguirJugadorArea>().StartCoroutine("Tackling");
            
            
        } else
        {//Animacion de stun y pierde posesion
            
        }
        //yield return new WaitForSeconds(1);
    }
    public void PassWin(int result)
    {
        if(result > 0)
        {
            
            //Animacion pase
        } else
        {//Animacion de stun y pierde posesion
           
        }
        //yield return new WaitForSeconds(1);
    }
    
    

    public void SuperShot(int result) //Va a salir +0 en esta Demo siempre
    {
        //var movetype = _movsUI._selectedMov;
        _movsUI = BattleManager.BattleInstance._movsUI;
        var movetype = _movsUI.Type;
        ballScript.isElementalHit = true;

        if (result > 0)
        {
             switch (movetype)
                    {
                        case MoveBase.Type.Fire:
                            
                            StartCoroutine("SuperShotAnim");
                            playerMove._animator.SetTrigger("FireSuperShot");
                            Debug.Log("SuperTiro de Fuego");
                            break;
                        
                        case MoveBase.Type.Water:
                            StartCoroutine("SuperShotAnim");
                            playerMove._animator.SetTrigger("WaterSuperShot");
                            Debug.Log("SuperTiro de Agua");
                            
                            break;
                        
                        case MoveBase.Type.Electric:
                            StartCoroutine("SuperShotAnim");
                            playerMove._animator.SetTrigger("ElectricSuperShot");
                            Debug.Log("SuperTiro Eléctrico");
                            break;
                        
                        case MoveBase.Type.Ground:
                             StartCoroutine("SuperShotAnim");
                             playerMove._animator.SetTrigger("GroundSuperShot");
                             Debug.Log("SuperTiro de Tierra");
                            break;
                        
                        case MoveBase.Type.Dark:
                            
                            break;
                        case MoveBase.Type.Light:
                            
                            break;
                        case MoveBase.Type.None:
                            
                            break;
                    }
            
        } else {} //Animacion perdiendo pelota o encontronazo o pifia

       
        
    }
    public void SuperDrible(int result)
    {
        
        //var movetype = _movsUI._selectedMov;
        _movsUI = BattleManager.BattleInstance._movsUI;
        var movetype = _movsUI.Type;
        ballScript.isElementalHit = true;

        if (result > 0)
        {
            BattleManager.BattleInstance.DribbleCounter();
            switch (movetype)
            {
                case MoveBase.Type.Fire:

                    StartCoroutine("SuperDribleAnim");
                    playerMove._animator.SetTrigger("FireSuperDrible");
                    Debug.Log("SuperD de Fuego");
                    break;

                case MoveBase.Type.Water:
                    StartCoroutine("SuperDribleAnim");
                    playerMove._animator.SetTrigger("WaterSuperDrible");
                    Debug.Log("SuperD de Agua");

                    break;

                case MoveBase.Type.Electric:
                    StartCoroutine("SuperDribleAnim");
                    playerMove._animator.SetTrigger("ElectricSuperDrible");
                    Debug.Log("SuperD Eléctrico");
                    break;

                case MoveBase.Type.Ground:
                    StartCoroutine("SuperDribleAnim");
                    playerMove._animator.SetTrigger("GroundSuperDrible");
                    Debug.Log("SuperD de Tierra");
                    break;

                case MoveBase.Type.Dark:

                    break;
                case MoveBase.Type.Light:

                    break;
                case MoveBase.Type.None:

                    break;
            }

        }
        else { } //Animacion perdiendo pelota o encontronazo o pifia, EN ESTA DEMO ES IMPARABLE PARA VER ANIMACION


    }


    private IEnumerator NormalShot()
    {
        
        BattleManager.BattleInstance.battleBox.SetActive(false);
        playerMove._animator.SetTrigger("NormalShot");
        playerMove._animator.SetBool("moving",false);
        ballScript.StartCoroutine("Shoot");
        ballScript.transform.SetParent( null);
        
        yield return new WaitForSeconds(4);
        playerMove._animator.SetTrigger("afterShot");
        
    }
    
    private IEnumerator SuperShotAnim()
    {
        UIPlayerPanel.SetActive(false);
        ballTransform.localPosition = new Vector3(0.12f, -0.12f, 0f);
        BattleManager.BattleInstance.battleBox.SetActive(false);
        ballScript.ballAnimator.SetBool("IsMoving", false);
        playerMove._animator.SetBool("moving",false);
        ballScript.isFree = true;

        yield return new WaitForSeconds(1f);
        ballScript.transform.SetParent(null);
        
        yield return new WaitForSeconds(8);

        UIPlayerPanel.SetActive(true);
        playerMove._animator.SetTrigger("noShot");
        ballScript.isElementalHit = false;

    }
   
    
    private IEnumerator NormalDrible()
    {
       
        playerMove._animator.SetTrigger("NormalDrible");
        
        yield return new WaitForSeconds(1);
        
        StartCoroutine("BackPositions");
        

        //Llamar nueva posición tras regate
    }

    private IEnumerator SuperDribleAnim()
    {
        UIPlayerPanel.SetActive(false);
        BattleManager.BattleInstance.battleBox.SetActive(false);

        playerMove._animator.SetBool("moving", false);
       // ballScript.transform.SetParent(null);

        yield return new WaitForSeconds(3.3f);
        //playerTransform.DOMove(playerTransform.transform.position + Vector3.right,0f);
        //playerTransform.position = comTransform.position+new Vector3(0.2f,0f,0f);
        playerTransform.DOMove(comTransform.transform.position + new Vector3(0.2f, 0f, 0f),0f);
        playerMove._animator.SetTrigger("go");
        effects.BallNoGhost();

        yield return new WaitForSeconds(0.1f);
        
        comTransform.GetComponent<SeguirJugadorArea>().StartCoroutine("Stunned");
        PauseManager.Instance.Pause();
        ballScript.isElementalHit = false;
        UIPlayerPanel.SetActive(true);
        BattleManager.BattleInstance.comTarget = null;
        BattleManager.BattleInstance.isBattle = false;
        BattleManager.BattleInstance.RestartShadows();
        BattleManager.BattleInstance.StartCoroutine("EstadoNoBatalla");

    }

    public void BallJump(Transform target) //Traslacion de la pelota de un enemigo a otro, PASS
    {
        Transform jumper = ballScript.gameObject.transform;
        
        //var sequence = DOTween.Sequence(); //con secuencia puedo manejar mejor el tiempo de ejecucion
        
        
        //sequence.Insert(1f, cm.transform.DOMoveY(target, CamPosDuration));
        //sequence.Insert(3f,title.DOFade(titleFadeValue, titleFadeDuration).SetEase(Ease.InQuint)); //to do: Tipo de Fading
        //sequence.OnComplete(() => InitialMenu()); Cuando termine que enemy2 haga control;
        
        jumper.DOLocalJump(target.position - new Vector3(0.1f,0.1f,0), 0.5f, 1, 2f).SetEase(Ease.InQuad);
        //_jumper.DOJump(target.transform.position, 0.01f, 1, 0.5f).SetEase(Ease.OutSine);

        
    }

    
    
}
