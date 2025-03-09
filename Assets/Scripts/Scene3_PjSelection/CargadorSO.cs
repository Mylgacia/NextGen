using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargadorSO : MonoBehaviour //PARA SCENE 3 FACE SELECTION
{ //Script principal para elegir partes de la cabeza
    
    [SerializeField] private Image hair;
    [SerializeField] private Image eye;
    [SerializeField] private Image skin;

    //referencia al tipo de SO que queremos
    public PlayerSelection _pjSelectedSO;


    [SerializeField] Sprite[] hairs_sprites;
    [SerializeField] Sprite[] eyes_sprites;
    [SerializeField] Sprite[] skin_sprites;

    public int currentIndex_hairs;
    public int currentIndex_eyes;
    public int currentIndex_skins;

    public int NumeroDeEscena; //Cambio de escena

    private void Awake()
    {
        AudioManager.instance.menuSource.mute = true;

    }


    void Start()
    {
        StartCoroutine(Sounds());
        if (hairs_sprites != null & hairs_sprites.Length > 0) DisplayCurrentHair();
        if (eyes_sprites != null & eyes_sprites.Length > 0) DisplayCurrentEyes();
        if (skin_sprites != null & skin_sprites.Length > 0) DisplayCurrentSkin();
        
    }
   

   
    // Update is called once per frame
    void Update()
    {

    }

    public void NextButtonHair()
    {
        currentIndex_hairs++;
        
        if (currentIndex_hairs >= hairs_sprites.Length) currentIndex_hairs = 0;

        DisplayCurrentHair();
    }

    public void PreviousButtonHair()
    {
        currentIndex_hairs--;
        
        if (currentIndex_hairs < 0) currentIndex_hairs = hairs_sprites.Length - 1;

        DisplayCurrentHair();
    }

    public void NextButtonSkin()
    {
        currentIndex_skins++;
        if (currentIndex_skins >= skin_sprites.Length) currentIndex_skins = 0;

        DisplayCurrentSkin();
    }

    public void PreviousButtonSkin()
    {
        currentIndex_skins--;
        if (currentIndex_skins < 0) currentIndex_skins = skin_sprites.Length - 1;

        DisplayCurrentSkin();
    }
    public void NextButtonEyes()
    {
        currentIndex_eyes++;
        
        if (currentIndex_eyes >= eyes_sprites.Length) currentIndex_eyes = 0;

        DisplayCurrentEyes();
    }

    public void PreviousButtonEyes()
    {
        currentIndex_eyes--;
        
        if (currentIndex_eyes < 0) currentIndex_eyes = eyes_sprites.Length - 1;

        DisplayCurrentEyes();
    }
    public void DisplayCurrentHair()
    { 
        hair.sprite = hairs_sprites[currentIndex_hairs];
        AudioManager.instance.PlayMenuSounds("Click");
        
    }
    public void DisplayCurrentEyes()
    {
        eye.sprite = eyes_sprites[currentIndex_eyes];
        AudioManager.instance.PlayMenuSounds("Click");
       
    }
    public void DisplayCurrentSkin()
    {
        skin.sprite = skin_sprites[currentIndex_skins];
        AudioManager.instance.PlayMenuSounds("Click");
       
    }


    //CONFIRM HAIR,SKIN AND EYES SELECTED.KEEP IT UP INSIDE OUR SCRIPTABLEOBJECT CALLED "PlayerSelection"
    public void SelectButton()
    {


        _pjSelectedSO.Hair_selected = gameObject.GetComponent<CargadorSO>().hairs_sprites[currentIndex_hairs];
        _pjSelectedSO.Eyes_selected = gameObject.GetComponent<CargadorSO>().eyes_sprites[currentIndex_eyes];
        _pjSelectedSO.Skin_selected = gameObject.GetComponent<CargadorSO>().skin_sprites[currentIndex_skins];
        _pjSelectedSO.keepHair = currentIndex_hairs;
        _pjSelectedSO.keepEyes = currentIndex_eyes;
        _pjSelectedSO.keepSkin = currentIndex_skins;
        
        CargarNivel(NumeroDeEscena);
        AudioManager.instance.PlayMenuSounds("Confirm");

    }
    IEnumerator Sounds()
    {
        yield return new WaitForSeconds(1);
        AudioManager.instance.menuSource.mute = false;
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
