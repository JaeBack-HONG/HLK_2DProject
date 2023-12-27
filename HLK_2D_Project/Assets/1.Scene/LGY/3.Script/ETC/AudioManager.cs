using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [SerializeField] private AudioSource[] source;

    [SerializeField] private AudioClip[] bgmClip;
    [SerializeField] private AudioClip[] sfxClip;
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

        source = GetComponents<AudioSource>();
        
    }

    private void Start()
    {
        source[0].clip = bgmClip[0];
        source[0].Play();
    }



    private void BGM_Play()
    {
        for (int i = 0; i < source.Length; i++)
        {
            if (!source[i].isPlaying)
            {
                source[i].Play();
                break;
            }
            
        }
    }
}
