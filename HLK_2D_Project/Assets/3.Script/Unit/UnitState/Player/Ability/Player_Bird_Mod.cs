using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bird_Mod : Ability
{
    public override void UseAbility()
    {
        PlayerManager.instance.UsedAb();
        PlayerManager.instance.UsedAb();
        PlayerManager.instance.UsedAb();
        PlayerManager.instance.UsedAb();
        P_Move.maxJumps = 2;
        P_Move.jumpCount = 2;
    }
}
