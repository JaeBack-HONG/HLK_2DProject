using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ability : MonoBehaviour
{

    public Ability[] abilities;
    public Ability[] my_Abilities;
    public Ability current_Ab;

    private Player_State P_state;
    [SerializeField] private CircleCollider2D absorbcol;
    private IEnumerator absorbab_co;

    private void Awake()
    {
        P_state = GetComponent<Player_State>();
    }

    public void Choice_Ab()
    {
        if (Input.GetKeyDown(KeyCode.Q)) AbilitySet(0);

        if (Input.GetKeyDown(KeyCode.W)) AbilitySet(1);

        if (Input.GetKeyDown(KeyCode.E)) AbilitySet(2);
    }

    public void UseAbsorb()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            absorbab_co = AbsorbAb_co();
            StartCoroutine(absorbab_co);
        }
    }

    private IEnumerator AbsorbAb_co()
    {
        P_state.animator.SetTrigger("Absorb");
        yield return new WaitForSeconds(0.3f);
        absorbcol.enabled = true;
        yield return new WaitForSeconds(0.7f);
        P_state.animator.SetTrigger("Idle");
        absorbcol.enabled = false;
    }

    public void AbilitySet(int idx)
    {
        PlayerManager.instance.Select_Idx = idx;
        current_Ab = my_Abilities[PlayerManager.instance.Select_Idx];
        PlayerManager.instance.Border_Link(idx);
    }

   

    public void TriggerEvent(int current_idx, Collider2D collision)
    {
        PlayerManager.instance.AbIdx[current_idx] = collision.gameObject.GetComponent<AbilityItem>().itemidx;
        my_Abilities[current_idx] = abilities[(int)PlayerManager.instance.AbIdx[current_idx]];
        PlayerManager.instance.icon_Image[current_idx].sprite = PlayerManager.instance.icon_Image_All[(int)PlayerManager.instance.AbIdx[current_idx]];
        LinkUI(current_idx, collision);
    }

    public void LinkUI(int current_idx, Collider2D collision)
    {
        current_Ab = my_Abilities[current_idx];
        PlayerManager.instance.count_List[current_idx] = PlayerManager.instance.max_Count;
        PlayerManager.instance.icon_Bar[current_idx].sprite = PlayerManager.instance.icon_Bar_All[current_idx * 5 + 4];
        PlayerManager.instance.Border_Link(current_idx);
        Destroy(collision.gameObject);
    }

   

}
