using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ability : MonoBehaviour
{
    [Header("직접참조")]
    [SerializeField] private GameObject[] AbilityHUD;
    [SerializeField] private Image[] AbilityImgs;
    [SerializeField] private Ability[] abilities;

    private Ability[] my_Abilities;
    [HideInInspector] public Ability current_Ab;

    public int current_idx = 0;

    private void Awake()
    {
        my_Abilities = new Ability[3];
        current_Ab = my_Abilities[current_idx];
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
        for (int i = 0; i < AbilityHUD.Length; i++)
        {
            AbilityHUD[i].SetActive(false);
        }
        AbilityHUD[idx].SetActive(true);
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
            my_Abilities[current_idx] = abilities[(int)collision.gameObject.GetComponent<AbilityItem>().itemidx];
            AbilityImgs[current_idx].sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            current_Ab = my_Abilities[current_idx];
            Destroy(collision.gameObject);
        }

    }





}
