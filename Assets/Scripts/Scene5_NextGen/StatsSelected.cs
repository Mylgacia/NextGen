using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsSelected : MonoBehaviour
{
    public PlayerStats statsSO;
    [Header("Element")]
    [SerializeField] private Image elementBox;

    [Header("Energy")]
    [SerializeField] private TextMeshProUGUI energy;

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
    {
        elementBox.sprite = statsSO.elementIcon;
        currentShot = statsSO.Shoot;
        currentTackle = statsSO.Tackle;
        currentDrible = statsSO.Dribble;
        currentCut = statsSO.Cut;
        currentPass = statsSO.Pass;
        currentPhysical = statsSO.Physical;
        energy.text = statsSO.MaxEnergy.ToString();

    }
    
    void Start()
    {
        imageS.sprite = statsSO.stats[currentShot];
        imageT.sprite = statsSO.stats[currentTackle];
        imageD.sprite = statsSO.stats[currentDrible];
        imageC.sprite = statsSO.stats[currentCut];
        imageP.sprite = statsSO.stats[currentPass];
        imagePh.sprite = statsSO.stats[currentPhysical];
        
        
    }

    
    void Update()
    {
        
    }
}
