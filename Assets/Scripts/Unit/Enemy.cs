using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public float AttackRadiue;  //공격 범위

    public override bool Hit(float _damage)
    {
        return true;
    }

}
