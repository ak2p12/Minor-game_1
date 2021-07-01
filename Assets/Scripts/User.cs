﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Unit
{
    public JUMPMODE JumpMode;
    public USERCALSS UserClass;
    bool isMove;
    bool isJump;
    bool isAttack;
    bool isDodge;
    bool isGround;
    bool isWallCrash;
    bool isWallRide;
    bool isWallJump;
    bool isDoubleJump;


    void Start()
    {
        base.SetUp();
        JumpMode = JUMPMODE.NormalJump;
        UserClass = USERCALSS.Archer;
        StartCoroutine(Update_Coroutine());
    }
    void Update()
    {

    }

    IEnumerator Update_Coroutine()
    {
        while (!isDead)
        {
            MoveAction();
            JumpAction();
            DodgeAction();
            AttackAction();
            yield return null;
        }
    }

    void MoveAction()
    {
        //움직이는동안
        //공격하는중에 움직임 x
        //구르는중에 움직임 x
        //점프중에 움직임 x
        //벽점프중에 움직임 x
        if (Input.GetKey(KeyCode.LeftArrow) && !isAttack && !isDodge && !isJump && !isWallJump) //왼쪽 이동
        {
            Rigid.velocity = new Vector2(Vector2.left.x * (MoveSpeed), Rigid.velocity.y);
            Render.flipX = true;
            isMove = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !isAttack && !isDodge && !isJump && !isWallJump) //오른쪽 이동
        {
            Rigid.velocity = new Vector2(Vector2.right.x * (MoveSpeed), Rigid.velocity.y);
            Render.flipX = false;
            isMove = true;
        }
        else
            isMove = false;

        Anim.SetBool("Run", isMove);
    }
    void JumpAction()
    {
        switch (JumpMode)
        {
            case JUMPMODE.NormalJump:
                {
                    //점프 중 점프 x
                    //구르기 중 점프 x
                    //공격 중 점프x
                    if (Input.GetKeyDown(KeyCode.Space) && !isJump && !isDodge && !isAttack) //점프
                    {
                        Rigid.velocity = new Vector2(Rigid.velocity.x, JumpPower);
                        Anim.SetTrigger("Jump");
                        isJump = true;
                    }

                    if (!isGround && isWallCrash && !isWallJump) //벽타기
                    {
                        Anim.SetBool("WallCrash", isWallCrash);
                        Rigid.velocity = new Vector2(Rigid.velocity.x, -2.0f);
                        isWallRide = true;
                    }

                    if (Input.GetKeyDown(KeyCode.Space) && isWallRide && !isWallJump) //벽타기중 점프
                    {
                        if (Render.flipX == true) //왼쪽 벽에서 점프
                        {
                            Rigid.velocity = new Vector2(Vector2.right.x * (MoveSpeed * 1.5f), JumpPower);
                            Render.flipX = false;
                        }
                        else if (Render.flipX == false) //오른쪽 벽에서 점프
                        {
                            Rigid.velocity = new Vector2(Vector2.left.x * (MoveSpeed * 1.5f), JumpPower);
                            Render.flipX = true;
                        }

                        Anim.SetTrigger("Jump");
                        isWallJump = true;
                        isJump = true;
                    }

                }
                break;
            case JUMPMODE.DoubleJump:
                break;
        }
          
        Anim.SetFloat("velocity_y_float", Rigid.velocity.y);

        if (Rigid.velocity.y <= 0)
            Anim.SetInteger("velocity_y_int", (int)Rigid.velocity.y);

        Debug.Log(Rigid.velocity.y.ToString());
    }
    void DodgeAction()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isJump && !isDodge) //구르기
        {
            isDodge = true;
            Anim.SetTrigger("Roll");
        }

        if (isDodge)
        {
            if (Render.flipX == true) //왼쪽 구르기
                Rigid.velocity = new Vector2(Vector2.left.x * (MoveSpeed * 1.5f), Rigid.velocity.y);
            else if (Render.flipX == false) //오른쪽 구르기
                Rigid.velocity = new Vector2(Vector2.right.x * (MoveSpeed * 1.5f), Rigid.velocity.y);
        }
    }
    void AttackAction()
    {
        switch (UserClass)
        {
            case USERCALSS.Defender:
                break;
            case USERCALSS.Archer:
                {
                    
                    if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.UpArrow) && !isAttack) //위방향 공격
                    {
                        isAttack = true;
                        Anim.SetTrigger("Attack_1");
                    }
                    else if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.DownArrow) && !isAttack) //아래방향 공격
                    {
                        isAttack = true;
                        Anim.SetTrigger("Attack_3");
                    }
                    else if (Input.GetKeyDown(KeyCode.Z) && !isAttack) //정면방향 공격
                    {
                        isAttack = true;
                        Anim.SetTrigger("Attack_2");
                    }
                }
                break;
            case USERCALSS.Mage:
                break;
            case USERCALSS.Spearman:
                break;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Land"))
        {
            isGround = true;
            isJump = false;
            isAttack = false;
            isWallCrash = false;
            isWallRide = false;
            isWallJump = false;
            Anim.SetBool("WallCrash", isWallCrash);
        }
        if (collision.gameObject.tag.Equals("Wall"))
        {
            isWallCrash = true;
            isWallJump = false;
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.tag.Equals("Wall"))
        //{
        //    isWallCrash = true;
        //}
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Land"))
        {
            isGround = false;
        }
        if (collision.gameObject.tag.Equals("Wall"))
        {
            isWallCrash = false;
            isWallRide = false;
            Anim.SetBool("WallCrash", isWallCrash);
        }
        //else if (collision.gameObject.tag.Equals("Wall"))
        //{
        //    isWallCrash = false;
        //    Anim.SetBool("WallCrash", isWallCrash);
        //    Debug.Log("벽 아님");
        //}
    }

    void AnimationJump_Start()
    {
        isAttack = false;
    }
    void Animation_Landing()
    {
        //isJump = false;
        //isAttack = false;
    }
    void AnimationAttack_Start()
    {
        isAttack = true;
    }
    void AnimationAttack_End()
    {
        isAttack = false;
    }
    void AnimationIdle()
    {
        //isAttack = false;
    }
    void AnimationDodge_Start()
    {
        //isAttack = false;
    }
    void AnimationDodge_End()
    {
        isDodge = false;
        isAttack = false;
    }
}
