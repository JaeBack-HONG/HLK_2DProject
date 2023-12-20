using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brown_State : Monster_State
{

    private void Start()
    {
        MonsterDataSetting();
    }


    public override void MonsterDataSetting()
    {
        data = new UnitData
        (name: "Brown", hp: 5, detection: 3, range: 0.5f,
            attackSpeed: 2, strength: 2, moveSpeed: 1, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case Unit_state.Idle:
                return;
            case Unit_state.Move:
                monsterMove.TotalMove();

                break;
            case Unit_state.Attack:

                return;
            case Unit_state.Hit:
                break;
            default:
                break;
        }
        Monster_HealthCheck();
        RayDetectionAttack();
    }
    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            base.Die();
        }
    }

    private void RayDetectionAttack()
    {
        LayerMask PlayerMask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 2f, transform.position.y - 0.75f), Vector2.right, 4f, PlayerMask);

        Debug.DrawRay(new Vector2(transform.position.x - 2f, transform.position.y - 0.75f), Vector2.right * 4f, Color.red);
        if (hit.collider != null)
        {   
            state = Unit_state.Attack;
            rigidbody.velocity = Vector2.zero;
            hit.transform.position = new Vector2(transform.position.x, transform.position.y + 1f);

            hit.transform.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigid);
            hit.transform.gameObject.TryGetComponent<Player_State>(out Player_State playerstate);
            playerstate.actState = Unit_state.Grab;
            
            StartCoroutine(Brown_Ab(otherRigid));
        }
        else
        {
            return;
        }
    }
    IEnumerator Brown_Ab(Rigidbody2D otherRigid)
    {
        float random = Random.Range(0, 2).Equals(0) ? -1f : 1f;
        yield return new WaitForSeconds(2f);
        Vector2 randomvec = new Vector2(random, 1f).normalized;
        otherRigid.gravityScale = 4f;
        otherRigid.AddRelativeForce(randomvec * 20f, ForceMode2D.Impulse);
        state = Unit_state.Move;
        yield return null;
    }
}
