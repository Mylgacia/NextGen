using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmShake : MonoBehaviour
{
    public static CmShake Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera cmVirtualCam;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        
    }
   
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cmVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
    void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f) //Timer over
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cmVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
