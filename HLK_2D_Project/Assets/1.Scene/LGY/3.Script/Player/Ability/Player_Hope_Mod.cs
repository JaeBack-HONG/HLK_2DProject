using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hope_Mod : Ability
{
    [SerializeField] private GameObject player_Bullet;

    public override void UseAbility()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        P_state.isAttack = true;
        PlayerManager.instance.UsedAb();
        P_state.actState = Unit_state.Default;
        rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionX;
        animator.SetTrigger("HopeMod");
        yield return new WaitForSeconds(0.55f);
        CreateBullet();


        P_state.actState = Unit_state.Idle;
        animator.SetTrigger("Idle");
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        P_state.isAttack = false;

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.current_Count] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }

        yield return null;
    }
    private void CreateBullet()
    {
        GameObject bullet = Instantiate(player_Bullet,
            new Vector3(transform.position.x + P_state.direction.x, transform.position.y + 0.2f), Quaternion.identity);
        Player_Projectile bullet_C = bullet.GetComponent<Player_Projectile>();
        bullet_C.StartCoroutine(bullet_C.Shot(P_state.direction));

    }
}
