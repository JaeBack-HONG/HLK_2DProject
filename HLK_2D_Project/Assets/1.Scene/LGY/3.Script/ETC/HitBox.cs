using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class HitBox : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachinevir;
    public CinemachineBasicMultiChannelPerlin noise;
    public Player_State P_state;

    [Range(1, 5)]
    [SerializeField] private int dmg = 2;

    [Range(0f, 0.5f)]
    [SerializeField] private float noiseTime = 0.15f;   
    
    [Range(0f, 10f)]
    [SerializeField] private float noiseScale = 3f;


    private void Start()
    {
        transform.root.TryGetComponent<Player_State>(out P_state);
        cinemachinevir = FindObjectOfType<CinemachineVirtualCamera>();
        noise = cinemachinevir.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void HitDmg(Monster_State monstate)
    {
        monstate.Health -= dmg;
        P_state.Attack(monstate);
    }

    public IEnumerator WindowNoise()
    {
        noise.m_AmplitudeGain = noiseScale;
        yield return new WaitForSeconds(noiseTime);
        noise.m_AmplitudeGain = 0;
        yield return null;
    }
}
