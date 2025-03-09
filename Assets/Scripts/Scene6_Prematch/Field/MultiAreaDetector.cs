using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAreaDetector : MonoBehaviour
{
    [SerializeField] private ball ballScript;

    public void TopTrigger(Collider2D collider2D)
    {
        ballScript.isLosed = false;
        PauseManager.Instance.Pause();

        BattleManager.BattleInstance.PlayerMove.GetComponent<Animator>().SetBool("moving", false);
        BattleManager.BattleInstance.StartCoroutine("RestartGame");
        
        Debug.Log("Saque de banda");
        AudioManager.instance.PlayMatchSounds("Whistle02");
        //Saque de banda o reinicia 
    }
    public void BottomTrigger(Collider2D collider2D)
    {
        ballScript.isLosed = false;
        PauseManager.Instance.Pause();

        BattleManager.BattleInstance.PlayerMove.GetComponent<Animator>().SetBool("moving", false);
        BattleManager.BattleInstance.StartCoroutine("RestartGame");
        
        Debug.Log("Saque de banda");
        AudioManager.instance.PlayMatchSounds("Whistle02");

        //Saque de banda o reinicia 
    }
    public void OutTrigger(Collider2D collider2D) 
    {
            ballScript.isBallFlying = false;
            ballScript.isLosed = false;
            PauseManager.Instance.Pause();

            BattleManager.BattleInstance.PlayerMove.GetComponent<Animator>().SetBool("moving", false);
            BattleManager.BattleInstance.StartCoroutine("RestartGame");
            
            Debug.Log("Fueraaaa");
            AudioManager.instance.PlayMatchSounds("Whistle02");
        
       
        //Corner, saque de puerta o reinicia 
    }
    public void BackTrigger(Collider2D collider2D)
    {
        ballScript.isLosed = false;
        PauseManager.Instance.Pause();
        
        BattleManager.BattleInstance.PlayerMove.GetComponent<Animator>().SetBool("moving", false);
        BattleManager.BattleInstance.StartCoroutine("RestartGame");
        AudioManager.instance.PlayMatchSounds("Whistle02");

    }    
    
}
