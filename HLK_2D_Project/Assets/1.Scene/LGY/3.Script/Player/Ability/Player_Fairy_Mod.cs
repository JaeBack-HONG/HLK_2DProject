using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fairy_Mod : Ability
{
    public override void UseAbility()
    {
        P_state.actState = Unit_state.Idle;

        if (P_state.isFairy) return;

        P_state.isFairy = true;
        PlayerManager.instance.UsedAb();
    }

    //��� �� ���� ����ϰԵǴ� �ɷ��� �ߵ��� 2ȸ�� ����ȴ�. �ɷº�������


}
