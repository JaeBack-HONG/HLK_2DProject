using System.Collections;
using System.Collections.Generic;

public interface IUnit
{
    void Die();
}

public class UnitData
{
    private string _Name;
    private int _HP;
    private float _Detection;
    private float _Range;
    private float _AttackSpeed;
    private int _Strength;
    private float _MoveSpeed;
    private float _JumpForce;

    //Ä¸½¶È­µÈ ÇÁ·ÎÆÛÆ¼µé
    public string Name => _Name;
    public int HP => _HP;
    public float Detection => _Detection;
    public float Range => _Range;
    public float AttackSpeed => _AttackSpeed;
    public int Strength => _Strength;
    public float MoveSpeed => _MoveSpeed;
    public float JumpForce => _JumpForce;

    public UnitData(string name, int hp, float detection, float range, float attackSpeed, int strength, float moveSpeed, float jumpForce)
    {
        _Name = name;
        _HP = hp;
        _Detection = detection;
        _Range = range;
        _AttackSpeed = attackSpeed;
        _Strength = strength;
        _MoveSpeed = moveSpeed;
        _JumpForce = jumpForce;
    }    
}



