using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PjSelected")]
public class PlayerStats : ScriptableObject //Stats min 2 y max 5
{
   public Sprite elementIcon;
   [SerializeField] private PlayerType miEnum;

   public PlayerType MiEnum
   {
       get { return miEnum; }
       set { miEnum = value; }
   } //valor modificable */
  
  
   
   [SerializeField] private int maxEnergy = 500;
   [SerializeField] private int shoot;
   [SerializeField] private int tackle;
   [SerializeField] private int dribble;
   [SerializeField] private int cut;
   [SerializeField] private int pass;
   [SerializeField] private int physical;
   [SerializeField] private int luck = 25; //25% de mejorar la habilidad al usarla
   [SerializeField] private int charisma = 1; //Carisma del 1 al 10 que te ayuda a pedir la pelota y ganar dialogos
   
   public Sprite[] stats;

  [Header("Movimientos")] 
   public List<Move.LearnableMove> moves;

    [Header("Equipo")]
    [SerializeField] private int currentTeam;
    public int CurrentTeam
    {
        get => currentTeam;
        set => currentTeam = value;
    }



    public int MaxEnergy //Practicando get y set
   {
       get => maxEnergy;
       set => maxEnergy = value;
   }
   public int Dribble
   {
       get => dribble;
       set => dribble = value;
   }

   public int Pass
   {
       get => pass;
       set => pass = value;
   } 

   public int Shoot
   {
       get => shoot;
       set => shoot = value;
   }

   public int Tackle
   {
       get => tackle;
       set => tackle = value;
   }

   public int Cut
   {
       get => cut;
       set => cut = value;
   }
   public int Physical
   {
       get => physical;
       set => physical = value;
   } 
   
   public int Luck
   {
       get => luck;
       set => luck = value;
   } 
   
   public int Charisma
   {
       get => charisma;
       set => charisma = value;
    } 
  
   
   public enum PlayerType
   {
       Fire = 0,
       Water = 1,
       Electric = 2,
       Ground = 3,
       Dark = 4,
       Light = 5,
       None = 6,
   }

    public void TeamChosen(int current) //Equipo que estamos en ese momento
    {
        currentTeam = current;
        
    }


    /*public enum Stat //Podemos simplificarlo a ataque y defensa ahora mismo
    {
        _Shot, _Tackle, _Drible, _Cut, _Pass
    }*/

    public class TypeMatrix //Retocar, quizás mejor en BattleManager?¿?
   {
       private static float[][] matrix =
       {
           //                   FIR    WAT   ELE   GRO   DAR   LIG   NON
           /*FIR*/ new float[] { 0.5f, 0.5f, 1.0f, 0.5f, 1.5f, 1.5f, 1.5f },
           /*WAT*/ new float[] { 1.5f, 0.5f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
           /*ELE*/ new float[] { 1.0f, 1.5f, 0.5f, 1.0f, 1.0f, 0.0f, 1.5f },
           /*GRO*/ new float[] { 1.0f, 1.0f, 1.5f, 0.5f, 1.0f, 1.0f, 1.5f },
           /*DAR*/ new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.5f, 1.5f },
           /*LIG*/ new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.5f, 0.5f, 1.0f },
           /*NON*/ new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 1.0f, 1.0f, 0.5f },

       };
   
       
       public static float GetMultEffectiveness(MoveBase.Type attackType, Rival_SO.EnemyType playerDefenderType)
       {
           if (attackType == MoveBase.Type.None) //Seguramente sea MoveType y no playerType
           {
               return 1.0f;
           }
   
           int row = (int)attackType;
           int col = (int)playerDefenderType;
   
           return matrix[row][col];
   
       }
   
   
   }
   
   
}


