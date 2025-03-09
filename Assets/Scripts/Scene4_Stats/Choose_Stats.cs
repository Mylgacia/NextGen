using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Choose_Stats : MonoBehaviour
{
    public Image _image;
    public PlayerStats statsSO;
    public UIStats script;
    
    public Button moreStats;
    public Button lessStats;

    public int currentIndex = 2;
    private void Awake()
    {
        AudioManager.instance.menuSource.mute = true; 
    }
    void Start()
    {
        StartCoroutine(Sounds());
        if (statsSO.stats != null & statsSO.stats.Length > 0) DisplayCurrentStats();

    }

    
    void Update()
    {
        
    }
    
    public void DisplayCurrentStats() //Pon el sprite actual numero tal 
    {
       
        _image.sprite = statsSO.stats[currentIndex];
        AudioManager.instance.PlayMenuSounds("Click");
       
    }

    public void MoreStats()
    {
        //Necesito llamar a UIStats para ver si el Counter es 0 de 5
        if (script.counter > 0 & currentIndex <5 )
        {
             currentIndex++;
             script.counter--;
                    
             if (currentIndex >= statsSO.stats.Length) currentIndex = 0;
            
             DisplayCurrentStats();
             script.UpdateCounter();
        }
       
    }
    
    public void LessStats()
    {
        if (script.counter < 5 & currentIndex >2 )
        {
            currentIndex--;
            script.counter++;
                    
            if (currentIndex < 0) currentIndex = statsSO.stats.Length - 1;
            
            DisplayCurrentStats();
            script.UpdateCounter();
        }
        
    }
    
    public void SelectionStats() 
    {
        
        if (EventSystem.current.currentSelectedGameObject == moreStats.gameObject)
        {
            moreStats.onClick.Invoke(); 
            
        } else if (EventSystem.current.currentSelectedGameObject == lessStats.gameObject)
            
        {
            lessStats.onClick.Invoke();
        }
        
    }

    IEnumerator Sounds()
    {
        yield return new WaitForSeconds(1);
        AudioManager.instance.menuSource.mute = false;
    }

}
