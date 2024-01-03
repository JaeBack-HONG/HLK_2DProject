using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Option_Btn : MonoBehaviour
{    
    [SerializeField] private GameObject optionUI_obj;

    private void Awake()
    {
        optionUI_obj = GameObject.Find("UI_Option_Panel");
    }

    public void OnClickOption_Btn()
    {              
        
        Debug.Log(optionUI_obj);
        optionUI_obj.SetActive(!optionUI_obj.activeSelf);        
    }
}
