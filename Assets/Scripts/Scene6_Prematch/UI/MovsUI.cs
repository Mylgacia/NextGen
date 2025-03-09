using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovsUI : MonoBehaviour
{
    public MoveBase _selectedMov;

    [SerializeField] private TextMeshProUGUI nameMove;
    [SerializeField] private TextMeshProUGUI powerMove;
    [SerializeField] private TextMeshProUGUI accuaryMove;
    [SerializeField] private TextMeshProUGUI energyWasted;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private MoveBase.Type type;
    public MoveBase.Type Type => type;
    
    
    //Name,Power,Accuary,EnergyWasted & Description 

    public void UpdateSelectedMove()
    {
         _selectedMov = BattleManager.BattleInstance.SMoveBase;
         type = BattleManager.BattleInstance.SMoveBase.Type1;
         SelectMove();
    } 

    private void SelectMove()
    {
        nameMove.text = _selectedMov.Name;
        powerMove.text = _selectedMov.Power.ToString();
        accuaryMove.text = _selectedMov.Accuracy.ToString()+ "%";
        energyWasted.text = _selectedMov.EnergyWasted.ToString();
        description.text = _selectedMov.Description;

    }
    
}
