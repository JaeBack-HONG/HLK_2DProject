using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ability : MonoBehaviour
{

    [SerializeField] private Ability[] abilities;
    private Ability[] my_Abilities;
    public Ability current_Ab;

    private void Awake()
    {
        my_Abilities = new Ability[3];
    }

    public void Choice_Ab()
    {
        if (Input.GetKeyDown(KeyCode.Q)) AbilitySet(0);

        if (Input.GetKeyDown(KeyCode.W)) AbilitySet(1);

        if (Input.GetKeyDown(KeyCode.E)) AbilitySet(2);
    }

    private void AbilitySet(int idx)
    {
        PlayerManager.instance.current_Count = idx;
        current_Ab = my_Abilities[PlayerManager.instance.current_Count];
        PlayerManager.instance.Border_Link(idx);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Item") && Input.GetKey(KeyCode.R))
        {
            
            for (int i = 0; i < my_Abilities.Length; i++)
            {
                if (my_Abilities[i] == null)
                {
                    PlayerManager.instance.current_Count = i;
                    break;
                }
            }
            TriggerEvent(PlayerManager.instance.current_Count, collision);

        }
    }
    private void TriggerEvent(int current_idx, Collider2D collision)
    {
        my_Abilities[current_idx] = abilities[(int)collision.gameObject.GetComponent<AbilityItem>().itemidx];
        PlayerManager.instance.icon_Image[current_idx].sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
        current_Ab = my_Abilities[current_idx];
        PlayerManager.instance.count_List[current_idx] = PlayerManager.instance.max_Count;
        PlayerManager.instance.icon_Bar[current_idx].sprite = PlayerManager.instance.icon_Bar_All[current_idx * 5 + 4];
        Destroy(collision.gameObject);
    }
}
