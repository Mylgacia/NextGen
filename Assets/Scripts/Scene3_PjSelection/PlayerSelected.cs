using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelected : MonoBehaviour
{
    public PlayerSelection _pjSelectedSO;
    //public int NumeroDeEscena;
    //public PlayerSelection asset_pjSelectedSO;

    public Image hair_sprite;
    public Image eyes_sprite;
    public Image skin_sprite;
    

    // Start is called before the first frame update

    private void Awake()
    {//En cuanto se despierte el objeto contenedor de este script  coge las partes de la cabeza guardadas en el scriptableObject
        
        hair_sprite.sprite = _pjSelectedSO.Hair_selected;
        eyes_sprite.sprite = _pjSelectedSO.Eyes_selected;
        skin_sprite.sprite = _pjSelectedSO.Skin_selected;

    }


   
}
