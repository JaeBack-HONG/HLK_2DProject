using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public void MainGame_1()
    {
        SceneManager.LoadScene("HJB_Scene");
    }
    public void ScreenSet_All()
    {

    }
    public void ScreenSet_Window()
    {

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
