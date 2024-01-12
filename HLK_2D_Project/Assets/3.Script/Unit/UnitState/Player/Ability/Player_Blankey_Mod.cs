using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Blankey_Mod : Ability
{

    [SerializeField] private GameObject detectbox;

    public override void UseAbility()
    {
        StartCoroutine(OnOffDetect());
    }
    
    IEnumerator OnOffDetect()
    {
        DefaulutSet("BlankeyMod");
        rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionY;
        detectbox.SetActive(true);
        PlayerManager.instance.UsedAb();
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.3f/anispeed);

        Time.timeScale = 1f;

        detectbox.SetActive(false);
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        EndSet();

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.current_Count] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }

        yield return null;
    }

}
