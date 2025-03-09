using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoader : MonoBehaviour
{
    [Header("Player")] 
    public PlayerSelection _pjSelectedSO;
    //public List<PlayerData> playerData;
    public PlayerData playerData; //Esta en el script Data
    
    [Header("Rivals")]
    public Draft draft;
    public List<EnemyData> enemyData;
    
    [Header("MemoryCard")]
    public Memory memory;
    


    public void Save()
    {
        StartCoroutine(("Saving"));
        
    }
    
    
    
    IEnumerator Saving()
    {
        memory.enemyData = enemyData.ToArray();
        memory.playerData = playerData;
       
        yield return new WaitForEndOfFrame();
        string json = JsonUtility.ToJson(memory);
        File.WriteAllText(Application.persistentDataPath + "/save.txt",json);
    }
    
    
    IEnumerator Loading()
    {   
        if(File.Exists(Application.persistentDataPath + "/save.txt"))
        {
          string savedString = File.ReadAllText(Application.persistentDataPath + "/save.txt");
          memory = JsonUtility.FromJson<Memory>(savedString);
        }
        yield return new WaitForEndOfFrame();
        enemyData.AddRange(memory.enemyData);
        playerData = memory.playerData;

    }
    

    public void AddEnemyData() //No añado los ScriptableObjects de los rivales porque no puede
                          //transformar en txt los objetos de unity como por ejemplo los GameObjects
    {
        foreach (var enemy in draft.EnemiesID)
        {
           enemyData.Add(new EnemyData(enemy)); 
        }
        
    }

    public void AddPlayerData() //currentface variables selected
    {
        //playerData.Add(new PlayerData(cargadorSo.currentIndex_hairs));
       // playerData.Add(new PlayerData(_pjSelectedSO.keepHair));
       // playerData.Add( new PlayerData(_pjSelectedSO.keepEyes));
       // playerData.Add ( new PlayerData(_pjSelectedSO.keepSkin));
       playerData.playerHair = _pjSelectedSO.keepHair;
       playerData.playerEyes = _pjSelectedSO.keepEyes;
       playerData.playerSkin = _pjSelectedSO.keepSkin;

    }

    public void LoadEnemyData()
    {
        StartCoroutine(("Loading"));
    }

}
