using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControll_Warrior : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cinemachinevir_Intro;
    [SerializeField] private CinemachineVirtualCamera cinemachinevir_Boss;
        
    private CinemachineBasicMultiChannelPerlin noise;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals("Player"))
        {

        }
    }
}
