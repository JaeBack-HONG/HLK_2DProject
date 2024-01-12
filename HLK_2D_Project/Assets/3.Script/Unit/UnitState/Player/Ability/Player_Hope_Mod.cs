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
        DefaulutSet("HopeMod");

        PlayerManager.instance.UsedAb();


        yield return new WaitForSeconds(0.6f / anispeed);
        CreateBullet();
        noise.m_AmplitudeGain = 2;
        yield return new WaitForSeconds(0.05f / anispeed);
        noise.m_AmplitudeGain = 0;
        EndSet();

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.Select_Idx] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }

        yield return null;
    }
    private void CreateBullet()
    {
        GameObject bullet = Instantiate(player_Bullet,
            new Vector3(transform.position.x + P_state.direction.x, transform.position.y + 0.4f), Quaternion.identity);
        Player_Projectile bullet_C = bullet.GetComponent<Player_Projectile>();
        bullet_C.StartCoroutine(bullet_C.Shot(P_state.direction));

    }
}
