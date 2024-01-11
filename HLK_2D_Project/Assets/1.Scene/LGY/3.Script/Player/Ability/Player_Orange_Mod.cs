using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Orange_Mod : Ability
{
    [Header("구르기 속도")]
    [SerializeField] private float rollingSpeed = 30f;
    private bool ishit;
    [SerializeField] private int attackDmg = 3;

    public override void UseAbility()
    {
        StartCoroutine(Orange_Attack_Co());
    }

    private IEnumerator Orange_Attack_Co()
    {
        DefaulutSet("OrangeMod");
        gameObject.layer = (int)Layer_Index.Hit;

        ishit = false;

        float Gauge = 0f;
        while (Gauge <= 3f && Input.GetKey(KeyCode.Z))
        {
            Gauge += Time.fixedDeltaTime * 2f;
            gaugeUI.fillAmount = Gauge / 3f;
            yield return new WaitForFixedUpdate();
        }

        while (Input.GetKey(KeyCode.Z))
        {
            yield return new WaitForFixedUpdate();
        }

        PlayerManager.instance.UsedAb();

        while (Gauge >= 0 && !ishit)
        {
            Gauge -= Time.fixedDeltaTime;
            gaugeUI.fillAmount = Gauge / 3f;
            rigidbody.velocity = new Vector2(P_state.direction.x * rollingSpeed, rigidbody.velocity.y);
            StartCoroutine(OrangeRollingStopCheck());
            yield return new WaitForFixedUpdate();
        }

        while (!P_state.JumState.Equals(Jump_State.Falling) && ishit)
        {
            yield return new WaitForFixedUpdate();
        }

        gameObject.layer = (int)Layer_Index.Player;
        EndSet();
        yield return null;

    }

    private IEnumerator OrangeRollingStopCheck()
    {
        LayerMask StopCheckMask = LayerMask.GetMask("Enemy", "Ground");

        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - 0.2f), P_state.direction, 1f, StopCheckMask);

        if (hit.collider != null)
        {
            ishit = true;
            P_state.isAttack = false;
            if (hit.collider.gameObject.layer.Equals((int)Layer_Index.Enemy))
            {
                Monster_State monstate = hit.collider.gameObject.GetComponent<Monster_State>();
                monstate.rigidbody.AddForce(P_state.direction * 13f, ForceMode2D.Impulse);
                monstate.Health -= attackDmg;
                monstate.UnitHit.Hit((int)Layer_Index.Enemy, transform.position);
            }
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce((-P_state.direction + Vector2.up * 2f).normalized * 20f, ForceMode2D.Impulse);

            gaugeUI.fillAmount = 0;
            noise.m_AmplitudeGain = 10;
            yield return new WaitForSeconds(0.2f / anispeed);
            noise.m_AmplitudeGain = 0;
            yield return null;

        }


    }


}
