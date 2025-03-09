
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    // ~~ 1. Controls All Player Movement
    // ~~ 2. Updates Animator to Play Idle & Walking Animations

    [SerializeField] private float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 playerMovement;
    public Vector3 PlayerMovements { get => playerMovement; set => playerMovement = value; }
    private Vector3 moveInput;
    private Animator animator;
    //private BallMovements ballmovements;
    [SerializeField] private GameObject ball;
    [SerializeField] private ball ballscript;
    [SerializeField] private Animator ballAnimator;
    [SerializeField] private Transform ballPosition;
    [SerializeField] private GameObject confused;
    
    [Header ("Positions")]
    //[SerializeField] private Transform exTransform;

    private float velocity = 0.0f;
    

    public Transform _ballPosition => ballPosition;
    public Animator _animator => animator;

    public Animator _ballAnimator
    {
        get => ballAnimator;
        set => ballAnimator = value;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
       // ballAnimator = ball.GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        
       /* if (playerMovement != Vector3.zero) //and Ball setactive true
        {
            ballAnimator.SetBool("IsMoving",true);
        }
        else
        {
            ballAnimator.SetBool("IsMoving",false);
        }*/
    }

    private void FixedUpdate()
    {
        if (PauseManager.Instance.isInputPaused)
        {
            Debug.Log("Entrado");
            return; 
        }
        playerMovement = Vector3.zero;
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(playerMovement.x, playerMovement.y, 0).normalized;

        UpdateAnimationAndMove();
    }

    public void UpdateAnimationAndMove()
    {
      

        if (playerMovement != Vector3.zero)
        {
            MoveCharacter();
            PlayerMoving();
            if (ballAnimator != null) //si ball está disponible para coger )
            {
               
                BallMoving();
                BallParentPosition();
            }
        
        }
        else
        {
            animator.SetBool("moving", false);
            if (ballAnimator != null) //si ball está disponible para coger )
            {
               ballAnimator.SetBool("IsMoving", false); 
                
            }
           
        }
    }

    private void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + moveInput * speed * Time.deltaTime);
    }

    private void PlayerMoving()
    {
        animator.SetFloat("moveX", playerMovement.x);
        animator.SetFloat("moveY", playerMovement.y);
        animator.SetBool("moving", true);
    }

    public void GetBall()
    {
        ball.transform.localPosition = Vector3.zero;
        ballAnimator = ball.GetComponent<ball>().ballAnimator;
        BattleManager.BattleInstance.isBall = true;
        
        
    }

    private void BallMoving()
    {
        
        ballAnimator.SetFloat("moveX", playerMovement.x);
        ballAnimator.SetFloat("moveY", playerMovement.y);
        ballAnimator.SetBool("IsMoving", true);
       /* if (playerMovement.x <= 0) //quitamos el collider fijo de la bola para que no interactue MUY COSTOSO
        {
            ball.GetComponent<Collider2D>().enabled = false;
        } */
    }

    private void BallParentPosition()
    {
        if (playerMovement.x > 0)
        {
           ball.transform.localPosition = new Vector3(0.13f, -0.11f, 0f) ;
        }
        if (playerMovement.x < 0)
        {
           ball.transform.localPosition = new Vector3(-0.09f, -0.11f, 0f);
        }
        if (playerMovement.y > 0 & playerMovement.x == 0)
        {
            ball.transform.localPosition = new Vector3(0.06f, -0.05f, 0f);
        }
        if (playerMovement.y < 0 & playerMovement.x == 0)
        {
          ball.transform.localPosition = new Vector3(0f, -0.2f, 0f);
        }
        
    }

    public void StopPosition() //Parar antes de la animación
    {
        animator.SetBool("moving", false);
        animator.SetFloat("moveX",1);
        //animator.SetFloat("moveY",0);
        
        if (ballAnimator != null) //Para cuando hagamos TACKLING
        {
             //ball.SetActive(true);
            // ball.transform.localPosition = new Vector3(0.16f, -0.11f, 0f); 
             ballAnimator.SetBool("IsMoving", false);
             ballAnimator.SetFloat("moveX",1);
             // ballAnimator.SetFloat("moveY",0);
        }
       
        
    }

    public void Go() //De los estados de anim al idle
    {
        
        animator.SetTrigger("go");
        PauseManager.Instance.isInputPaused = false;
       
        BattleManager.BattleInstance.comTarget = null;
        BattleManager.BattleInstance.isBattle = false;
        BattleManager.BattleInstance.StartCoroutine("EstadoNoBatalla");
    }

    public IEnumerator Confused()
    {
        PauseManager.Instance.Pause();
        animator.SetTrigger("confused");
        confused.SetActive(true);
        yield return new WaitForSeconds(4f);
        confused.SetActive(false);
        Go();
    }
    
    
}
