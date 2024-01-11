using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public abstract class Ability : MonoBehaviour
{
    [HideInInspector] public Player_State P_state;
    [HideInInspector] public Player_Move P_Move;
    [HideInInspector] public Rigidbody2D rigidbody;
    [HideInInspector] public Animator animator;
    [HideInInspector] public IEnumerator Double_co;
    public Image gaugeUI;
    public float anispeed = 1f;
    public CinemachineVirtualCamera cinemachinevir;
    public CinemachineBasicMultiChannelPerlin noise;

    private void Start()
    {
        cinemachinevir = FindObjectOfType<CinemachineVirtualCamera>();
        noise = cinemachinevir.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        TryGetComponent<Player_Move>(out P_Move);
        TryGetComponent<Player_State>(out P_state);
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<Animator>(out animator);
    }
    public abstract void UseAbility();
}
