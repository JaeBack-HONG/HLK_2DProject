using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Angie_Mod : Ability
{

    public override void UseAbility()
    {
        P_state.actState = Unit_state.Idle;
        int havingAbCount = 0;
        AbCheck(ref havingAbCount);

        if (havingAbCount.Equals(0)) return;

        StartCoroutine(AddAbCount());
    }

    private void AbCheck(ref int idx)
    {
        for (int i = 0; i < PlayerManager.instance.count_List.Length; i++)
        {
            if (!PlayerManager.instance.count_List[i].Equals(0) && !PlayerManager.instance.count_List[i].Equals(4))
            {
                idx++;
            }
        }
    }

    private IEnumerator AddAbCount()
    {
        int randomidx = Random.Range(0, 3);
        while (PlayerManager.instance.count_List[randomidx].Equals(0) || PlayerManager.instance.count_List[randomidx].Equals(4))
        {
            randomidx = Random.Range(0, 3);
            yield return null;
        }
        PlayerManager.instance.UsedAb();
        yield return new WaitForSeconds(0.15f);
        PlayerManager.instance.count_List[randomidx]++;
        P_state.Health--;
        PlayerManager.instance.HeartCheck(P_state.Health);

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.Select_Idx] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }

        yield return null;
    }
}
