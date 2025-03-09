using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="PjSelected")]
public class PlayerSelection : ScriptableObject
{
    public Sprite Hair_selected;
    public Sprite Eyes_selected;
    public Sprite Skin_selected;

    [Header("Keep int values")] 
    public int keepHair;
    public int keepEyes;
    public int keepSkin;

}
