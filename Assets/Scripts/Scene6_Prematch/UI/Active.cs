using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.instance.PlayMatchSounds("Whistle01"); 
    }
}
