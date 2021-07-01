using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}


    public override bool Hit(float _damege)
    {
        Debug.Log(gameObject.name + " Hit()");
        return false;
    }
}
