using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolrdObject : MonoBehaviour, IDamage
{
    [HideInInspector] public Rigidbody2D Rigid;
    [HideInInspector] public Animator Anim;
    [HideInInspector] public SpriteRenderer Render;

    protected void SetUp()
    {
        Anim = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody2D>();
        Render = GetComponent<SpriteRenderer>();
    }

    public virtual bool Hit(float _damege)
    {
        Debug.Log(gameObject.name + " Hit()");
        return false;
    }
}
