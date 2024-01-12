using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Percy_Mod : Ability
{
    [SerializeField] private GameObject Percy_FireBall_obj;

    public override void UseAbility()
    {
        StartCoroutine(PercyAttack_Co());
    }

    private IEnumerator PercyAttack_Co()
    {

        DefaulutSet("PercyMod");

        PlayerManager.instance.UsedAb();
        yield return new WaitForSeconds(0.35f / anispeed);



        CreateBullet();

        yield return new WaitForSeconds(0.25f / anispeed);

        EndSet();

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.current_Count] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }

        yield return null;
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(Percy_FireBall_obj,
            new Vector3(transform.position.x + P_state.direction.x, transform.position.y + 0.2f), Quaternion.identity);
        Player_Projectile bullet_C = bullet.GetComponent<Player_Projectile>();
        bullet_C.StartCoroutine(bullet_C.Shot(P_state.direction));
    }
}
