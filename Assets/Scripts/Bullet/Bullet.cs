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

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
