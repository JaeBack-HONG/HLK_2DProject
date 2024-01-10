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

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private GameObject OptionUI_obj;
    

    private void Awake()
    {        
        if (instance == null )
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    private void Update()
    {
        OptionKeyDown_Set();
    }

    private void OptionKeyDown_Set()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionUI_obj.SetActive(!OptionUI_obj.activeSelf);

            Time.timeScale = OptionUI_obj.activeSelf ? 0 : 1;            
        }
    }

    public void MainGame_1()
    {
        SceneManager.LoadScene("HJB_Scene");        
    }
    public void MainMenu_Btn()
    {           
        OptionUI_obj.SetActive(!OptionUI_obj.activeSelf);
        Time.timeScale = OptionUI_obj.activeSelf ? 0 : 1;
        SceneManager.LoadScene("MainMenu");        
    }
        

    public void OnClickOption_Btn()
    {
        OptionUI_obj.SetActive(!OptionUI_obj.activeSelf);
        Time.timeScale = OptionUI_obj.activeSelf ? 0 : 1;
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
