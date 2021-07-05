using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : WolrdObject
{
    public float Speed;         //날아가는속력
    public float LifeTime;      //생존 시간
    [HideInInspector] public float Damage;        //피해
    [HideInInspector] public Vector2 Direction;     //날아가는 방향
    public float Radius;        //범위

    public virtual void SetUp(Vector2 _startPos, Vector2 _direction, float _speed, float _damage, float _range , bool _flipX)
    {
        transform.position = new Vector3(_startPos.x, _startPos.y);
        Direction = _direction;
        Speed = _speed;
        Damage = _damage;
        LifeTime = _range;
        Render.flipX = _flipX;
        this.gameObject.SetActive(true);
    }
}
