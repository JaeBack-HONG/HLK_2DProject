using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Layer_Index
{
    Ground =6,
    Enemy,
    Player
}
public enum Animator_List
{
    Player = 0,
    Brown,
    Hope1,
    Knight1,
    BlackWolf
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Animator[] animators;


    private void Awake()
    {
        if (instance = null)
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
