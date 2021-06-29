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

    public bool isDead;

    [HideInInspector] public Rigidbody2D Rigid;
    [HideInInspector] public Animator Anim;
    [HideInInspector] public SpriteRenderer Render;

    void Start()
    {
        //Anim = GetComponent<Animator>();
        //Rigid = GetComponent<Rigidbody2D>();
        //Render = GetComponent<SpriteRenderer>();
    }

    protected void SetUp()
    {
        Anim = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody2D>();
        Render = GetComponent<SpriteRenderer>();
    }

    //void Update()
    //{
        
    //}
}
