using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public AudioSource BGMAudio;
    public AudioSource[] SFXAudio;

    public AudioClip[] bgmClip;
    public AudioClip[] sfxClip;
    private void Awake()
    {
        if (Instance == null )
        {
            
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        SFXAudio = GetComponents<AudioSource>();
        
    }

    private void Start()
    {
        BGM_Play(0);

    }

    public void BGM_Play(int idx)
    {
        BGMAudio.clip = bgmClip[idx];
        BGMAudio.Play();
    }

    private void SFX_Play()
    {
        for (int i = 0; i < SFXAudio.Length; i++)
        {
            if (!SFXAudio[i].isPlaying)
            {
                SFXAudio[i].Play();
                break;
            }
            
        }
    }
}
