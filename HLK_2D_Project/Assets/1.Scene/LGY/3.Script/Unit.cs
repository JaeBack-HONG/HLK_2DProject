using System.Collections;
using System.Collections.Generic;

public interface IUnit
{
    void Die();
    void Attack(Player_State other);
}
public enum Unit_state
{
    Default=0,
    Idle,
    Move,
    Attack,
    Grab,
    Hit,
    Jump,
    Die,
    Stun,
    Dash = 9
}
public enum Condition_state
{
    Default = 0,
    Groggy,
    Poison,
    Weakness,//약점노출
    GroggyCool,
    PoisonCool,
    WeaknessCool,
}

public enum Ability_Item
{
    Brown,
    Hope,
    Handrick,
    BabyBoss,
    LilWiz,
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
    

    //캡슐화된 프로퍼티들
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



