using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using UnityEngine.Playables;



public class ball : MonoBehaviour
{
    [Header ("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement playerScript;
    private SpriteRenderer spr;
    
    [Header("Enemy")]
    [SerializeField] private SeguirJugadorArea enemyScript;

    //public SeguirJugadorArea _enemyScript => enemyScript; 
    public SeguirJugadorArea EnemyScript
    {
        get => enemyScript;
        set => enemyScript = value;
    }
    

    [Header("Goal")] 
    [SerializeField] private Transform topTarget;
    [SerializeField] private Transform upTarget;
    [SerializeField] private Transform downTarget;
    [SerializeField] private int _finalTarget;
    public int FinalTarget => _finalTarget;
    [SerializeField] private int _encounters; //Cuantos enemigos tocaron la pelota
    [SerializeField] private PlayableDirector goalEvent_playable;
    
    public int Encounters
    {
        get =>_encounters;
        set => _encounters = value;
    }  
    
    [Header ("Animations")]
    public Animator ballAnimator;
    
    
    public bool isBallFlying = false;
    public bool isLosed = true; //Cuando robamos la pelota
    public bool isFree = true; //Cuando robemos la pelota o la cojamos estando libre se pondrá verde
    public bool isCatchable = false;
    public bool isElementalHit = false;
    void Start()
    {   
        ballAnimator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")& isFree == true) //poner verde y padre dentro del PlayerCharacter
        {
            this.transform.SetParent(playerScript._ballPosition);
           playerScript.GetBall();
          // ballAnimator = GetComponent<Animator>();

           spr.DOColor(Color.green, 0.5f).SetLoops(2).OnComplete(RestartColor);
           
        }

        if ((other.gameObject.CompareTag("Enemy") & isCatchable) || (other.gameObject.CompareTag("Enemy2") & isCatchable))
        {
            BattleManager.BattleInstance.isBall = false;
           this.transform.SetParent(other.transform);
           if(isLosed){ 
               spr.DOColor(Color.red, 0.5f).SetLoops(3).OnComplete(LoseColor);
               
           }
           enemyScript = other.GetComponent<SeguirJugadorArea>();
           playerScript._ballAnimator = null;
           
           enemyScript.isNoBall = false; //Enemigo coge pelota
           enemyScript.GetBallAnim();
           player.GetComponent<PlayerMovement>().StartCoroutine("Confused");

        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
         if (other.gameObject.CompareTag("Goal"))
         { 
             Debug.Log("Goal");
            AudioManager.instance.PlayMusic("GoalTrainStadium");
            gameObject.SetActive(false);
             goalEvent_playable.Play();
            BattleManager.BattleInstance.Goal();
            AudioManager.instance.PlayMatchSounds("Goal");
             
         }
         
         
    }

    public IEnumerator Shoot()
    {
        
        ballAnimator.SetTrigger("NormalShoot");
        int target = Mathf.FloorToInt(Random.Range(0f, 3f));
        yield return new WaitForSeconds(1);
        BallShoot(target);
        ballAnimator.SetTrigger("NormalShootII");
    }
    
    public void BallShoot(int target) //hacer random antes y enviar aquí el parametro target
    {
        isBallFlying = true;
        Debug.Log("Lado"+ target);
        
        if (target == 0)
        {
            transform.DOMove(topTarget.position, 2f);//LEFT
            
        }
        if (target == 1)
        {
            transform.DOMove(downTarget.position, 2f);//RIGHT
            
        }
        if (target == 2)
        {
            transform.DOMove(upTarget.position, 2f);//UP
            
            //transform.DOLocalJump(upTarget.position + new Vector3(0.1f,0.1f,0), 1f, 1, 2f).SetEase(Ease.OutBounce);
        }

        _finalTarget = target;
        playerScript._ballAnimator = null;

    }
   

   public void RestartColor()
   {
       spr.DOColor(Color.white, 0f);
       isFree = false;
   }

   public void LoseColor()
   { 
       spr.DOColor(Color.white, 0f);
       isLosed = false;
   }

    public void TouchSound() 
    {
        int randomTouch = Random.Range(0, 1);
        if (randomTouch == 0)
        {
            AudioManager.instance.PlayMatchSounds("BallTouch01");
        }
        else { AudioManager.instance.PlayMatchSounds("BallTouch02"); }

    }
   
}
