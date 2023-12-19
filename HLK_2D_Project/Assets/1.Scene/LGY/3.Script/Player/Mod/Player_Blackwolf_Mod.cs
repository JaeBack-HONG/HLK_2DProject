using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Blackwolf_Mod : Player_Ability
{
    public override void UseAbility(Animator player_ani, int changeidx)
    {

        player_ani = GameManager.instance.animators[changeidx];

        // 기능구현


        player_ani = GameManager.instance.animators[(int)Animator_List.Player];
    }
}
