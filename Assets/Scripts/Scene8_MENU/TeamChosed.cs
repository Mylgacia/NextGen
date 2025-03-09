using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamChosed : MonoBehaviour
{
    [SerializeField] private PlayerStats statsSO;
    [SerializeField] private Image Logo;
    [SerializeField] private SO_Teams[] team;
    [SerializeField] private int currentTeam;

    // Start is called before the first frame update
    void Start()
    {
       UpdateLogo();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)|| Input.GetKeyDown(KeyCode.Joystick1Button2))
        { 
            Exit();
        }
    }


    private void UpdateLogo()
    {
        currentTeam = statsSO.CurrentTeam;
        Logo.sprite = team[currentTeam].logo;
        
    }

    public void Exit()
    {
        Application.Quit();

    }
}
