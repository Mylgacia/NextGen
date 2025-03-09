using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="MainEnemy")]
public class MainsEnemySO : ScriptableObject
{
    public Sprite hair;
    public Sprite eyes;
    public Sprite skin;
    
    [SerializeField] private EnemyType type1;
    public EnemyType Type1 => type1;
        
    public Sprite elementIcon;
    public String name;
    
    public int shot;
    public int tackle;
    public int drible;
    public int cut;
    public int pass;
    public int physical;
    public Sprite[] stats;


    [SerializeField] private List<LearnableMove> learnableMoves;

    public List<LearnableMove> LearnableMoves => learnableMoves;

    public static int NUMBER_OF_LEARNABLE_MOVES { get; } = 4;
    
    

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
    
    [Serializable]
    public class LearnableMove
    {
        [SerializeField] private MoveBase move;
        [SerializeField] private int level;

        public MoveBase Move => move;
        public int Level => level;
    }
}
