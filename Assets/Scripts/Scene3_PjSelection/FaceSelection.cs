
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class FaceSelection : MonoBehaviour
{
    [Header("Hair")]
    public Button btnHairPrevious;
    public Button btnHairNext;

    [Header("Eyes")] 
    public Button btnEyesPrevious;
    public Button btnEyesNext;
    
    /*NO IMPLEMENTADO PORQUE TENGO DOS SOLAMENTE
    [Header("Skin")] 
    public Button btnSkinPrevious;
    public Button btnSkinNext;*/

    private void Start()
    {
        
    }

    public void SelectionHair() 
    {
        
        if (EventSystem.current.currentSelectedGameObject == btnHairNext.gameObject) //detecta si la actual seleccion
                                                                                     //es la flecha-boton del hairNext
        {
            btnHairNext.onClick.Invoke();
            //ACTIVAMOS EL ANIMATOR DEL BUTTON DESDE HAIR, por ejemplo, y lo desactivamos cuando bajemos o subamos
            //btnHairNext.animator.enabled = true;

        } 
        
        if(EventSystem.current.currentSelectedGameObject == btnHairPrevious.gameObject)
        {
           // btnHairNext.animator.enabled = !btnHairNext.animator.enabled;
            btnHairPrevious.onClick.Invoke();
        }
        
    }
    
    public void SelectionEyes() 
    {
        
        if (EventSystem.current.currentSelectedGameObject == btnEyesNext.gameObject)
        {
            btnEyesNext.onClick.Invoke(); 
            
        } else if (EventSystem.current.currentSelectedGameObject == btnEyesPrevious.gameObject)
            
        {
            btnEyesPrevious.onClick.Invoke();
        }
        
    }

   
     
}
    
    
    
