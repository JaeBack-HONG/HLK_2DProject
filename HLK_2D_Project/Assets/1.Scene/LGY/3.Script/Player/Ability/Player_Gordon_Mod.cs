using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Gordon_Mod : Ability
{
    [SerializeField] private BoxCollider2D guardcol;

    public override void UseAbility()
    {
        StartCoroutine(guard_co());
    }

    IEnumerator guard_co()
    {
        DefaulutSet("GordonMod");
        PlayerManager.instance.UsedAb();
        guardcol.enabled = true;
        while (Input.GetKey(KeyCode.Z))
        {
            yield return new WaitForFixedUpdate();
        }
        guardcol.enabled = false;
        EndSet();

    }
}
