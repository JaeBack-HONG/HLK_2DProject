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

    public void MainGame()
    {
        SceneManager.LoadScene("HJB_Scene");
    }
}
