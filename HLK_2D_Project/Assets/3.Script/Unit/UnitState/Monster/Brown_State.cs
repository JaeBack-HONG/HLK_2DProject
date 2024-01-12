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
        (name: "Brown", hp: healthSet, detection: 3, range: 0.5f,
            attackSpeed: 2, strength: damageSet, moveSpeed: speedSet, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.Brown;
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        if (!state.Equals(Unit_state.Die))
        {
            Monster_HealthCheck();
        }
        switch (state)
        {
            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                return;
            case Unit_state.Move:
                monsterMove.TotalMove();
                RayDetectionAttack();
                break;
            case Unit_state.Attack:
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:                
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Die:
                break;
            default:
                break;
        }        
    }
    private void ChangeState(Unit_state newState)
    {
        if (state.Equals(newState))
        {
            return;
        }
        state = newState;

        switch (state)
        {

            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                break;
            case Unit_state.Attack:
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Dash:
                break;
            case Unit_state.Die:
                break;
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
            hit.transform.position = new Vector2(transform.position.x-0.1f, transform.position.y + 1.5f);

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

    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            ChangeState(Unit_state.Die);
            base.Die();
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
        }
    }
}
