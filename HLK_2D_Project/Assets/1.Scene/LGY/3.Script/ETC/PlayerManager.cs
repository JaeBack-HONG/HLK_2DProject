using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Ability_List
{
    Brown = 0,
    Knight,
    Hope,
    BlackWolf,
    Handrick,
    BabyBoss,
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    [Header("능력UI")]
    public GameObject[] icon_Border;
    public Sprite[] AbilityItem_All;
    public Image[] icon_Image;
    public Image[] icon_Bar;
    public Sprite[] icon_Bar_All;

    [Header("체력UI")]
    public Image[] heart_Panels;
    public Sprite[] heart_All;

    [Header("능력카운트")]
    public int[] count_List; // 사용 가능 횟수 0~4
    public int current_Count = 0; // 현재 선택한 능력
    public int max_Count = 4;

    PlayerDataJson playerData;

    [SerializeField] private Player_Ability P_Ab;
    [SerializeField] private float RemoveGuage = 0;
    public Ability_Item[] AbIdx;
    private void Awake()
    {
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        


    }
    private void Start()
    {
        AbilityDataLoad();

    }
    private void AbilityDataLoad()
    {
        Debug.Log("초기화");
        
        P_Ab.my_Abilities = new Ability[3];
        count_List = new int[3];
        AbIdx = new Ability_Item[3]; 
        

        P_Ab.my_Abilities[0] = P_Ab.abilities[GameManager.instance.PlayerData.Ability_1];
        P_Ab.my_Abilities[1] = P_Ab.abilities[GameManager.instance.PlayerData.Ability_2];
        P_Ab.my_Abilities[2] = P_Ab.abilities[GameManager.instance.PlayerData.Ability_3];
        
        
        count_List[0] = GameManager.instance.PlayerData.AbilityCheck_1;
        count_List[1] = GameManager.instance.PlayerData.AbilityCheck_2;
        count_List[2] = GameManager.instance.PlayerData.AbilityCheck_3;
        
        count_List[0] = GameManager.instance.PlayerData.Ability_1_count;
        count_List[1] = GameManager.instance.PlayerData.Ability_2_count;
        count_List[2] = GameManager.instance.PlayerData.Ability_3_count;
        StartSetting();

    }
    private void FixedUpdate()
    {
        
        RemoveItem();
        ResetAbList();      
        
    }

    public void StartSetting()
    {
        AbIdx[0] = (Ability_Item)GameManager.instance.PlayerData.Ability_1;
        AbIdx[1] = (Ability_Item)GameManager.instance.PlayerData.Ability_2;
        AbIdx[2] = (Ability_Item)GameManager.instance.PlayerData.Ability_3;
        for (int i = 0; i < 3; i++)
        {
            icon_Image[i].sprite = AbilityItem_All[(int)AbIdx[i]];
            icon_Bar[i].sprite = icon_Bar_All[i * 5 + 4];
            
        }
        for (int i = 0; i < icon_Image.Length; i++)
        {
            if (count_List[i].Equals(0))
            {
                icon_Image[i].sprite = null;
            }            
        }               
    }

    #region Ability Reset
    public void ResetAbList()
    {
        for (int i = 0; i < count_List.Length; i++)
        {
            icon_Bar[i].sprite = icon_Bar_All[i * 5 + count_List[i]];

        }
        if (count_List[current_Count].Equals(0))
        {
            int idx = current_Count;
            icon_Image[current_Count].sprite = null;
            for (int i = 0; i < icon_Border.Length; i++)
            {
                if (!count_List[i].Equals(0))
                {
                    P_Ab.AbilitySet(current_Count);
                    break;
                }
            }
            if (current_Count.Equals(idx)) icon_Border[idx].SetActive(false);
        }
    }
    #endregion

    private void RemoveItem()
    {
        if (Input.GetKey(KeyCode.T))
        {
            RemoveGuage += Time.fixedDeltaTime;
            if (RemoveGuage >= 2)
            {
                count_List[current_Count] = 0;
            }
        }
        else RemoveGuage = 0;
    }

    #region PlayerHPCheck
    public void HeartCheck(int Health)
    {
        int currentHealth = Health;
        currentHealth = (int)(currentHealth * 0.5f) - 1;
        Health = Health % 2;

        for (int i = 0; i < heart_Panels.Length; i++)
        {
            if (i <= currentHealth) heart_Panels[i].sprite = heart_All[2];
            else if (i.Equals(currentHealth + 1) && Health.Equals(1)) heart_Panels[i].sprite = heart_All[1];
            else heart_Panels[i].sprite = heart_All[0];
        }
    }
    #endregion

    public void Border_Link(int idx)
    {
        for (int i = 0; i < icon_Border.Length; i++)
        {
            icon_Border[i].SetActive(false);
        }
        icon_Border[idx].SetActive(true);
    }

    public void UsedAb()
    {
        count_List[current_Count]--;
    }
}
