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
    [SerializeField] private GameObject PlayerDieUI_obj;
    [SerializeField] private GameObject ClearUI_obj;
    [SerializeField] private Text playTimeText_obj;
    
    [SerializeField] private string introSceneName;
    [SerializeField] private int currentSceneName;
    [SerializeField] private int saveSceneName;
    
    private AudioSource bgmAudio;

    public PlayerDataJson PlayerData;

    public Player_State player;
    public PlayerManager player_Ability;

    [SerializeField] float time = 0f;

    [Header("플레이어 체력 설정")]
    [SerializeField] int MaxHealthSet = 6;
    [SerializeField] int CurrentHealthSet = 6;
    [NonSerialized] public bool gameClear = false;

    private void Awake()
    {        
        FileDataCheck();
        
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
        TryGetComponent<AudioSource>(out bgmAudio);
    }   
    
    

    private void Update()
    {
        TimeChekc();
        GetCompoPlayerCheck();
        OptionKeyDown_Set();       

        if(Input.GetKeyDown(KeyCode.M))
        {
            Time.timeScale = 0.1f;
        }
    }

    public void OnClearUI()
    {
        playTimeText_obj.text = $"{time.ToString("F2")}초";
        ClearUI_obj.SetActive(true);
        DefaultDataSet();
        DataSave();
        gameClear = false;
    }

    private void TimeChekc()
    {
        if (SceneManager.GetActiveScene().name != introSceneName&&
            SceneManager.GetActiveScene().name != "tutorial")
        {
            time += Time.deltaTime;
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
        if (!SceneManager.GetActiveScene().name.Equals(introSceneName)&&!gameClear)
        {
            player_Ability = FindObjectOfType<PlayerManager>();        
            currentSceneName = SceneManager.GetActiveScene().buildIndex;
            PlayerData.SceneName = currentSceneName;

            PlayerData.currentHealth = player.Health;
            PlayerData.maxHealth = player.MaxHealth;

            PlayerData.AbilityCheck_1 = PlayerManager.instance.count_List[0];
            PlayerData.AbilityCheck_2 = PlayerManager.instance.count_List[1]; 
            PlayerData.AbilityCheck_3 = PlayerManager.instance.count_List[2]; 
            //현재 능력 종류
            PlayerData.Ability_1 = (int)player_Ability.AbIdx[0];
            PlayerData.Ability_2 = (int)player_Ability.AbIdx[1];
            PlayerData.Ability_3 = (int)player_Ability.AbIdx[2];
            
            //현재 능력 사용횟수
            PlayerData.Ability_1_count = PlayerManager.instance.count_List[0];
            PlayerData.Ability_2_count = PlayerManager.instance.count_List[1];
            PlayerData.Ability_3_count = PlayerManager.instance.count_List[2];

            PlayerData.SpeedrunTime = time;            
            
        }
        
        string fileName;

        fileName = Application.dataPath + "/PlayerDataJson.json";
        string toJson = JsonConvert.SerializeObject(PlayerData, Formatting.Indented);
        
        File.WriteAllText(fileName, toJson);
    }

    public void SceneDataSave()
    {
        currentSceneName = SceneManager.GetActiveScene().buildIndex;
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
            PlayerData = DataLoad();
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
        PlayerData.maxHealth = MaxHealthSet;
        PlayerData.currentHealth = CurrentHealthSet;
        PlayerData.SceneName = 0;

        PlayerData.AbilityCheck_1 = 0;
        PlayerData.AbilityCheck_2 = 0;
        PlayerData.AbilityCheck_3 = 0;

        PlayerData.Ability_1 = 0;
        PlayerData.Ability_2 = 0;
        PlayerData.Ability_3 = 0;

        PlayerData.Ability_1_count = 0;
        PlayerData.Ability_2_count = 0;
        PlayerData.Ability_3_count = 0;

        PlayerData.SpeedrunTime = 0;
        time = 0f;        

    }    

    public void PlayerDieUI()
    {
        PlayerDieUI_obj.SetActive(true);
    }

    #region //버튼 이벤트 메서드
    public void MainGame_1()
    {        
        DefaultDataSet();
        //DataLoad();
        DataSave();

        AudioManager.Instance.BGM_Play((int)Scene_Name.tutorial);
        SceneManager.LoadScene((int)Scene_Name.tutorial);
        
    }
    public void MainMenu_Btn()
    {
        ClearUI_obj.SetActive(false);        
        

        if (!player.actState.Equals(Unit_state.Die))
        {
            OptionUI_obj.SetActive(!OptionUI_obj.activeSelf);
        }
        else
        {
            PlayerDieUI_obj.SetActive(!PlayerDieUI_obj.activeSelf);
        }
        Time.timeScale = OptionUI_obj.activeSelf ? 0 : 1;
        SceneDataSave();
        AudioManager.Instance.BGM_Play((int)Scene_Name.MainMenu);
        SceneManager.LoadScene((int)Scene_Name.MainMenu);        
    }        

    public void OnClickOption_Btn()
    {
        OptionUI_obj.SetActive(!OptionUI_obj.activeSelf);
        Time.timeScale = OptionUI_obj.activeSelf ? 0 : 1;
    }
    public void SceneLoadData_Btn()
    {        
        DataLoad();
        AudioManager.Instance.BGM_Play(PlayerData.SceneName);
        SceneManager.LoadScene(PlayerData.SceneName);
    }
    #endregion
    public void ExitGame()
    {
        ClearUI_obj.SetActive(false);
        //클리어시 초기화 넣어주기
        if (gameClear)
        {
            DefaultDataSet();
        }
#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
