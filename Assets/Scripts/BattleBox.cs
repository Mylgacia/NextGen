using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using TMPro;
using Unity.VisualScripting;

public class BattleBox : MonoBehaviour
{
    [Header ("Instantiate UIBox")]
    public Transform transformJugador;
    //public float altura;
    public RectTransform rtr;
    public float offset;
    private Camera cam;
    //private ColorBlock theColor;
    public Button buttonDisable;
   //private ColorBlock originalColorBlock;
   //private ColorBlock pressedColorBlock; 
   
    [Header ("SelectionOption")]
    //public bool isSelectionOn;
    public int currentPlayerSelection;
    public GameObject panelMovs;
    [SerializeField] private GameObject warning; //MENSAJE AVISO NO DISPONIBLE ALGO
    public GameObject _Warning => warning;
    [SerializeField] private bool isButtonActive = true;
    [SerializeField] private GameObject advise; //MENSAJE RIVAL DEMASIADO CERCA!
    
    [Header ("Shoot")]
    public GameObject Tirar_Menu;
    public Image childTirar;
    public Button buttonSubMenuTirar;
    public bool isSuperShotOn;
    public GameObject SpecialBoxS;
    
    [Header ("Dribble")]
    public GameObject Driblar_Menu; //Primer objeto seleccionado en el menú
    public Image childDriblar;
    public Button buttonSubMenuDriblar;
    public bool isSuperDribbleOn;
    public GameObject SpecialBoxD;
    
    [Header ("Pass")]
    public GameObject Pasar_Menu;
    public Image childPasar;
    public Button buttonSubMenuPasar;
    public bool isSuperPassOn;
    public GameObject SpecialBoxP;
    

    void Start()
    {
        rtr = GetComponent<RectTransform>();
        
    }

    private void Update()
    {

       
    }
   
    private void OnEnable()
    {
         FollowPlayer();
         currentPlayerSelection = 0;
         EventSystem.current.SetSelectedGameObject(Tirar_Menu); //no tiene setter
         
         Time.timeScale = 0f;
       
         
    }
	
    
    public void ButtonDisableColors()
    {
        isButtonActive = false;
        ColorBlock colors = buttonDisable.colors;
        colors.normalColor = Color.gray;
        //colors.selectedColor = new Color32(255, 100, 100, 255);
        colors.selectedColor = Color.gray;
        buttonDisable.colors = colors;
    }
    public void ButtonAvaibleColors()
    {
        isButtonActive = true;
        ColorBlock colors = buttonDisable.colors;
        colors.normalColor = Color.white;
        //colors.selectedColor = new Color32(255, 100, 100, 255);
        colors.selectedColor = Color.red;
        buttonDisable.colors = colors;
    }

    
    public void DestinationButton(Button destinationButton)
    {
        //AQUI HACEMOS EL CHECKEO DE SI EL BOOL ESTA ACTIVO AL TENER 4 o + de STATS
        //SpecialDribbleBox.SetActive(true);
        destinationButton.Select();
    }

    #region *Shoot

     public void EnciendeTirar()
     {
        if (EventSystem.current.currentSelectedGameObject == Tirar_Menu)
        {
            childDriblar.gameObject.SetActive(false);
            childPasar.gameObject.SetActive(false);
            childTirar.gameObject.SetActive(true); 
        }
                
     }
     public void ShotExecution()
     {
          BattleManager.BattleInstance.isEnemyFar = false; //Para que no nos salte el menu al llegar el enemigo durante la acción de tiro
         if (isSuperShotOn)
         {
           SpecialBoxS.SetActive(true);
           DestinationButton(buttonSubMenuTirar);
         }
        else
        {
            if(BattleManager.BattleInstance.states == BattleState.StartingBattle) { RivalNear(); return; }
            BattleManager.BattleInstance.EjecutarMovimientoNormal(0);
            Debug.Log("ejecutar Tiro normal o sin modificador");
        } 
     }
     public void NormalTiro()
        {
        if (BattleManager.BattleInstance.states == BattleState.StartingBattle) { RivalNear(); return; }
            BattleManager.BattleInstance.EjecutarMovimientoNormal(0); 
            Debug.Log("ejecutar Tiro normal o sin modificador");
        }
     public void SuperTiro()
        {
        Debug.Log("ejecutar o añadir modificador Super Tiro");
        //LLama a los movs del elemento del jugador con skillmove = 0 y ponlos en panelmovs;
        //Seguramente haya que llamar al movebase dentro del Move.LearnableMove del PlayerStats (statsSO) y pasar los stats al objeto IU llamado "Ability" (intentar hacer prefab)
         BattleManager.BattleInstance.EstadoEmpezandoBatalla(0);
         panelMovs.SetActive(true);
        }

