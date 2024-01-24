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
    Dash,
    DashAttack,
    Wait,
    Gimmick= 12,
}
public enum Condition_state
{
    Default = 0,
    Groggy,
    Poison,
    Ignition,
    Weakness,//약점노출
    GroggyCool,
    PoisonCool,
    WeaknessCool,
}

public enum Ability_Item
{
    Empty = 0,
    Brown,
    Hope,
    Handrick,
    BabyBoss,
    LilWiz,
    Blankey,
    Orange,
    Tracy,
    Percy,
    Holly,
    Bird,
    Skeleton,
    Mr_Chomps,
    Diego,
    Gordon,
    Vessa,
    MoeScotty,
    Angie,
    Fairy,
    Armand,
    BigRed,
    Penguin,
    Warrior,
    
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
    public int HP
    {
        get
        {
            return _HP;
        }
        set 
        {
            _HP = value;

            if (_HP<=0)
            {
                _HP = 0;
            }
        }
    }
    public float Detection => _Detection;
    public float Range => _Range;
    public float AttackSpeed => _AttackSpeed;
    public int Strength => _Strength;
    public float MoveSpeed
    {
        get 
        {
            return _MoveSpeed;
        }
        set
        {
            _MoveSpeed = value;

            if (_MoveSpeed <= 0)
            {
                _MoveSpeed = 0;
            }
        }
    }
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



