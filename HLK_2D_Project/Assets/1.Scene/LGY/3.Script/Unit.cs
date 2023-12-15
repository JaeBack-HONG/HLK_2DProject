using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data
{
    public int _HP;            // 체력
    public float _Detection;   // 탐지범위
    public float _Range;       // 공격사거리
    public float _AttackSpeed; // 공격속도
    public int _Strength;      // 공격력
    public float _MoveSpeed;   // 이동속도
    public float _JumpForce;   // 점프력
}

public abstract class Unit : MonoBehaviour
{
    public Data data;

    public abstract void Die();
}
