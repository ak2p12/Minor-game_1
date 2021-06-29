using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Unit
{
    bool isMove;
    bool isJump;
    bool isAttack;
    bool isDodge;
    bool isGround;
    bool isWallCrash;
    bool isWallJump;
    bool isDoubleJump;
    bool isJumpUpDown;


    void Start()
    {
        base.SetUp();
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
            DodgeAction();
            JumpAction();
            AttackAction();
            SetAnimtion();
            yield return null;
        }
    }

    void MoveAction()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !isAttack && !isDodge && isGround ) //왼쪽 이동
        {
            Rigid.velocity = new Vector2(Vector2.left.x * (MoveSpeed), Rigid.velocity.y);
            Render.flipX = true;
            isMove = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !isAttack && !isDodge && isGround ) //오른쪽 이동
        {
            Rigid.velocity = new Vector2(Vector2.right.x * (MoveSpeed), Rigid.velocity.y);
            Render.flipX = false;
            isMove = true;
        }
        else
            isMove = false;


        if (Input.GetKeyDown(KeyCode.Space) && !isJump) //점프
        {
            Rigid.velocity = new Vector2(Rigid.velocity.x, JumpPower);
            Anim.SetTrigger("Jump");
            isJump = true;
        }

        if (!isGround && isWallCrash && (Rigid.velocity.y < 0) )
        {
            Anim.SetBool("WallCrash", isWallCrash);
            Rigid.velocity = new Vector2(Rigid.velocity.x, -2.0f);
        }

        Anim.SetFloat("velocity_y_float", Rigid.velocity.y);

        if (Rigid.velocity.y <= 0)
            Anim.SetInteger("velocity_y_int", (int)Rigid.velocity.y);

        
        //Debug.Log("점프력 : " + Rigid.velocity.y.ToString());

        if (isGround)
            Debug.Log("땅");
        else
            Debug.Log("땅 아님");

        if (isWallCrash)
            Debug.Log("벽");
        else
            Debug.Log("벽 아님");
    }
    void JumpAction()
    {

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
        if (Input.GetKeyDown(KeyCode.Z) && !isAttack) //공격
        {
            isAttack = true;
            int randomAttack = 1;//Random.Range(1, 5);
            Anim.SetTrigger("Attack_" + randomAttack.ToString());
            Debug.Log(randomAttack.ToString());

        }
    }
    void SetAnimtion()
    {
        Anim.SetBool("Run", isMove);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Land"))
        {
            isGround = true;
            isJump = false;
            isAttack = false;
            isWallCrash = false;
            Anim.SetBool("WallCrash", isWallCrash);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            isWallCrash = true;
        }
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
