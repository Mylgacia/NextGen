using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIStats : MonoBehaviour
{
    public UIElements uiElements;
    [Header("Stats")]
    [SerializeField] private Image stats;
    
    [Header("Counter")] 
    [SerializeField] private TextMeshProUGUI count_text;
    public int counter;

    [Header("SaveStats")] 
    public PlayerStats statsSO; //Guardar que currentIndex_shot, pass... es el que se puso en la UI
    [SerializeField] private GameObject shotIndex;
    [SerializeField] private GameObject tackleIndex;
    [SerializeField] private GameObject dribleIndex;
    [SerializeField] private GameObject cutIndex;
    [SerializeField] private GameObject passIndex;
    [SerializeField] private GameObject physicalIndex;
    
    
    [Header("")] 
    public int NumeroDeEscena;
    
    private void Awake()
    {
        statsSO.MaxEnergy = 500;
    }

    private void Start()
    {
        UpdateCounter();
    }

    public void UpdateCounter()
    {
        count_text.text = counter.ToString();

    }
    
    public void SelectButton() //Se acciona en el Confirm_Button al darle a submit
    {
       if (counter == 0 & uiElements.currentIndex_element < 4)
       {
           StartCoroutine("UpdateSO");
           StartCoroutine("SaveElement");
           
           CargarNivel(NumeroDeEscena);
           AudioManager.instance.PlayMenuSounds("Confirm");

       } else { AudioManager.instance.PlayMenuSounds("Error02"); } //sonido ERROR
       
       
    }
    IEnumerator UpdateSO()
    {   
        statsSO.Shoot = shotIndex.GetComponent<Choose_Stats>().currentIndex;
        statsSO.Tackle = tackleIndex.GetComponent<Choose_Stats>().currentIndex;
        statsSO.Dribble = dribleIndex.GetComponent<Choose_Stats>().currentIndex;
        statsSO.Cut = cutIndex.GetComponent<Choose_Stats>().currentIndex;
        statsSO.Pass = passIndex.GetComponent<Choose_Stats>().currentIndex;
        statsSO.Physical = physicalIndex.GetComponent<Choose_Stats>().currentIndex;

        CheckMaxEnergy(); //Depende del físico tendremos más o menos energía
        IntToEnum(); //Actualizar tipo del player
        //EnumToInt();
        //statsSO.miEnum = PlayerStats.PlayerType.Light;
        
        yield return new WaitForSeconds(1);
    }

    IEnumerator SaveElement()
    {
        statsSO.elementIcon = uiElements.element.sprite;
        IntToEnum();
        
        yield return new WaitForSeconds(0);
    }
    IEnumerator CargarAsync(int NumeroDeEscena)
    {
        yield return new WaitForSeconds(1); //esperar unos segundos antes de empezar la Coroutine

        AsyncOperation operation = SceneManager.LoadSceneAsync(NumeroDeEscena);
    }
    
    public void CargarNivel(int NumeroDeEscena)
    {
        StartCoroutine(CargarAsync(NumeroDeEscena));
    }
    

    private void IntToEnum() //Guardada por si hago conversión
    {
        int num = uiElements.currentIndex_element;
        statsSO.MiEnum = (PlayerStats.PlayerType)Enum.ToObject(typeof(PlayerStats.PlayerType), num);
        
        //int valorInt = 1;
        //MyEnum valorEnum = (MyEnum)Enum.ToObject(typeof(MyEnum), valorInt);
    }

    public void CheckMaxEnergy()
    {
       int resistance = statsSO.Physical;
       int increment=0;

        switch (resistance) //También podemos hacer que por cada punto subir una cantidad de energia adicional
        {
            case 3:
                increment = 100;
                statsSO.MaxEnergy += increment;
                break;
            case 4:
                increment = 200;
                statsSO.MaxEnergy += increment;
                break;
            case 5:
                increment = 300;
                statsSO.MaxEnergy += increment;
                break;
            
        }
        
        //Debug.Log(resistance);

    }
}
