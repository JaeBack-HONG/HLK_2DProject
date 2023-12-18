using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster_State : MonoBehaviour
{
    public UnitData data;
    public MonsterMove monsterMove;

    public virtual void MonsterDataSetting()
    {
        monsterMove = GetComponent<MonsterMove>();
        monsterMove.MoveSpeed = data.MoveSpeed;
    }
}
