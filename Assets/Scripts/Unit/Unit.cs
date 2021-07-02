using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : WolrdObject
{
    public float HP;
    public float MP;
    public float SP;
    public float MoveSpeed;
    public float JumpPower;
    public float AttackDamage;
    public Vector2 AttackPosition;

    public bool isDead;

    public override bool Hit(float _damage)
    {
        return true;
    }
}
