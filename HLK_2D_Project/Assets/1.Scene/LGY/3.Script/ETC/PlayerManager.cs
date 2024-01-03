using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    [Header("직접참조")]
    public GameObject[] icon_Border;
    public Image[] icon_Image;
    public Image[] icon_Bar;
    public Sprite[] icon_Bar_All;

    public int[] count_List;
    public int current_Count = 0;
    public int max_Count = 4;

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
        // 실시간으로 능력 쓸 때마다 UI연동
        icon_Bar[current_Count].sprite = icon_Bar_All[current_Count * 5 + count_List[current_Count]];

    }

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
