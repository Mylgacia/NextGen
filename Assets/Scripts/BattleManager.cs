

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum BattleState //DISTINTOS ESTADOS DEL JUGADOR PARA TENER UN ORDEN
{
    NoBattle, //Estado inicial donde Pass y Dribble no están disponibles, puede abrirse/cerrarse BattleBox
    WaitingBattle, //Estado NADA o PreBatalla o reposo
    StartingBattle, //Se ejecutará después del submit option, este Estado de una estadistica contra otro y ejecución de animaciones
    BattleAnimation,
    FinishingBattle,
}

public class BattleManager : MonoBehaviour //CONTROLA LA INSTANCIACIÓN DE MENÚS (BATTLEBOX) Y EL POSTERIOR CÁLCULO DE STATS PLAYER-RIVAL + ANIMACION
{
    public static BattleManager BattleInstance { get; private set; }
    public GameObject battleBox;
    public BattleBox battleBoxScript;
    public BattleState states;

    public bool isActivated;
    public bool isBattle;

    [Header("IUSelection")]
    
    [SerializeField] private GameObject iconSubMenuSuperShot;
    [SerializeField] private GameObject iconSubMenuSuperDribble;
    [SerializeField] private GameObject iconSubMenuSuperPass;
    public bool isEnemyFar = true;
    
   
    [Header("Player Stats")]
    
    [SerializeField] private PlayerStats statsSO;
    public Rival_SO rivalSO;
    
    [SerializeField] private MoveBase selectedMove;
   
    public MoveBase SMoveBase
    {
        get => selectedMove;
        set => selectedMove = value;
    }

    [SerializeField] private MovsUI movsUI;
    public MovsUI _movsUI => movsUI;
    
    [SerializeField] private MoveBase specialMove; //If stats.Shoot == 5 por ejemplo, se rellena pero no disponible si no se cumplen ciertas condiciones

    //public int currentPlayerSelection;

    [Header("Battle Animation")] 
    public GameObject comTarget;
    public bool isBall = false;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ball;
    [SerializeField] private ball ballScript;

    public ball BallScript { get { return ballScript; } }

    [SerializeField] private PlayerMovement playerMove;
    public PlayerMovement PlayerMove
    {
        get => playerMove;
        set => playerMove = value;  
    }
    [SerializeField] private int superType;


   [Header("BallEffects")]
    [SerializeField] private List<GameObject> ballEffects;
    [SerializeField] private List<GameObject> shadows;

   public List<GameObject> Lista
    {
        get => ballEffects;
        set => ballEffects = value;
    }

    


    [Header("Goal")] 
    [SerializeField] private int _result;
    [SerializeField] private float GkType;
    [SerializeField] private GkMove gkscript;
    [SerializeField] private Transform gkrival;
    [SerializeField] private GolesUI golesUI;
    [SerializeField] private SO_Counter counter;

 public int Result
    {
        get => _result;
        set => _result = value;
    }

    public float GkType1 => GkType;


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   


    private void Awake()
    {
        if (BattleInstance == null)
        {
            BattleInstance = this;
        } else Destroy(gameObject);
        
        CheckingStats();
        ballEffects = new List<GameObject>();
        counter.Dribbles = 0;
        
       
    }

    private void Start()
    {
        
       
       


    }

    void Update()
    {

        //Si la distancia es corta o pulsa X Boton se abre la BattleBox
        if (states == BattleState.NoBattle & Input.GetKeyDown(KeyCode.M) & isBall
            || states == BattleState.NoBattle & Input.GetKeyDown(KeyCode.Joystick1Button3) & isBall)
        {
            battleBox.SetActive(isActivated = !isActivated); // también se puede battleBox.SetActive(!isActivated.activeInHierarchy);
            battleBoxScript.ButtonDisableColors();
            battleBoxScript.Warning(false);
            battleBoxScript.SpecialBoxS.SetActive(false);

            //PRUEBAS
            
            
        }
        
    }

