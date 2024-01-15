using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skeleton_Mod : Ability
{
    public override void UseAbility()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerManager.instance.UsedAb();

        }
        P_Move.isUpgradeDash = true;
    }

}
