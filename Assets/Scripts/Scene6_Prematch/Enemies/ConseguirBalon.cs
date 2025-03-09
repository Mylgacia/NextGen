using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConseguirBalon : MonoBehaviour
{
    [SerializeField] private Collider2D col;
    [SerializeField] private GameObject ballEnemy;
    [SerializeField] private ball ballScript;
    [SerializeField] private Transform EnemyBallPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        { 
            //ballScript.spr.DOColor(Color.red, 0.5f).SetLoops(2).OnComplete(ballScript.RestartColor);
            //ballScript.isWinned = false;
            ballEnemy.transform.SetParent(gameObject.transform);
           Debug.Log("TACKLIIIING!!!");
        }
    }*/

   public void EnemyGetBall()
   {
       Debug.Log("TACKLIIIING!!!");
   }
}
