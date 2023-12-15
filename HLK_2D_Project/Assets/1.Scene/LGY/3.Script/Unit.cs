using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data
{
    public int _HP;            // ü��
    public float _Detection;   // Ž������
    public float _Range;       // ���ݻ�Ÿ�
    public float _AttackSpeed; // ���ݼӵ�
    public int _Strength;      // ���ݷ�
    public float _MoveSpeed;   // �̵��ӵ�
    public float _JumpForce;   // ������
}

public abstract class Unit : MonoBehaviour
{
    public Data data;

    public abstract void Die();
}
