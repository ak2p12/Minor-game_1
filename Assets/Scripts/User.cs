using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Unit
{
    bool isMove;
    bool isJump;
    bool isAttack;
    bool isDodge;

    void Start()
    {
        Anim = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody2D>();
        Render = GetComponent<SpriteRenderer>();

        StartCoroutine(Update_Coroutine());
    }
    void Update()
    {

    }

    IEnumerator Update_Coroutine()
    {
        while (true)
        {
            MoveAction();
            AttackAction();
            yield return null;
        }
    }

    void MoveAction()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !isAttack && !isDodge) //왼 쪽
        {
            Rigid.velocity = new Vector2(Vector2.left.x * (MoveSpeed), Rigid.velocity.y);
            Render.flipX = true;
            isMove = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !isAttack && !isDodge) //오른 쪽
        {
            Rigid.velocity = new Vector2(Vector2.right.x * (MoveSpeed), Rigid.velocity.y);
            Render.flipX = false;
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isJump && !isDodge) //구르기
        {
            isDodge = true;
            Anim.SetTrigger("Dodge");
        }

        if (isDodge)
        {
            if (Render.flipX == true) //왼쪽
            {
                Rigid.velocity = new Vector2(Vector2.left.x * (MoveSpeed * 1.5f), Rigid.velocity.y);
            }
            else if (Render.flipX == false) //오른쪽
            {
                Rigid.velocity = new Vector2(Vector2.right.x * (MoveSpeed * 1.5f), Rigid.velocity.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJump) //점프
        {
            Rigid.velocity = new Vector2(Rigid.velocity.x, JumpPower);
            Anim.SetTrigger("Jump");
            isJump = true;
        }

        Anim.SetFloat("velocity_Y", Rigid.velocity.y);
        Anim.SetInteger("velocity_Y_Int", (int)Rigid.velocity.y);
        Anim.SetBool("Walk", isMove);
    }
    void AttackAction()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isAttack) //공격
        {
            isAttack = true;
            int randomAttack = Random.Range(1, 5);
            Anim.SetTrigger("Attack_" + randomAttack.ToString());
            Debug.Log(randomAttack.ToString());

        }
    }
    void AnimationJump_Start()
    {
        isAttack = false;
    }
    void AnimationJump_End()
    {
        isJump = false;
        isAttack = false;
    }
    //void AnimationAttack_Start()
    //{
    //    isAttack = true;
    //}
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
