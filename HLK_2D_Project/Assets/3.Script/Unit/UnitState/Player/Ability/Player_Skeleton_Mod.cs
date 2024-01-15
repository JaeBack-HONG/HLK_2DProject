using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skeleton_Mod : Ability
{
    public override void UseAbility()
    {
        PlayerManager.instance.UsedAb();
        PlayerManager.instance.UsedAb();
        PlayerManager.instance.UsedAb();
        PlayerManager.instance.UsedAb();
        P_Move.isUpgradeDash = true;
    }

}
