using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using Newtonsoft.Json;

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
    [SerializeField] private string introSceneName;
    [SerializeField] private string currentSceneName;
    [SerializeField] private string saveSceneName;

    public PlayerDataJson PlayerData;

    public Player_State player;

    private void Awake()
    {             
        
        if (instance == null)
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

    
    private void Start()
    {
        FileDataCheck();
        
        //���̵��� ���� �� ����
        currentSceneName = SceneManager.GetActiveScene().name;
        if (!currentSceneName.Equals(introSceneName))
        {            
            DataSave();
        }
    }

    private void Update()
    {
        OptionKeyDown_Set();
        GetCompoPlayerCheck();
    }
    private void OnApplicationQuit()
    {
        //���� ������ �� ����
        currentSceneName = SceneManager.GetActiveScene().name;
        if (!currentSceneName.Equals(introSceneName))
        {
            DataSave();
        }
    }
    private void OptionKeyDown_Set()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionUI_obj.SetActive(!OptionUI_obj.activeSelf);

            Time.timeScale = OptionUI_obj.activeSelf ? 0 : 1;            
        }
    }
        
    private void GetCompoPlayerCheck()
    {        
        try
        {
            if (SceneManager.GetActiveScene().name != introSceneName && player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();
            }

        }
        catch (NullReferenceException e)
        {
            player = null;
        }
    }
    public void DataSave()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        PlayerData.maxHealth = 0;
        PlayerData.currentHealth = player.Health;
        PlayerData.SceneName = currentSceneName;
        string fileName;

        fileName = Application.dataPath + "/Data" + "/PlayerDataJson.json";
        string toJson = JsonConvert.SerializeObject(PlayerData, Formatting.Indented);
        File.WriteAllText(fileName, toJson);
        Debug.Log(toJson);
    }
    public void FileDataCheck()
    {
        string fileName;

        fileName = Application.dataPath + "/Data"+ "/PlayerDataJson.json";       
        
        if (!File.Exists(fileName))
        {
            File.Create(fileName);            

        }
        else if (File.Exists(fileName))
        {
            Debug.Log("���� True��");
        }      
        string toJson = JsonConvert.SerializeObject(PlayerData, Formatting.Indented);
        File.WriteAllText(fileName, toJson);
        Debug.Log(toJson);
        
    }
    public PlayerDataJson DataLoad()
    {        
        string fileName;
        try
        {            
            fileName = Application.dataPath + "/Data" + "/PlayerDataJson.json";
            Debug.Log(fileName);
            string readData = File.ReadAllText(fileName);
            PlayerDataJson playerData = new PlayerDataJson();
            PlayerData = JsonConvert.DeserializeObject<PlayerDataJson>(readData);
            PlayerData = playerData;
            return playerData;
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log(e);
            return null;
            
        }
    }
    public void DeletSaveData()
    {
        string fileName;
        fileName = Application.dataPath + "/Data" + "/PlayerDataJson.json";
        File.Delete(fileName);
    }


    #region //��ư �̺�Ʈ �޼���
    public void MainGame_1()
    {
        SceneManager.LoadScene("HJB_Scene");
        GetCompoPlayerCheck();
    }
    public void MainMenu_Btn()
    {           
        OptionUI_obj.SetActive(!OptionUI_obj.activeSelf);
        Time.timeScale = OptionUI_obj.activeSelf ? 0 : 1;
        DataSave();
        SceneManager.LoadScene("MainMenu");        
    }        

    public void OnClickOption_Btn()
    {
        OptionUI_obj.SetActive(!OptionUI_obj.activeSelf);
        Time.timeScale = OptionUI_obj.activeSelf ? 0 : 1;
    }
    public void SceneLoadData_Btn()
    {
        PlayerData = DataLoad();        
        SceneManager.LoadScene(PlayerData.SceneName);
    }
    #endregion
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
