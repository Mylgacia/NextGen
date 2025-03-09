using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;




public class DisplayText : MonoBehaviour //Hacemos aparecer el Pj_panel para el nombre y se activa, luego
                                         //aparece dialogo antes de que pasemos de escena
{
    public TextMeshProUGUI obj_text;
    public TMP_InputField display;
    [SerializeField] private int charLimit; //min caracteres del nombre
    
    //Segundo dialogo, introductorio a la escena de Seleccion de Pj
    [SerializeField] private GameObject botonAgent;
    [SerializeField] private GameObject botonPj;
    [SerializeField] private GameObject backText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private TextMeshProUGUI dialogueText2;
    [SerializeField] private float characterPorSecond = 0.08f;
    private int lineIndex = 0;
    public bool writedName = false;
    public bool passScene;
    
    
    
    void Start()
    {
        display.text = String.Empty;
        display.ActivateInputField(); //Para posicionarse en el input y escribir
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) & writedName == true || Input.GetKeyDown(KeyCode.Joystick1Button0) & writedName == true)
        {
            Create();
            AudioManager.instance.PlayMenuSounds("Confirm");
            display.DeactivateInputField();
            backText.SetActive(true);
            ReStartDialogue();
            
            writedName = false;
            botonPj.SetActive(false);
            display.readOnly = true;
            

        }


        if (Input.GetKeyDown(KeyCode.A) & passScene == true || Input.GetKeyDown(KeyCode.Joystick1Button0) & passScene == true)
        {
            passScene = false;
            AudioManager.instance.PlayMenuSounds("ButtonAccept");
            Invoke("LoadNextScene",2f);

        }
        
        
    }

    public void Create()
    {
        obj_text.text = display.text;
        PlayerPrefs.SetString("user_name",obj_text.text);
        PlayerPrefs.Save();
    }

    private void ReStartDialogue()
    {
        StartCoroutine(ShowLineFinal());
        
    }

    
     
    private void LoadNextScene()
    {
        SceneManager.LoadScene("3_PJSELECTION");
    }
    private IEnumerator ShowLineFinal()
    {

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText2.text += ch;
            yield return new WaitForSecondsRealtime(characterPorSecond);

        }
        botonAgent.SetActive(true);
        passScene = true;



    }

    public void MinCharacters() //Para que el nombre no sea inferior a 4 caracteres y quede mal. Entre 4 y 8 debe ser
    {
        if ((obj_text.text.Length >= charLimit) & (obj_text.text.Length <= 8))
        {
            botonPj.SetActive(true);
            writedName = true;
        }
        else
        {
            botonPj.SetActive(false);
            writedName = false;
           
            
        }
    }
}
