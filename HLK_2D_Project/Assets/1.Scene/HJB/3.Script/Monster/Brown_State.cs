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
        ability_Item = Ability_Item.Brown;
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                return;
            case Unit_state.Move:
                monsterMove.TotalMove();
                break;
            case Unit_state.Attack:
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                StartCoroutine(Stun_co());
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Die:
                break;
            default:
                break;
        }

        RayDetectionAttack();
        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();
        }
    }
    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            base.Die();
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
        }
    }


    #region//곰 던지기 로직(CO)
    private void RayDetectionAttack()
    {
        LayerMask PlayerMask = LayerMask.GetMask("Player");        
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 3f, transform.position.y - 1.5f), Vector2.right, 6f, PlayerMask);

        Debug.DrawRay(new Vector2(transform.position.x - 3f, transform.position.y - 1.5f), Vector2.right * 6f, Color.red);
        if (hit.collider != null)
        {   
            state = Unit_state.Attack;
            rigidbody.velocity = Vector2.zero;
            hit.transform.position = new Vector2(transform.position.x, transform.position.y + 2f);

            hit.transform.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigid);
            hit.transform.gameObject.TryGetComponent<Player_State>(out Player_State playerstate);
            playerstate.actState = Unit_state.Grab;
            
            StartCoroutine(Brown_Ab(otherRigid,playerstate));
        }
        else
        {
            return;
        }
    }
    IEnumerator Brown_Ab(Rigidbody2D otherRigid,Player_State playerState)
    {
        animator.SetTrigger("Attack");
        float random = Random.Range(0, 2).Equals(0) ? -1f : 1f;
        yield return new WaitForSeconds(0.5f);
        Vector2 randomvec = new Vector2(random, 1f).normalized;
        otherRigid.gravityScale = 4f;
        playerState.actState = Unit_state.Default;
        otherRigid.AddRelativeForce(randomvec * 20f, ForceMode2D.Impulse);
        state = Unit_state.Move;
        animator.SetTrigger("Default");
        yield return null;
    }
    #endregion
}
