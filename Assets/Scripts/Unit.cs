using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float HP;
    public float MP;
    public float SP;
    public float MoveSpeed;
    public float JumpPower;
    public float AttackDamage;

    [HideInInspector] public Rigidbody2D Rigid;
    [HideInInspector] public Animator Anim;
    [HideInInspector] public SpriteRenderer Render;

    //void Start()
    //{
        
    //}

    //void Update()
    //{
        
    //}
}
