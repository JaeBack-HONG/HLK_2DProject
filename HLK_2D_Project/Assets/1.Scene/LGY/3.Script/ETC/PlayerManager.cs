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
    public Image[] icon_Image;
    public Image[] icon_Bar;
    public Sprite[] icon_Bar_All;

    [Header("체력UI")]
    public Image[] heart_Panels;
    public Sprite[] heart_All;

    [Header("능력카운트")]
    public int[] count_List;
    public int current_Count = 0;
    public int max_Count = 4;


    [SerializeField] private Player_Ability P_Ab;
    [SerializeField] private float RemoveGuage = 0;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        count_List = new int[3];
        for (int i = 0; i < icon_Bar.Length; i++)
        {
            icon_Bar[i].sprite = icon_Bar_All[i * 5];
            count_List[i] = 0;
        }

    }

    private void FixedUpdate()
    {
        RemoveItem();
        ResetAbList();
    }

    #region Ability Reset
    private void ResetAbList()
    {
        icon_Bar[current_Count].sprite = icon_Bar_All[current_Count * 5 + count_List[current_Count]];
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
