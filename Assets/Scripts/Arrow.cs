using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    void Start()
    {
        StartCoroutine(Update_Coroutine());
    }

    IEnumerator Update_Coroutine()
    {
        while(true)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(10.0f);
        }
    }
}
