using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Diego_Mod : Ability
{
    [SerializeField] private GameObject diego_Bullet_obj;
    IEnumerator DiegoMod_co;

    public override void UseAbility()
    {
        DiegoMod_co = DiegoAttack_co();
        StartCoroutine(DiegoMod_co);
    }

    private IEnumerator DiegoAttack_co()
    {
        PlayerManager.instance.UsedAb();
        P_state.actState = Unit_state.Default;
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("DiegoMod");
        animator.speed = anispeed * 1.5f;
        yield return new WaitForSeconds(0.45f / anispeed);
        CreateBullet();
        noise.m_AmplitudeGain =2;
        yield return new WaitForSeconds(0.16f / anispeed);
        CreateBullet();
        yield return new WaitForSeconds(0.16f / anispeed);
        CreateBullet();
        noise.m_AmplitudeGain =0;

        yield return new WaitForSeconds(0.3f / anispeed);

        animator.speed = 1;
        animator.SetTrigger("Idle");
        P_state.actState = Unit_state.Idle;

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.current_Count] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }

        yield return null;
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(diego_Bullet_obj,
            new Vector3(transform.position.x + P_state.direction.x, transform.position.y + 0.35f), Quaternion.identity);
        Player_Projectile bullet_C = bullet.GetComponent<Player_Projectile>();
        bullet_C.StartCoroutine(bullet_C.Shot(P_state.direction));
    }
}
