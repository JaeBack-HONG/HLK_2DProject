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

    //사용 시 다음 사용하게되는 능력이 발동이 2회로 실행된다. 능력복사제외


}
