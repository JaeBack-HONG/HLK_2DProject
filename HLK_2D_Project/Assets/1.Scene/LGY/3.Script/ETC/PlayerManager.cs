using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    private void Awake()
    {
        if(instance==null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            return;
        }
    }


}
