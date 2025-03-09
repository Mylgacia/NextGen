

[System.Serializable]
public class EnemyData //necesita un constructor
{
    //public int[] enemySave;
    public int enemyID;

    public EnemyData(int EnemyID) //para que se vea en el texto json
    {
        enemyID = EnemyID;
    }

   

}
[System.Serializable]
public class PlayerData
{
    
    //public int[] enemySave;
    public int playerHair, playerEyes, playerSkin;


    public PlayerData(PlayerSelection playerSelection) //para que se vea en el texto json
    {
        playerHair = playerSelection.keepHair;
        playerEyes = playerSelection.keepEyes;
        playerSkin = playerSelection.keepSkin;

    }
    

    
}  

[System.Serializable]
public class Memory
{
    
    //Guardas ID de los enemigos
    public EnemyData[] enemyData;
    public PlayerData playerData;
}
