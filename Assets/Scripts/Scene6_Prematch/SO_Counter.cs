using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Counter")]
public class SO_Counter : ScriptableObject
{
    [SerializeField] private int goals;
    [SerializeField] private int dribbles;
    [SerializeField] private int goalAssits;

    public int Goals
    {
        get => goals;
        set => goals = value;
    }

    public int Dribbles
    {
        get => dribbles;
        set => dribbles = value;
    }

    public int GoalAssists
    {
        get => goalAssits;
        set => goalAssits = value;

    }
}
