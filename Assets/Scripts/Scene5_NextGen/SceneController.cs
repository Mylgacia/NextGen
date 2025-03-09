
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{

    [SerializeField] private Draft draft;
    [SerializeField] private GameObject botonAgent;
    [SerializeField] private GameObject firstText;
    [SerializeField] private GameObject backText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField, TextArea(4, 6)] private string finalLine;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI dialogueText2;
    [SerializeField] private float characterPorSecond = 0.08f;
    private int lineIndex = 0;
    public bool passScene = false;
    public bool waitScene = false;
    
    public bool isDialogueStarted;
    public bool finalText = false;
    public bool agentEnd = false;
    
    [Header("Menu Options")]
    [SerializeField] private GameObject PanelSave;
    [SerializeField] private GameObject SaveBox;
    [SerializeField] private GameObject ContinueBox;

    [Header("SAVE&LOAD")]
    [SerializeField] private SaveLoader saveLoader;
    
    void Start()
    {
        
        isDialogueStarted = false;
        //TO DO: Hacer un move y fade con la ventana de texto del agente antes de hablarnos
        //Invocar Agent_Presentation() al terminar sequence y Activar Boton "A"
        Invoke("AgentPresentation",0f);
        botonAgent.SetActive(true);
        
        // obj_text.text = PlayerPrefs.GetString("user_name");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)&!finalText || Input.GetKeyDown(KeyCode.Joystick1Button0) & !finalText) //Empezando
        {
            AgentPresentation();
            AudioManager.instance.PlayMenuSounds("ButtonAccept");
        }

        if (Input.GetKeyDown(KeyCode.A) & passScene == true || Input.GetKeyDown(KeyCode.Joystick1Button0) & passScene == true || Input.GetKeyDown(KeyCode.Return) & passScene == true)
        {
            passScene = false;
            Invoke("LoadNextScene",2f);
            AudioManager.instance.PlayMenuSounds("Confirm");
        }
        
        if (Input.GetKeyDown(KeyCode.Y) & waitScene == true || Input.GetKeyDown(KeyCode.Joystick1Button3) & waitScene == true)
        {
            PanelSave.SetActive(true);
            waitScene = false;
            AudioManager.instance.PlayMenuSounds("Guardar");
            //Guardar listas y pj
            StartCoroutine("SaveData");
            
            
        }
        
      /*  if (Input.GetKeyDown(KeyCode.L)&!finalText) 
        {
            //Load
            saveLoader.LoadEnemyData();
        }
       */ 
        
    }

    
    
    private void AgentPresentation()
    {
        if (!isDialogueStarted)
        {
            StartDialogue();
            isDialogueStarted = true;

        }
        else if (dialogueText.text == dialogueLines[lineIndex])
        {
            NextDialogueLine();
            //Invoke("ReadyDraft",2f);
            
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
        //dialoguePanel.SetActive(true);
        
        lineIndex = 0;
        StartCoroutine(ShowLine());
        //botonAgent.SetActive(true);
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
            botonAgent.SetActive(false);
            Invoke("ReadyDraft",2f);
            //agentEnd = true;
            finalText = true;
            
        }
        
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
    
    private IEnumerator ShowFinalLine()
    {
        dialogueText2.text = string.Empty;

        foreach (char ch in finalLine)
        {
            dialogueText2.text += ch;
            yield return new WaitForSecondsRealtime(characterPorSecond);
            
        }
    }

    public void ReadyDraft()
    {
        draft.StartCoroutine("ChooseEnemy"); //Llamando al script donde está la seleccion automatica de enemigos
        //finalText = true;
    }

    public void AgentFinal()
    {
        StartCoroutine(ShowFinalLine());
        backText.SetActive(true);
        firstText.SetActive(false);
        Invoke("ActiveFinal",3f);
        
    }

    public void ActiveFinal()//Habilitar ventana de guardado y paso de escena
    {
        //botonAgent.SetActive(true);
        passScene = true;
        waitScene = true;
        SaveBox.SetActive(true);
        ContinueBox.SetActive(true);
        
    }

    IEnumerator SaveData()
    {
        saveLoader.AddEnemyData();
        saveLoader.AddPlayerData();
        yield return new WaitForSeconds(3);
        saveLoader.Save();
        PanelSave.SetActive(false);
    }
    
    private void LoadNextScene()
    {
        SceneManager.LoadScene("6_PREMATCH");
    }
}
