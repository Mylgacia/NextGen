using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class Scene6_PanelController : MonoBehaviour
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

    [Header("Pause&Time")]
    public GameObject timer;

    [Header("PreStart")]
    public TMP_Text timerFirstCount;
    [SerializeField] private GameObject cuentaRegresiva;
    public float tiempoInicial = 4f;
    public bool tiempoCorriendo = false;
    public bool interaction = true;

    [Header("StartTimer")]
    public float tiempoActual;
    public TMP_Text timerGlobalText;
    private int minutes, seconds;

    [Header("FinalTime")]
    private bool isEndTime = false;
    [SerializeField] private Image fadePanel;



    void Start()
    {
        AudioManager.instance.musicSource.clip = null;
        AudioManager.instance.PlayMusic("Stadium");
        AudioManager.instance.musicSource.volume = 0.5f;
        //Primero activamos panel entrenador y paramos inputs (jugador no se mueva)
        isDialogueStarted = false;
        PauseManager.Instance.Pause();
        //TO DO: Hacer un move y fade con la ventana de texto del agente antes de hablarnos
        //Invocar Agent_Presentation() al terminar sequence y Activar Boton "A"
        Invoke("CoachPresentation", 0f);
        botonCoach.SetActive(true);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)& interaction || Input.GetKeyDown(KeyCode.Joystick1Button0) & interaction) //Empezando
        {
            CoachPresentation();
            AudioManager.instance.PlayMenuSounds("ButtonAccept");
        }
        if (tiempoCorriendo) //No puedo hacer dos corrutinas porque comparten tiempo?¿?
        {
            
            timerGlobalText.gameObject.SetActive(true);
            tiempoActual -= Time.deltaTime;
            minutes = (int)(tiempoActual / 60f);
            seconds = (int)(tiempoActual - minutes * 60f);

            timerGlobalText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


            if (tiempoActual <= 0f & isEndTime == false)
            {
                tiempoActual = 0f;

                tiempoCorriendo = false;
                isEndTime = true;
                timerGlobalText.transform.parent.gameObject.SetActive(false);

                //método TimerExpirado();
                AudioManager.instance.PlayMatchSounds("WhistleFinal");
                PauseManager.Instance.Pause(); 
                BattleManager.BattleInstance.GoalCounter();

                StartCoroutine(Ending());
               
                

            }

        }


    }


    private void CoachPresentation()
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
            //botonCoach.SetActive(false);
            firstPanel.SetActive(false);
            cuentaRegresiva.SetActive(true);
            StartCoroutine(PreStart());
            //agentEnd = true;
            //finalText = true;

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

    private IEnumerator PreStart() //Cuenta regresiva para empezar
    {
        while (true)
        {
           
            interaction = false;
            tiempoInicial -= Time.deltaTime;

            timerFirstCount.text = Mathf.FloorToInt(tiempoInicial).ToString();     
            

            if (tiempoInicial <= 1f)
            {
                
                cuentaRegresiva.SetActive(false);
                tiempoInicial = 0;
                tiempoCorriendo = true;

                //AudioManager.instance.matchSource.clip = AudioManager.instance.matchSounds[0].clip;
                //AudioManager.instance.matchSource.Play();


                StopCoroutine(PreStart());
            }

            yield return null;

        }



    }


    //CARGAR NUEVA ESCENA

    IEnumerator Ending()
    {
        BattleManager.BattleInstance.isBall = false;
        AudioManager.instance.PlayMatchSounds("WhistleFinal");
        yield return new WaitForSeconds(3f);
        fadePanel.DOFade(1, 2f);
        CargarNivel(7);
    }

    IEnumerator CargarAsync(int NumeroDeEscena)
    {
        yield return new WaitForSeconds(2); //esperar unos segundos antes de empezar la Coroutine

        AsyncOperation operation = SceneManager.LoadSceneAsync(NumeroDeEscena);
    }

    public void CargarNivel(int NumeroDeEscena)
    {
        StartCoroutine(CargarAsync(NumeroDeEscena));
    }
}
