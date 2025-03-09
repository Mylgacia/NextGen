using System.Collections;
using TMPro;
using UnityEngine;



public class Agent_Intro : MonoBehaviour
{
    
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private float characterPorSecond = 0.08f;
    private int lineIndex;
    
    //Para pasar al panel del player, pj_panel
    [SerializeField] private GameObject botonAgent;
    [SerializeField] private GameObject botonPj;
    [SerializeField] private bool agent_ready = true;
    [SerializeField] private GameObject pjpanel;
    [SerializeField] private GameObject saveName;

    public bool isDialogueStarted;
    

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.musicSource.clip = null;
        AudioManager.instance.PlayMusic("MenuTheme");
        AudioManager.instance.musicSource.volume = 0.1f;
        isDialogueStarted = false;
        //TO DO: Hacer un move y fade con la ventana de texto del agente antes de hablarnos
        //Invocar Agent_Presentation() al terminar sequence y Activar Boton "A"
        Invoke("Agent_Presentation",1f);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) & agent_ready || Input.GetKeyDown(KeyCode.Joystick1Button0) & agent_ready)  //Sustituir por A de gamepad
        {
            Agent_Presentation();
            AudioManager.instance.PlayMenuSounds("ButtonAccept");
        }
        

       


    }

    
    private void Agent_Presentation()
    {
         if (!isDialogueStarted)
         {
             
             StartDialogue();
             botonAgent.SetActive(true);
         }
        
         else if (dialogueText.text == dialogueLines[lineIndex])
         {
             
             NextDialogueLine();
             
         }
         
         
         else
         {
             StopAllCoroutines();
             dialogueText.text = dialogueLines[lineIndex];
             

         }
        
        
        
        
        
    }

    

    private void StartDialogue()
    {
        isDialogueStarted = true;
        dialoguePanel.SetActive(true);
        
        lineIndex = 0;
        StartCoroutine(ShowLine());
        //Time.timeScale = 0;
       //Invoke("Boton_Anim", 2f);

    }

    private void NextDialogueLine()
    {
        lineIndex++;
        
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            agent_ready = false;
            isDialogueStarted = false;
            //dialoguePanel.SetActive(false);
            
           // Time.timeScale = 1;
           
           PjpanelOn(); //Ir a pjpanel a introducir el nombre del player
        }
       

    }
    
    private void PjpanelOn()
    {
        botonAgent.SetActive(false);
        pjpanel.SetActive(true);
        saveName.SetActive(true);
    }

    private IEnumerator ShowLine()
    {
       
        dialogueText.text = string.Empty;
       
        foreach (char ch in dialogueLines[lineIndex])
        { 
            
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(characterPorSecond);

        }

        


    }



    
}