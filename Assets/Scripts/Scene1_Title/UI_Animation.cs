
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;
using TMPro;
using UnityEngine.SceneManagement;



public class UI_Animation : MonoBehaviour
{
    public RectTransform cam;
    public int finalPos;
    public float CamPosDuration;

    public Image title;
    public int titleFadeValue;
    public float titleFadeDuration;
    
    //OptionsMenu logic
    [SerializeField] private List<TextMeshProUGUI> actionTexts;
    
    public int currentPlayerSelection;
    
    [SerializeField] private Color selectedColor = Color.white;
    [SerializeField] private GameObject miniBall1; 
    [SerializeField] private GameObject miniball2;

    [SerializeField] private bool selectionIsOn;

    private float timeSinceLastClick;
    public float timeBetweenClicks = 1.0f;
    
    //UI_textAnim
   public Animator _optionsAnim;
   [SerializeField] private Image fadePanel;


    private void Awake()
    {
        
        _optionsAnim= GetComponent<Animator>();
       

    }

    void Start()
    {
        StartCoroutine(ThemeMusic());
        currentPlayerSelection = 1; //Cursor de reinicio, TO DO: despues de la animacion del title
        
        var sequence = DOTween.Sequence(); //con secuencia puedo manejar mejor el tiempo de ejecucion
        sequence.Insert(1f, cam.DOMoveY(finalPos, CamPosDuration));
        sequence.Insert(2f,title.DOFade(titleFadeValue, titleFadeDuration).SetEase(Ease.InQuint)); //to do: Tipo de Fading
        sequence.OnComplete(() => InitialMenu());
        

        //Invoke("InitialMenu",3f);

    }

    
    void Update()
    {
        timeSinceLastClick += Time.deltaTime; 
        PlayerMenuSelection();
        
        
    }

    public void InitialMenu() //Funcion para que quede marcada la primera opcion, llamada cuando termine la secuencia del titulo DOTWEEN
    {
        //actionTexts[0].color = Color.black;
        //actionTexts[0].DOFade(0, 1).SetLoops(-1);
        miniBall1.SetActive(true);
        miniball2.SetActive(false);
        selectionIsOn = true;
        
        _optionsAnim.SetBool("Blink",true);
        _optionsAnim.SetTrigger("Activate");
        
        

    }
        
    public void PlayerMenuSelection()
        {
            if (selectionIsOn == false) //Hasta que no finalice la intro no irá ningún input
            {
                return;
            }
            if (timeSinceLastClick < timeBetweenClicks) 
            {
                return;
            }
            
            if (Input.GetAxisRaw("Vertical") != 0) //Al hacerlo así en lugar de con KeyDown pues podemos usar gamepads :D
            {
                timeSinceLastClick = 0;
               AudioManager.instance.PlayMenuSounds("Click");
                
                currentPlayerSelection = (currentPlayerSelection +1) % 2; //Si quiero añadir alguna opcion mas
                
                /*if (currentPlayerSelection == 0)
                {
                    currentPlayerSelection++;
                }else if (currentPlayerSelection == 1)
                {
                    currentPlayerSelection--;
                   
                }*/
                
                SelectAction(currentPlayerSelection);
                 
            }
            
            //Ejecutar accion
            if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Return)) // TO DO: Hacer que se pueda con el gamepad, project settings joy num
            {
                if (currentPlayerSelection == 1)
                {
                    //Continuar, es decir, cargar partida. Vertical Layout empieza de abajo hacia arriba
                    Debug.Log("no hay partida");
                    AudioManager.instance.PlayMenuSounds("Error01");


            }
                else if (currentPlayerSelection == 0)
                {
                  //Nueva partida, empezar de 0 con seleccion de pj
                  // SceneManager.LoadScene("2_INTRODUCTION");
                  selectionIsOn = false;
                  CargarNivel(2);
                  AudioManager.instance.PlayMenuSounds("Start");

                }
            }
                
                
            
        }
    
        private void SelectAction(int selectedAction) //Ojo que en un Vertical Layout el O empieza por abajo
        {
           // var sequence = DOTween.Sequence();
          
            
            for (int i = 0; i < actionTexts.Count; i++)
            { 
                if (i == selectedAction)
                {
                    _optionsAnim.SetBool("Blink",true);
                   // actionTexts[i].color = Color.black;
                   // actionTexts[i].DOFade(0, 1).SetLoops(-1);
                    
                    
                    miniBall1.SetActive(true);
                    miniball2.SetActive(false);
                    
                }
                else
                {
                    _optionsAnim.SetBool("Blink",false);
                    actionTexts[i].color = selectedColor;
                    //actionTexts[i].DOFade(0, 1).SetLoops(-1);
                    
                    miniBall1.SetActive(false);
                    miniball2.SetActive(true);
                    
                }
                    
                    
                    
                    
                
            } 
        }

        IEnumerator ThemeMusic()
        {
         yield return new WaitForSeconds(0.8f);
         AudioManager.instance.PlayMusic("TitleTheme");
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
