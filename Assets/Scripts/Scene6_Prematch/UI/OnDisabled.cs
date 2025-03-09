using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisabled : MonoBehaviour
{
  
    private void OnDisable()
    {
       PauseManager.Instance.Pause();  
    }
}