    private void CheckingStats() //Habilita que al ejecutar por ejemplo TIRAR se abra la ventana y no se ejecute directamente sin ver los movs que tiene el jugador
    {
        
        if (statsSO.Shoot >= 4)
        {
            battleBoxScript.isSuperShotOn = true;
            iconSubMenuSuperShot.SetActive(true);
            //Desbloquear level 5 si se cumplen unas condiciones del campo o charlas o afinidad
        }
        
        if (statsSO.Dribble >= 4)
        {
            battleBoxScript.isSuperDribbleOn = true;
            iconSubMenuSuperDribble.SetActive(true);
            //Desbloquear level 5 si se cumplen unas condiciones del campo o charlas o afinidad
        }
        
        if (statsSO.Pass >= 4)
        {
            battleBoxScript.isSuperPassOn = true;
            iconSubMenuSuperPass.SetActive(true);
            //Desbloquear level 5 si se cumplen unas condiciones del campo o charlas o afinidad
        }
    }

   
   private void EstadoNoBatalla()
   {
       states = BattleState.NoBattle;
       //StopAllCoroutines();
   }

    
    public void EstadoPreBatalla()
    {
        if (isBattle) //El enemigo llamará al isBattle cuando atacamos para abrir menú
        {
            states = BattleState.StartingBattle;
            battleBoxScript.ButtonAvaibleColors();
            battleBox.SetActive(true);
            isEnemyFar = false;
            
        }else states = BattleState.NoBattle;

    }
    

    public void EstadoEmpezandoBatalla(int skillMove) //Seleccionando movs y ejecutando cálculo de stats + Animación
    {
        states = BattleState.StartingBattle;
        superType = skillMove;

        switch (skillMove)
        {
            case 0:

                foreach (Move.LearnableMove movs in statsSO.moves)
                {
                    if (movs.SkillMove == 0)
                    {
                        if(movs.Level != 5)
                        {
                            selectedMove = movs.MMove; 
                            movsUI.UpdateSelectedMove();

                        }
                    }
                }

                break;

            case 1:
                foreach (Move.LearnableMove movs in statsSO.moves)
                {
                    if (movs.SkillMove == 1)
                    {
                        selectedMove = movs.MMove; 
                        movsUI.UpdateSelectedMove();
                    }
                }

                break;

            case 2:
                foreach (Move.LearnableMove movs in statsSO.moves)
                {
                    if (movs.SkillMove == 2)
                    {
                        selectedMove = movs.MMove; 
                        movsUI.UpdateSelectedMove();
                    }
                }

                break;
            
        }

    }
    public void EjecutarMovimientoNormal(int attackMode)
    {
        EstadoAnimacion();
       
        
        //Antes comprobar que el rival no sea el portero if(tag !Gk) y coger ver que RivalSO es el que tiene...
        
        if (comTarget != null)
        {
            StartCoroutine(BattleAnimation.BattleAnim.AnimPositions(comTarget.transform));
             float critical = 1f;
                            if (Random.Range(0f, 100f) < statsSO.Luck)
                            {
                                critical = 1.5f;
                            }
           
            
            float modifiers = Random.Range(1.5f, 1.75f)*critical;
                    Debug.Log(modifiers);
                    
            
                    float playerDamage;
                    float comDamage;
                    int totalDamage;
            
                    switch (attackMode)
                    {
                        case 0: //shot - EL RIVAL ESTA DEMASIADO CERCA!!!
                             
                           /* playerDamage = (20*statsSO.Shoot) * Mathf.FloorToInt(modifiers);
                            comDamage = rivalSO.tackle + rivalSO.cut;
                            totalDamage = Mathf.FloorToInt(playerDamage - comDamage);
                            Debug.Log(totalDamage); // si totalDamage es mayor que 0 gana PLAYER, animación o corrutina de tiro
                            _result = Mathf.FloorToInt(playerDamage);
                            
                            BattleAnimation.BattleAnim.ShotWin(totalDamage);*/
                            break;
                        
                        case 1: //drible - tackle
                            
                            playerDamage = statsSO.Dribble * Mathf.FloorToInt(modifiers);
                            comDamage = rivalSO.tackle;
                            totalDamage = Mathf.FloorToInt(playerDamage - comDamage/2);
                            Debug.Log(totalDamage); // si totalDamage es mayor que 0 gana PLAYER, animación o corrutina de tiro
                            
                            
                            BattleAnimation.BattleAnim.DribleWin(totalDamage);
                            break;
                        
                        case 2: //pass - cut  NO ES POSIBLE EJECUTAR EN ESTA DEMO
                            
                            /*playerDamage = statsSO.Shoot * Mathf.FloorToInt(modifiers);
                            comDamage = rivalSO.tackle + rivalSO.cut;
                            totalDamage = Mathf.FloorToInt(playerDamage - comDamage);
                            Debug.Log(totalDamage); // si totalDamage es mayor que 0 gana PLAYER, animación o corrutina de tiro
                            
                            BattleAnimation.BattleAnim.PassWin(totalDamage);*/
                            break;
                    }
            
                    
                  
            
        }
        else //Tiro o pase sin nadie
        {
            playerMove.StopPosition();
            ballScript.transform.localPosition = new Vector3(0.15f, -0.12f, 0f);

            // playerMove.PlayerMovements = Vector3.right;
            //TO DO:Hacer que player mire a la derecha, input?¿?
            //BlendType Direct
            float critical = 1f;
            if (Random.Range(0f, 100f) < statsSO.Luck)
            {
                critical = 1.5f;
            }

           

            float modifiers = Random.Range(1.5f, 1.75f)*critical;
            Debug.Log(modifiers);
                    
            
            float playerDamage;
            
            int totalDamage;
            
            switch (attackMode)
            {
                case 0: //Shot sin rival
                    var distanceModifier = 1;
                    if (Vector2.Distance(transform.position, gkrival.transform.position) > 1.5f)
                    { distanceModifier = 2; }

                    playerDamage =(10*statsSO.Shoot) * Mathf.FloorToInt(modifiers)/distanceModifier;
                    totalDamage = Mathf.FloorToInt(playerDamage);
                    _result = totalDamage;
                    BattleAnimation.BattleAnim.ShotWin(totalDamage);
                    Debug.Log("Tiro normal saca" + totalDamage);
                    break;
                        
                case 1: //Drible - tackle EN GRIS NO DISPONIBLE AL NO HABER RIVAL
                   
                    break;
                        
                case 2: //Pass - cut  NO ES POSIBLE EJECUTAR EN ESTA DEMO
                    
                    /*playerDamage = statsSO.Shoot * Mathf.FloorToInt(modifiers);
                    comDamage = rivalSO.tackle + rivalSO.cut;
                    totalDamage = Mathf.FloorToInt(playerDamage - comDamage);
                    Debug.Log(totalDamage); // si totalDamage es mayor que 0 gana PLAYER, animación o corrutina de tiro
                            
                    BattleAnimation.BattleAnim.PassWin(totalDamage);*/
                    break;
            }
            
            
        }
        
       
        
    }
    
    
    
