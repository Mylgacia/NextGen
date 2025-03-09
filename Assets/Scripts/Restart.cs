using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
   [SerializeField] private GameObject gkEvent;
   [SerializeField] private GkMove gkScript;
   [SerializeField] private ball _ball;
   public void Restarting()
   {
      gkScript.TextReset();
      _ball.isElementalHit = false;
      _ball.isBallFlying = false;
      _ball.isFree = true;
      gkEvent.SetActive(false);
      if (PauseManager.Instance.isGamePaused)
      {
         PauseManager.Instance.isGamePaused = false;
      }
      BattleManager.BattleInstance.StartCoroutine("RestartGame");

   }
}
