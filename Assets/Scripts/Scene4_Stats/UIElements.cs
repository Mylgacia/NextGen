using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIElements : MonoBehaviour //PARA SCENE 4 IMAGEN Y TEXTO DEL ELEMENTO A ELEGIR
{   
    [Header("Elements")]
    [SerializeField] private Image icon;
    public Image element;
    
    public Sprite[] element_sprites;
    public Button reButton;
    public Button avButton;
    public TextMeshProUGUI textElement;
    public GameObject noAvaible;
    
    public int currentIndex_element;

    private void Awake()
    {
        AudioManager.instance.menuSource.volume = 0;
    }
    private void Start()
    {
        if (element_sprites != null & element_sprites.Length > 0) DisplayCurrentElement();
        

    }

   
    public void NextButtonElement()
    {
        currentIndex_element++;
        
        if (currentIndex_element >= element_sprites.Length) currentIndex_element = 0;

        DisplayCurrentElement();
    }

    public void PreviousButtonElement()
    {
        currentIndex_element--;
        
        if (currentIndex_element < 0) currentIndex_element = element_sprites.Length - 1;

        DisplayCurrentElement();
    }
    
    public void DisplayCurrentElement()
    {
        AudioManager.instance.PlayMenuSounds("Click");
        AudioManager.instance.menuSource.volume = 1;
        element.sprite = element_sprites[currentIndex_element];
        textElement.text = element.sprite.name; //COGEMOS EL NOMBRE DEL SPRITE Y LO MOSTRAMOS EN PANTALLA
        
        if (currentIndex_element >= 4) //Check si es un elemento no disponible como los de la posicion 4 y 5
        {
            noAvaible.SetActive(true) ;
        }else { noAvaible.SetActive(false);}
    }
    
    public void SelectionElement() 
    {
        
        if (EventSystem.current.currentSelectedGameObject == avButton.gameObject) //detecta actual seleccion == avButton
            
        {
            avButton.onClick.Invoke();
            

        } 
        
        if(EventSystem.current.currentSelectedGameObject == reButton.gameObject)
        {
            // btnHairNext.animator.enabled = !btnHairNext.animator.enabled;
            reButton.onClick.Invoke();
        }
        
    }
    
        
        
    
}

