using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster_State : MonoBehaviour,IUnit
{
    
    public UnitData data;
    public MonsterMove monsterMove;

    public virtual void MonsterDataSetting()
    {
        monsterMove = GetComponent<MonsterMove>();
        monsterMove.MoveSpeed = data.MoveSpeed;
        monsterMove.Detection = data.Detection;
        
        
    }
    

    public abstract void Monster_HealthCheck();

    public virtual void Die()
    {
        Destroy(gameObject, 1f);
    }
}
