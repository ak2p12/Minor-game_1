using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Unit
{
    bool move;

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
            yield return null;
        }
    }

    void MoveAction()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) //왼 쪽
        {
            Rigid.velocity = new Vector2(Vector2.left.x * (MoveSpeed), Rigid.velocity.y);
            Render.flipX = true;
            move = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) //오른 쪽
        {
            Rigid.velocity = new Vector2(Vector2.right.x * (MoveSpeed), Rigid.velocity.y);
            Render.flipX = false;
            move = true;
        }
        else
        {
            move = false;
        }
            

        if (Input.GetKeyDown(KeyCode.UpArrow)) //위
        {
            Rigid.velocity = new Vector2(Rigid.velocity.x, JumpPower);
            Anim.SetTrigger("Jump");
        }

        Anim.SetFloat("velocity_Y", Rigid.velocity.y);
        Anim.SetInteger("velocity_Y_Int", (int)Rigid.velocity.y);
        Anim.SetBool("Walk", move);
    }
    void AttackAction()
    {
        if (Input.GetKey(KeyCode.Z)) //공격
        {

        }
    }
}
