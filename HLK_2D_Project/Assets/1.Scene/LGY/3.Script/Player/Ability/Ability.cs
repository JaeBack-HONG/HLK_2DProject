using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class Ability : MonoBehaviour
{
    [HideInInspector] public Player_State P_state;
    [HideInInspector] public Rigidbody2D rigidbody;
    [HideInInspector] public Animator animator;
    public float anispeed = 1f;
    public CinemachineVirtualCamera cinemachinevir;
    public CinemachineBasicMultiChannelPerlin noise;

    private void Start()
    {
        cinemachinevir = FindObjectOfType<CinemachineVirtualCamera>();
        noise = cinemachinevir.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        TryGetComponent<Player_State>(out P_state);
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<Animator>(out animator);
    }
    public abstract void UseAbility();

}