    public void EjecutarMovimientoEspecial(int attackMode) //seguramente bool para activar skill level5 cuando condicion
    {
        attackMode = superType; //Al ser un 3 en 1 el boton de mov especial necesito guardar el valor 0,1 o 2 en superType para luego aquí decirle que estoy ejecutando Super tiro,drible o pase
        EstadoAnimacion();
        


        float playerDamage;
        float comDamage;
        int totalDamage;
                     
        
        if (comTarget != null)
        {
            StartCoroutine(BattleAnimation.BattleAnim.AnimPositions(comTarget.transform));

            float powerMove = movsUI._selectedMov.Power;
            float critical = 1f;
            if (Random.Range(0f, 100f) < statsSO.Luck)
            {
                critical = 1.5f;
                
            }

            float distance = 1f;
            if (Vector2.Distance(player.transform.position, gkrival.position) > 1.8f)
            {
                distance = 0.6f;
            }

            float modifiers = Random.Range(1.5f, 1.75f)*critical*powerMove*distance;
            
            Debug.Log(modifiers); 
            
             switch (attackMode)
                    {
                        case 0: //SuperShot
                                
                            playerDamage = statsSO.Shoot * Mathf.FloorToInt(modifiers);
                            comDamage = rivalSO.tackle + rivalSO.cut;
                            totalDamage = Mathf.FloorToInt(playerDamage - comDamage);
                            _result = totalDamage;
                            Debug.Log(totalDamage); // si totalDamage es mayor que 0 gana PLAYER, animación o corrutina de tiro
                                        
                            BattleAnimation.BattleAnim.SuperShot(totalDamage);
                            break;
                                    
                        case 1: //SuperDrible
                            playerDamage = statsSO.Dribble * Mathf.FloorToInt(modifiers);
                            comDamage = rivalSO.tackle;
                            totalDamage = Mathf.FloorToInt(playerDamage - comDamage);
                            Debug.Log("superDrible"+playerDamage); // Para comprobar valores de superDrible
                                        
                            BattleAnimation.BattleAnim.SuperDrible(totalDamage);
                            break;
                                    
                        case 2: //SuperPass - cut  NO ES POSIBLE EJECUTAR EN ESTA DEMO
                            battleBoxScript.Warning(true);    
                            battleBoxScript.DestinationButton(battleBoxScript.buttonSubMenuPasar);
                            break;
                    }
            
            
        }
        else //sin rival enfrente, ball detectará si disminuye el poder del tiro o pase que se ejecute
        {
            playerMove.StopPosition();
            ballScript.transform.localPosition = new Vector3(0.15f, -0.12f, 0f);

            float powerMove = movsUI._selectedMov.Power;
            float critical = 1f;
            if (Random.Range(0f, 100f) < statsSO.Luck)
            {
                critical = 1.5f;
            }

            float distance = 1f;
            if (Vector2.Distance(player.transform.position, gkrival.position) > 1.8f)
            {
                distance = 0.8f;
            }

            float modifiers = Random.Range(1.5f, 1.75f)*critical*powerMove*distance;
            //Debug.Log(modifiers);
            
            switch (attackMode)
            {
                    
                        
                case 0: //SuperShot
                                
                    playerDamage = statsSO.Shoot * Mathf.FloorToInt(modifiers);
                    
                    totalDamage = Mathf.FloorToInt(playerDamage);
                    _result = totalDamage;
                    Debug.Log(totalDamage); // si totalDamage es mayor que 0 gana PLAYER, animación o corrutina de tiro
                                        
                    BattleAnimation.BattleAnim.SuperShot(totalDamage);
                    break;
                                    
                case 1: //SuperDrible NO ES POSIBLE EJECUTAR EN ESTA DEMO
                    battleBoxScript.Warning(true);    
                    battleBoxScript.DestinationButton(battleBoxScript.buttonSubMenuDriblar);
                    break;
                                    
                case 2: //SuperPass - cut  NO ES POSIBLE EJECUTAR EN ESTA DEMO
                    battleBoxScript.Warning(true);    
                    battleBoxScript.DestinationButton(battleBoxScript.buttonSubMenuPasar);         
                    break;
            }
            
        }
        
    }
    
    
    
