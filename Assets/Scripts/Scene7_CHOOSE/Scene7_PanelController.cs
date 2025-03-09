using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Scene7_PanelController : MonoBehaviour
{
    [Header("CoachDialogue")]
    [SerializeField] private GameObject firstPanel;
    [SerializeField] private GameObject botonCoach;
    [SerializeField] private GameObject firstText;

    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private float characterPorSecond = 0.08f;
    private int lineIndex = 0;

    public bool isDialogueStarted;


    [Header("Element")]
    [SerializeField] private PlayerStats statsSO;
    [SerializeField] private Image elementBox;

    [Header("AgentOffers")]
    [SerializeField] private PlayableDirector playable;
    [SerializeField] private GameObject mobile;
    [SerializeField] private GameObject agentPanel;
    [SerializeField] private GameObject botonAgent;
    [SerializeField] private GameObject secondText;
    [SerializeField, TextArea(4, 6)] private string finalLine;
    [SerializeField] private TextMeshProUGUI dialogueText2;
    [SerializeField] private bool agentEnd = false;

    [Header("ChooseTeam")]
    [SerializeField] private GameObject team1;
    [SerializeField] private GameObject team2;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject panelSelectionTeam;
    [SerializeField] private Button firsSelected;
    [SerializeField] private SO_Counter soCounter;
    [SerializeField] private bool isChosenTeam = false;

    [SerializeField] private Image fadePanel;

    // Start is called before the first frame update
    private void Awake()
    {
        elementBox.sprite = statsSO.elementIcon;
       
        
    }
    void Start()
    {   
        AudioManager.instance.musicSource.clip = null;
        AudioManager.instance.PlayMusic("MenuTheme");
        AudioManager.instance.musicSource.volume = 0.8f;

        //Primero activamos panel entrenador y paramos inputs (jugador no se mueva)
        isDialogueStarted = false;
        
        
        //Invocar Agent_Presentation() al terminar sequence y Activar Boton "A"
        Invoke("CoachResume", 0f);
        botonCoach.SetActive(true);
    }

    void Update()
    {
        if (isDialogueStarted & Input.GetKeyDown(KeyCode.A) || isDialogueStarted & Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            CoachResume();
            AudioManager.instance.PlayMenuSounds("ButtonAccept");
        }

        if (agentEnd & Input.GetKeyDown(KeyCode.A) || agentEnd & Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            agentEnd = false;
            ChooseTeam();
            
            AudioManager.instance.PlayMenuSounds("ButtonAccept");
        }

        if(isChosenTeam & Input.GetKeyDown(KeyCode.Return) || isChosenTeam  & Input.GetKeyDown(KeyCode.Joystick1Button0) )
        {
            isChosenTeam = false;
            AudioManager.instance.PlayMenuSounds("Confirm");

        }


    }
    private void CoachResume()
    {
        if (!isDialogueStarted)
        {
            StartDialogue();
            isDialogueStarted = true;

        }
        else if (dialogueText.text == dialogueLines[lineIndex])
        {
            NextDialogueLine();


        }
        /* else if (dialogueText.text == dialogueLines[lineIndex])
         {
             NextDialogueLine();

         }*/
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[lineIndex];

        }

    }

    private void StartDialogue()
    {
        isDialogueStarted = true;

        lineIndex = 0;
        StartCoroutine(ShowLine()); 

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
            botonCoach.SetActive(false);
            isDialogueStarted= false;
           
            playable.Play();
            AudioManager.instance.PlayMenuSounds("MobileCalling");
            AudioManager.instance.musicSource.volume = 0.3f;

        }

    }
    // Update is called once per frame

    public void AgentFinal()
    {
        firstPanel.SetActive(false);
        agentPanel.SetActive(true);
        
        StartCoroutine(ShowFinalLine());
        

    }

    private void ChooseTeam()
    {
       
        botonAgent.SetActive(false);
        title.SetActive(true);
        panelSelectionTeam.SetActive(true);
        
        if (soCounter.Goals >= 2)
        {
            team1.SetActive(true);

        }

        if (soCounter.Goals >= 2 & soCounter.Dribbles >= 3)
        {
            team2.SetActive(true);

        }

        //EventSystem.current.firstSelectedGameObject = firsSelected.gameObject;
        // EventSystem.current.SetSelectedGameObject(firsSelected.gameObject); //Con el mando ejecuta submit?¿
        //Select();
        
        StartCoroutine(Select());
        
        

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
        agentEnd = true;
        botonAgent.SetActive(true);
    }

    private IEnumerator Select()
    {
        yield return new WaitForSeconds(0.5f);
        
        EventSystem.current.SetSelectedGameObject(firsSelected.gameObject); //Con el mando ejecuta submit?¿
        isChosenTeam = true;
    }
   
    //CARGAR NUEVA ESCENA

    IEnumerator CargarAsync(int NumeroDeEscena)
    {
        fadePanel.DOFade(1, 2f);
        yield return new WaitForSeconds(2); //esperar unos segundos antes de empezar la Coroutine

        AsyncOperation operation = SceneManager.LoadSceneAsync(NumeroDeEscena);
        
    }

    public void CargarNivel(int NumeroDeEscena)
    {
        StartCoroutine(CargarAsync(NumeroDeEscena));
    }
}
