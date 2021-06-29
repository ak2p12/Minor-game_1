using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : Unit
{


    void Start()
    {
        base.SetUp();
    }

    IEnumerator Update_Coroutine()
    {
        while (true)
        {
            yield return null;
        }
    }
}
