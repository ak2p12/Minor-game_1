using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    float time;
    bool test;

    BehaviorTree BT;    //AI 메인루프

    void Start()
    {
        base.SetUp();

        StartCoroutine(Update_Coroutine());
    }

    IEnumerator Update_Coroutine()
    {
        while (true)
        {
            Move();
            yield return null;
        }
    }

    void Move()
    {
        time += Time.deltaTime;

        if (time >= 1.5f)
        {
            time = 0.0f;
            test = !test;
        }

        if (!test)
        {
            Rigid.velocity = new Vector2(Vector2.left.x * (MoveSpeed), Rigid.velocity.y);
            Render.flipX = false;
        }
        else
        {
            Rigid.velocity = new Vector2(Vector2.right.x * (MoveSpeed), Rigid.velocity.y);
            Render.flipX = true;

        }
    }

    public override bool Hit(float _damage)
    {
        Debug.Log(gameObject.name.ToString());
        return true;
    }
}

public class Condition_IsDead : Condition 
{
    public override bool ChackCondition(Enemy _enemy)
    {
        if (_enemy.isDead)
            return true;

        return false;
    }

    public override bool Result(Enemy _enemy)
    {
        if (ChackCondition(_enemy))
        {
            if (childNode != null && childNode.Result(_enemy))
            {
                return true;
            }
        }
        return false;
    }

    public override void SetNode(Node _node)
    {
        childNode = _node;
    }
} //사망 판별

public class Action_Dead : ActionNode
{
    float originTime;
    float currentTime;
    public override void OnStart(Enemy _enemy)
    {
        isStart = true;
        //currentTime = _enemy.DestroyTime;
        //_enemy.animator.SetTrigger("Dead");
        //originTime = Time.time;
    }
    public override bool OnUpdate(Enemy _enemy)
    {
        ////이전 프레임시간 에서 현재프레임시간 까지 걸린 시간을 계산
        //float time = Time.time - originTime;

        ////걸린시간을 현재시간에 더한다
        //currentTime -= time;

        ////현재 프레임 시간을 예전 프레임 시간으로 대입
        //originTime = Time.time;
        //_enemy.spriteRenderer.color = new Color(1, 1, 1, currentTime);

        //if (currentTime <= 0.0f)
        //    return true;

        return false;
    }
    public override bool OnEnd(Enemy _enemy)
    {
        //_enemy.spriteRenderer.color = new Color(1, 1, 1, 0);
        //originTime = 0.0f;
        //currentTime = 0.0f;
        //isStart = false;
        return true;
    }
    public override bool Result(Enemy _enemy)
    {
        if (!isStart)
            OnStart(_enemy);

        if (OnUpdate(_enemy))
        {
            OnEnd(_enemy);
            return true;
        }

        return false;
    }
}