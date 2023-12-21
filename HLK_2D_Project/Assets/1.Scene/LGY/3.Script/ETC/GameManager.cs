using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Layer_Index
{
    Ground =6,
    Enemy,
    Player,
    Hit,
}
public enum Animator_List
{
    Player = 0,
    Brown,
    Hope1,
    Knight1,
    BlackWolf,
}
public enum Ability_List
{
    Brown = 0,
    Knight,
    Hope,
    BlackWolf,
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

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
