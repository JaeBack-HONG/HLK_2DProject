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
        //씬이동후 현재 씬 저장
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
        //게임 종료일 때 저장
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
        //if (player != null)
        //{
        //    
        //    PlayerData.maxHealth = 0;
        //    PlayerData.currentHealth = player.Health;
        //}
        currentSceneName = SceneManager.GetActiveScene().name;
        PlayerData.SceneName = currentSceneName;
        string fileName;

        fileName = Application.dataPath + "/PlayerDataJson.json";
        string toJson = JsonConvert.SerializeObject(PlayerData, Formatting.Indented);
        File.WriteAllText(fileName, toJson);        
    }
    public void FileDataCheck()
    {
        string fileName;

        fileName = Application.dataPath + "/PlayerDataJson.json";       
        
        if (!File.Exists(fileName))
        {
            File.Create(fileName);            
            string toJson = JsonConvert.SerializeObject(PlayerData, Formatting.Indented);
            File.WriteAllText(fileName, toJson);

        }
        else if (File.Exists(fileName))
        {
            DataLoad();            
        }              
    }
    public PlayerDataJson DataLoad()
    {        
        string fileName;
        try
        {            
            fileName = Application.dataPath + "/PlayerDataJson.json";            
            string readData = File.ReadAllText(fileName);            
            PlayerData = JsonConvert.DeserializeObject<PlayerDataJson>(readData);            
            return PlayerData;
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log(e);
            return null;            
        }
    }
    private void DefaultDataSet()
    {
        PlayerData.maxHealth = 3;
        PlayerData.currentHealth = 3;
        PlayerData.SceneName = "";
    }    

    #region //버튼 이벤트 메서드
    public void MainGame_1()
    {        
        DefaultDataSet();
        DataSave();
        SceneManager.LoadScene("HJB_Scene");
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
