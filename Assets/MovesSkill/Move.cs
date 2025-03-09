using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    
    [SerializeField] private List<LearnableMove> learnableMoves;

    public List<LearnableMove> LearnableMoves => learnableMoves;
   // public static int NUMBER_OF_LEARNABLE_MOVES { get; } = 5;
    
    private MoveBase _base; //USAR ESTO PARA LLAMADA A LOS PARAMETROS DENTRO DE MOVEBASE SCRIPTABLEOBJECT Y BUSCAR OTRO PARA EL DEFENSA ENEMIGO COMO ENEMY:DEFENSA

    //private int numero;
    [SerializeField] private PlayerStats statsSO;

    private int valorInt;
   // [SerializeField] private UIStats uiStats;

   private void Awake()
   {
        EnumToInt();
   }

   private void Start()
    {
        CheckMovsElement();
    }
    public void EnumToInt()
    {
        PlayerStats.PlayerType valorEnum = statsSO.MiEnum;
        valorInt = (int)valorEnum;
       // Debug.Log(valorInt);
    }
    public void CheckMovsElement()
    {
        statsSO.moves = new List<LearnableMove>();
        
       //AHORA VOY A HACER UN SWITCH COMO EL DE UISTATS EN EL QUE CASE 0 Fire,1 Agua...
       
       int enumElement = valorInt;

       switch (enumElement)
       {
           case 0:
               
               foreach (LearnableMove movs in learnableMoves)
               {
                   if (movs.MiEnum == LearnableMove.MoveType.Fire)
                   {
                       statsSO.moves.Add(movs);
                   }
               }
               break;
           
           case 1:
               foreach (LearnableMove movs in learnableMoves)
               {
                   if (movs.MiEnum == LearnableMove.MoveType.Water)
                   {
                       statsSO.moves.Add(movs);
                   }
               }
               break;
           
           case 2:
               foreach (LearnableMove movs in learnableMoves)
               {
                   if (movs.MiEnum == LearnableMove.MoveType.Electric)
                   {
                       statsSO.moves.Add(movs);
                   }
               }
               break;
           
           case 3:
               foreach (LearnableMove movs in learnableMoves)
               {
                   if (movs.MiEnum == LearnableMove.MoveType.Ground)
                   {
                       statsSO.moves.Add(movs);
                   }
               }
               break;
           
           case 4:
               foreach (LearnableMove movs in learnableMoves)
               {
                   if (movs.MiEnum == LearnableMove.MoveType.Dark)
                   {
                       statsSO.moves.Add(movs);
                   }
               }
               break;
           
           case 5:
               foreach (LearnableMove movs in learnableMoves)
               {
                   if (learnableMoves.Contains(movs) & movs.MiEnum == LearnableMove.MoveType.Light)
                   {
                       statsSO.moves.Add(movs);
                   }
               }
               break;
           case 6: //Normal, no usado en esta demo
               foreach (LearnableMove movs in learnableMoves)
               {
                   if (learnableMoves.Contains(movs) & movs.MiEnum == LearnableMove.MoveType.None)
                   {
                       statsSO.moves.Add(movs);
                   }
               }
               break;
           
       }
       
       


       
    }

   /* public MoveBase Base
    {
        get => _base;
        set => _base = value;
    }
    

    public Move(MoveBase mBase)
    {
        _base = mBase;
        _base.Type1 = mBase.Type1;
    }*/
    

   
    
    
    [Serializable]
    public class LearnableMove // AQUI SE PONEN TODOS LOS MOVS QUE EXISTEN EN EL JUEGO, INCLUSO LOS NO DISPONIBLES EN ESE MOMENTO
    {
        [SerializeField] private MoveBase move;
        [SerializeField] private int level;
        [SerializeField] private int skillMove;
       //public int skillMove;
        [SerializeField] private MoveType miEnum;
   
        public MoveBase MMove => move;
        public int Level => level;
        public int SkillMove => skillMove; // En este orden Tiro,Drible,Pase,Parar,Corte y Físico
        
       
        public MoveType MiEnum
        {
            get => miEnum;
            set => miEnum = value;
        }
        
        public enum MoveType
        {
            Fire = 0,
            Water = 1,
            Electric = 2,
            Ground = 3,
            Dark = 4,
            Light = 5,
            None = 6,
        }
    }
}
