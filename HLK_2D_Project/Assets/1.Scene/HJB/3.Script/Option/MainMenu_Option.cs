using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu_Option : MonoBehaviour
{
    

    
    public void OptionUI_Btn()
    {
        GameObject optionUI = GameObject.Find("UI_Option_Panel").GetComponent<GameObject>();
        optionUI.SetActive(!optionUI.activeSelf);
    }

    public void MainGame_Btn()
    {
        GameManager.instance.MainGame_1();
    }

    public void MainMenuSceneLoadData_Btn()
    {
       
        GameManager.instance.SceneLoadData_Btn();       
        
    }
}
