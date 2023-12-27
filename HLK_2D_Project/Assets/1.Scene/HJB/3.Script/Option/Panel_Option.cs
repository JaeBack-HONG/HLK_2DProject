using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Panel_Option : MonoBehaviour
{
    public static Panel_Option Instance = null;

    [SerializeField] private Slider master_volume;
    [SerializeField] private Slider bgm_volume;
    [SerializeField] private Slider sfx_volume;

    [SerializeField] private AudioMixer mixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }        
    }

    private void Start()
    {
        mixer.SetFloat("Master", Mathf.Log10(master_volume.value) * 20);
        mixer.SetFloat("BGM", Mathf.Log10(bgm_volume.value) * 20);
        mixer.SetFloat("SFX", Mathf.Log10(sfx_volume.value) * 20);
    }


    public void Master_VolumeSet()
    {
        
        mixer.SetFloat("Master", Mathf.Log10(master_volume.value)*20);
    }

    public void BGM_VolumeSet()
    {
        
        mixer.SetFloat("BGM", Mathf.Log10(bgm_volume.value) * 20);
    }
    public void SFX_VolumeSet()
    {
        mixer.SetFloat("SFX", Mathf.Log10(sfx_volume.value) * 20);
    }
}
