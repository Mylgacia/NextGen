using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class EnemySelected : MonoBehaviour
{

    //[SerializeField] private MainsEnemySO[] enemies;
    public Draft draft;
    public MainsEnemySO enemySO;
    //public int current;
    //public GameObject objectToFind;
    
    [Header("Face")]
    public Image hair_sprite;
    public Image eyes_sprite;
    public Image skin_sprite;

    [Header("Name&Element")]
    [SerializeField] private Image element;
    [SerializeField] private TextMeshProUGUI enemyName;

    [Header("Shot")]
    [SerializeField] private Image imageS;
    [SerializeField] private TextMeshProUGUI textS;
    [SerializeField] private int currentShot;
    
    [Header("Tackle")]
    [SerializeField] private Image imageT;
    [SerializeField] private TextMeshProUGUI textT;
    [SerializeField] private int currentTackle;
    
    [Header("Drible")]
    [SerializeField] private Image imageD;
    [SerializeField] private TextMeshProUGUI textD;
    [SerializeField] private int currentDrible;
    
    [Header("Cut")]
    [SerializeField] private Image imageC;
    [SerializeField] private TextMeshProUGUI textC;
    [SerializeField] private int currentCut;
    
    [Header("Pass")]
    [SerializeField] private Image imageP;
    [SerializeField] private TextMeshProUGUI textP;
    [SerializeField] private int currentPass;
    
    [Header("Physical")]
    [SerializeField] private Image imagePh;
    [SerializeField] private TextMeshProUGUI textPh;
    [SerializeField] private int currentPhysical;

    private void Awake()
    {//En cuanto se despierte el objeto contenedor de este script  coge las partes de la cabeza guardadas en el scriptableObject
       
        /* current = Random.Range(0, enemies.Length);
        if (enemies != null)
        {
            enemySO = enemies[current];
        }*/
        //Llamar al selector de enemigos para que me de uno (un scriptableObject) aleatorio y no se repita
        //draft = FindObjectOfType<Draft>();

        //objectToFind = FindObjectOfType<Draft>().gameObject;
        
        
        
        //enemySO = draft.MyList[current];

    }
    
    void Start()
    {
       //draft.ChooseEnemy(); 
        enemySO = draft.mySo; //vamos al draft a coger un jugador
        //current = draft.current;
        Face_Stats();
        Name_Sprites();
    }

    public void AllStats()
    {
        
    }
   public void Face_Stats()
    {
        hair_sprite.sprite = enemySO.hair; 
        eyes_sprite.sprite = enemySO.eyes;
        skin_sprite.sprite = enemySO.skin;
                                  
        currentShot = enemySO.shot;
        currentTackle = enemySO.tackle;
        currentDrible = enemySO.drible;
        currentCut = enemySO.cut;
        currentPass = enemySO.pass;
        currentPhysical = enemySO.physical; 
    }

    void Name_Sprites()
    {
        enemyName.text = enemySO.name;
        element.sprite = enemySO.elementIcon; 
      
        imageS.sprite = enemySO.stats[currentShot];
        imageT.sprite = enemySO.stats[currentTackle];
        imageD.sprite = enemySO.stats[currentDrible];
        imageC.sprite = enemySO.stats[currentCut];
        imageP.sprite = enemySO.stats[currentPass];
        imagePh.sprite = enemySO.stats[currentPhysical];
    }
    
    
}
