using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    
}
public class Monster_Projectile : MonoBehaviour
{
    [Range(1,5)]
    public int damage = 2;

    
}