    #endregion
    
    #region *Dribble
    
        public void EnciendeDriblar()
        {
            if (EventSystem.current.currentSelectedGameObject == Driblar_Menu)
            {
            childTirar.gameObject.SetActive(false);
            childDriblar.gameObject.SetActive(true);
            childPasar.gameObject.SetActive(false);
            
            }

        }

     public void DribbleExecution()
        {
        if (!isButtonActive) { return; }

            if (isSuperDribbleOn)
            {
                Warning(false);
                SpecialBoxD.SetActive(true);
                DestinationButton(buttonSubMenuDriblar);
            }
            else
            {
                Warning(false);
                BattleManager.BattleInstance.EjecutarMovimientoNormal(1);
                Debug.Log("ejecutar Regate normal o sin modificador");
            } 
        }

        public void NormalRegate()
        {
            
            BattleManager.BattleInstance.EjecutarMovimientoNormal(1);
            Debug.Log("ejecutar Regate normal o sin modificador");
        }
        public void SuperRegate()
        {
            
            BattleManager.BattleInstance.EstadoEmpezandoBatalla(1);
            panelMovs.SetActive(true);
            /* if (!isSuperDribbleOn)
             {
                 Debug.Log("pon gris o animación y sonido de ERROR"); //NormalRegate()
                 return;
             }*/
            Debug.Log("ejecutar o añadir modificador Super Regate");
        }
     
    #endregion

    #region *Pass
    
        public void EnciendePasar()
        {
        
         if (EventSystem.current.currentSelectedGameObject == Pasar_Menu) //no tiene setter, es decir, no se puede cambiar
            {
            childDriblar.gameObject.SetActive(false);
            childTirar.gameObject.SetActive(false);
            childPasar.gameObject.SetActive(true);
            }
        }

     public void PassExecution()
        {
            if (isSuperPassOn)
            {
                SpecialBoxP.SetActive(true);
                DestinationButton(buttonSubMenuPasar);
            }
            else
            { 
                Warning(true);
               // BattleManager.BattleInstance.EjecutarMovimientoNormal(2);
                Debug.Log("ejecutar Pase normal o sin modificador");
            } 
        }
        public void NormalPase()
        {
            Warning(true);
            //BattleManager.BattleInstance.EjecutarMovimientoNormal(2);
            Debug.Log("ejecutar Pase normal o sin modificador");
        }
        public void SuperPase()
        {
            BattleManager.BattleInstance.EstadoEmpezandoBatalla(2);
            panelMovs.SetActive(true);
            Debug.Log("ejecutar o añadir modificador Super Pase");
        }
     

    #endregion

    public void Warning(bool isTrue)
    {
       // warning.gameObject.SetActive(isActivated = !isActivated);
        warning.SetActive(isTrue);
    }
    
    void FollowPlayer()
    {
        cam = Camera.main;
       rtr.position = cam.WorldToScreenPoint(transformJugador.position + Vector3.left*offset + Vector3.up*offset);
       
       //rtr.position = transformJugador.position;
    }

    public void RivalNear()
    {
      advise.SetActive(true);
      DestinationButton(Tirar_Menu.GetComponent<Button>());

    }

    private void OnDisable()
    {
        
        Time.timeScale = 1f;
    }
    
}
