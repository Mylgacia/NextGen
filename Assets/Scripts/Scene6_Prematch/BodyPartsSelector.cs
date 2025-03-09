// Code written by tutmo (youtube.com/tutmo)
// For help, check out the tutorial - https://youtu.be/PNWK5o9l54w

using System;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartsSelector : MonoBehaviour
{
    // ~~ 1. Updates Character Face=Body
    [SerializeField] private PlayerSelection playerFace;
    // Full Character Body
    [SerializeField] private SO_CharacterBody characterBody;
    // Body Part Selections
    [SerializeField] private BodyPartSelection[] bodyPartSelections;
    //Update Parts in Manager
    [SerializeField] private BodyPartsManager manager;

    private void Awake()
    {
        //Al inicio podemos usar update si por ejemplo cambiamos de equipo, lo llamamos a updateCurrentParts
                //Corrutina de igual bodyParts=Player face y luego ya carga las animaciones
                bodyPartSelections[0].bodyPartCurrentIndex = playerFace.keepSkin;
                bodyPartSelections[1].bodyPartCurrentIndex = playerFace.keepEyes;
                bodyPartSelections[2].bodyPartCurrentIndex = playerFace.keepHair;
              // characterBody.characterBodyParts[0].bodyPart[] = playerFace.keepSkin;


               /* //Get All Current Body Parts
                for (int i = 0; i < bodyPartSelections.Length; i++)
                {
                    GetCurrentBodyParts(i);
                } */
    }

    private void Start() //ANTES DE COGER INFORMACION DE CHARACTER_BODY SO, TENDREMOS QUE RELLENARLO DESDE PLAYER FACE
    {
        for (int i = 0; i < bodyPartSelections.Length; i++)
        {
            UpdateCurrentPart(i);
        }        
        manager.UpdateBodyParts();
      
    }

   
    private bool ValidateIndexValue(int partIndex) //Sin uso actualmente
    {
        if (partIndex > bodyPartSelections.Length || partIndex < 0)
        {
            Debug.Log("Index value does not match any body parts!");
            return false;
        }
        else
        {
            return true;
        }
    }

    private void GetCurrentBodyParts(int partIndex)
    {
        // Get Current Body Part Name
//        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartName;
        // Get Current Body Part Animation ID
        bodyPartSelections[partIndex].bodyPartCurrentIndex = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartAnimationID;
    }

    private void UpdateCurrentPart(int partIndex) //Lo llamaremos otra vez si cambiamos de equipo, ropa...
    {
        // Update Selection Name Text
       // bodyPartSelections[partIndex].bodyPartNameTextComponent.text = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex].bodyPartName;
        // Update Character Body Part
        characterBody.characterBodyParts[partIndex].bodyPart = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex];
    }
}

[System.Serializable]
public class BodyPartSelection
{
    public string bodyPartName;
    public SO_BodyPart[] bodyPartOptions;
   // public Text bodyPartNameTextComponent;
    public int bodyPartCurrentIndex;
}
