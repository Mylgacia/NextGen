using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Move", menuName = "Players/Nuevo Movimiento")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string name;
    public string Name => name;

    [TextArea][SerializeField] private string description;
    public string Description => description;

    [SerializeField] private Type type1;
    //public Type Type1 => type1;
    public Type Type1
    {
       get => type1;
       set => type1 = value; 
    }
    
    
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private bool alwaysHit;
    [SerializeField] private int energyWasted;
    [SerializeField] private int priority;
    
    [SerializeField] private MoveTarget target;

    
    public int Power => power;
    public int Accuracy => accuracy;
    public bool AlwaysHit => alwaysHit;
    public int EnergyWasted => energyWasted;
    public int Priority => priority;
    
    
    public MoveTarget Target => target;
    
    public enum Type
    {
        Fire = 0,
        Water = 1,
        Electric = 2,
        Ground = 3,
        Dark = 4,
        Light = 5,
        None = 6,
    }

    /*if (type == PokemonType.Fire || type == PokemonType.Water ||
        type == PokemonType.Grass || type == PokemonType.Ice ||
        type == PokemonType.Electric || type == PokemonType.Dragon ||
        type == PokemonType.Dark || type == PokemonType.Psychic)
    {
        return true;
    }
    else
    {
        return false;
    }*/
    

    public enum MoveTarget
    {
    Self, Other
    }
    
}