    public void EstadoAnimacion()
    {
        
        PauseManager.Instance.isInputPaused = true;
        states = BattleState.BattleAnimation;
    }

    public void  Gk_SuperResolution(Rival_SO gkSO) //Le mando el elemento del portero??
    {
            
        GkType = PlayerStats.TypeMatrix.GetMultEffectiveness(selectedMove.Type1,gkSO.Type1);
        Debug.Log(GkType);
            
             
    }
    
    public void DesactiveBallEffects()//Desactivar effectos ball
    {
        foreach (GameObject effect in ballEffects) 
        {
            effect.SetActive(false);
        }

        ballEffects.Clear();

       


    }

    public void RestartShadows()
    { 
        foreach (GameObject objects in shadows)
        {
            objects.SetActive(false);
        }
    }

    public IEnumerator RestartGame()
    {
        PauseManager.Instance.isInputPaused = true;
        AudioManager.instance.PlayMusic("Stadium");
        ballScript.EnemyScript = null;
        ball.transform.SetParent(null);
        ball.SetActive(false);
        yield return new WaitForSeconds(1f);
        gkscript.GKAnim.applyRootMotion = false;
        ballScript.isFree = true;
        ballScript.isLosed = true;
        comTarget = null;
        DesactiveBallEffects();

        player.transform.position = new Vector3(2.65f, -0.75f, 0f);
        playerMove._ballAnimator = null;
        yield return new WaitForSeconds(1f);
        
        ball.transform.position = new Vector3(2.875f, -0.935f, 0f);
        ball.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.2f);

        isEnemyFar = true;
        isActivated = false;
        isBall = false;
        EstadoNoBatalla();
       
        yield return new WaitForSeconds(1f);
        player.SetActive(true);
        PauseManager.Instance.isInputPaused = false;




    }

    #region Counters
    public void Goal()
    {
        golesUI.GoalScore();
    }

    public void GoalCounter()
    {
        counter.Goals = golesUI.indexCount;
        
    }

    public void DribbleCounter() //Aumenta uno a uno cada vez que superdrible
    {
        counter.Dribbles++;
    }
    #endregion

    public void BackToBattle()
    {
        playerMove.PlayerMovements = Vector3.right;
    }

}

