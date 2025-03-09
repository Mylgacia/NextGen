using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DefenseEnemy")]
public class Rival_SO : ScriptableObject
{    
    [SerializeField] private EnemyType type1;
    public EnemyType Type1 => type1;
    
    public int shot;
    public int tackle;
    public int drible;
    public int cut;
    public int pass;
    public int physical;
    
    [Header ("GoalKeeper")]
    public int saved;
    public int catched;
    
    public enum EnemyType
    {
        Fire,
        Water,
        Electric,
        Ground,
        Dark,
        Light,
        None,
    }
    
    
    
    
    
}
