using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    [Header("직접참조")]
    [SerializeField] private GameObject[] abilityHUD;
    [SerializeField] private Image[] abilityImgs;
    public Image[] abilityGuageUI;
    public Sprite[] abilityGauge;
    [SerializeField] private Ability[] abilities;

    private Ability[] my_Abilities;
    [HideInInspector] public Ability current_Ab;

    public int[] abilitycount;
    public int current_idx = 0;
    private int maxcount = 4;

    //private void Awake()
    //{
    //    for (int i = 0; i < abilityGuageUI.Length; i++)
    //    {
    //        abilityGuageUI[i].sprite = abilityGauge[i * 5];

    //    }
    //    abilitycount = new int[3];
    //    my_Abilities = new Ability[3];
    //    current_Ab = my_Abilities[current_idx];
    //}

    private void FixedUpdate()
    {
        abilityGuageUI[current_idx].sprite = abilityGauge[current_idx * 5 + abilitycount[current_idx]];
    }

    public void Choice_Ab()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Ab_Set(0);

        if (Input.GetKeyDown(KeyCode.W)) Ab_Set(1);

        if (Input.GetKeyDown(KeyCode.E)) Ab_Set(2);

    }

    private void Ab_Set(int idx)
    {
        AbilitySet(idx);
        for (int i = 0; i < abilityHUD.Length; i++)
        {
            abilityHUD[i].SetActive(false);
        }
        abilityHUD[idx].SetActive(true);
    }

    private void AbilitySet(int idx)
    {
        current_idx = idx;
        current_Ab = my_Abilities[current_idx];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            for (int i = 0; i < my_Abilities.Length; i++)
            {
                if (my_Abilities[i] == null)
                {
                    current_idx = i;
                    break;
                }
            }
            {
                my_Abilities[current_idx] = abilities[(int)collision.gameObject.GetComponent<AbilityItem>().itemidx];
                abilityImgs[current_idx].sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
                current_Ab = my_Abilities[current_idx];
                abilitycount[current_idx] = maxcount;
                abilityGuageUI[current_idx].sprite = abilityGauge[current_idx * 5 + 4];
                Destroy(collision.gameObject);
            }

        }




    }

    private void Awake()
    {
        if(instance==null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


}
