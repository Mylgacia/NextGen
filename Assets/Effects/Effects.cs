using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Effects : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Animator EffectsAnim; //Poner aquí el Gameobject Effects del player

    [Header("BallFollow")]
    [SerializeField] private GameObject ballPosition;
    [SerializeField] private GameObject ballGhost;
    private bool isDribleOn;

    [Header ("Fire")]
    [SerializeField] private GameObject ballFireCover;
    [SerializeField] private GameObject ballFireWave;
    
    [Header ("Water")]
    [SerializeField] private GameObject ballWaterCover;
    [SerializeField] private GameObject ballWaterWave;
    
    [Header ("Electric")]
    [SerializeField] private GameObject ballElectricCover;
    [SerializeField] private GameObject ballElectricRay;

    [Header("Ground")]
    [SerializeField] private GameObject ballGroundCover;
    [SerializeField] private GameObject ballGroundSplash;

    [Header("Others")]
    [SerializeField] private GameObject shadow_obj0;
    [SerializeField] private GameObject shadow_obj1;
    [SerializeField] private GameObject shadow_obj2;
    private bool isActivated;

    private void Start()
    {
        
       // fireCover = GameObject.Find("Ball/FireCover"); TIENEN QUE ESTAR ACTIVOS EN JERARQUIA PARA ENCONTRARLOS...
       // fireWave = GameObject.Find("FireWave");
    }

    

    #region Fire

    public void BallFireCover()
     {
        ballFireCover.SetActive(true);
        BattleManager.BattleInstance.Lista.Add(ballFireCover);
     }
    
     public void BallFireWave()
     {
       ballFireWave.SetActive(true);
       ball.GetComponent<ball>().StartCoroutine("Shoot");
       BattleManager.BattleInstance.Lista.Add(ballFireWave);
     }
    
     public void FireAura()
     {
        
        EffectsAnim.SetTrigger("FireAura");
     }
    
     public void Recharging()
     {
       EffectsAnim.SetTrigger("Recharging");
     }

    public void FireMove()
    {
        BallGhost();
        EffectsAnim.SetTrigger("FireMove");
    }

    #endregion

    #region Water

    public void BallWaterCover()
    {
        ballWaterCover.SetActive(true);
        BattleManager.BattleInstance.Lista.Add(ballWaterCover);
    }
    
    public void BallWaterWave()
    {
        ballWaterWave.SetActive(true);
        ball.GetComponent<ball>().StartCoroutine("Shoot");
        BattleManager.BattleInstance.Lista.Add(ballWaterWave);
    }
    
    public void WaterAura()
    {
        
        EffectsAnim.SetTrigger("WaterAura");
    }
    
    public void RechargingWater()
    {
        EffectsAnim.SetTrigger("Recharging");
    }

    public void WaterMove()
    {
        BallGhost();
        EffectsAnim.SetTrigger("WaterMove");
    }



    #endregion

    #region Electric

    public void BallElectricCover()
    {
        ballElectricCover.SetActive(true);
        BattleManager.BattleInstance.Lista.Add(ballElectricCover);
    }
    
    public void BallElectricRay()
    {
        ballElectricRay.SetActive(true);
        ball.GetComponent<ball>().StartCoroutine("Shoot");
        BattleManager.BattleInstance.Lista.Add(ballElectricRay);
    }
    
    public void ElectricAura()
    {
        
        EffectsAnim.SetTrigger("ElectricAura");
    }
    
    public void RechargingElectric()
    {
        EffectsAnim.SetTrigger("Recharging");
    }

    public void ElectricMove()
    {
        BallGhost();
        EffectsAnim.SetTrigger("ElectricMove");
    }

    #endregion

    #region Ground

    public void BallGroundCover()
    {
        ballGroundCover.SetActive(true);
        BattleManager.BattleInstance.Lista.Add(ballGroundCover);
    }

    public void BallGroundSplash()
    {
        ballGroundSplash.SetActive(true);
        BattleManager.BattleInstance.Lista.Add(ballGroundSplash);
        ball.GetComponent<ball>().StartCoroutine("Shoot");
    }

    public void GroundAura()
    {
        
        EffectsAnim.SetTrigger("GroundAura");
    }

    public void RechargingGround()
    {
        EffectsAnim.SetTrigger("Recharging");
    }

    public void GroundMove()
    {
        BallGhost();
        EffectsAnim.SetTrigger("GroundMove");
    }
    #endregion


    public void Shadow0()
    {
        shadow_obj0.SetActive(isActivated =!isActivated);
        
        shadow_obj2.SetActive(isActivated = !isActivated);
    }
    public void Shadow1()
    {
        shadow_obj1.SetActive(isActivated = !isActivated);
    }

    public void Shadow2()
    {
        shadow_obj2.SetActive(isActivated = !isActivated);
    }

    public void Shake() 
    {
        CmShake.Instance.ShakeCamera(5f, 0.1f);
    
    }

    private void BallGhost() //Pelota sustituta para los dribles
    {
        ballPosition.SetActive(false);
        ballGhost.SetActive(true);
    }

    public void BallNoGhost()
    {
        ballPosition.SetActive(true);
        ballGhost.SetActive(false);
    }

    // SOUNDS

    public void SoundNormalShoot()
    {
        AudioManager.instance.PlayMatchSounds("NormalShoot");
    }
}
