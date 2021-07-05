using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    float currentLifeTime;

    private void OnEnable()
    {
        StartCoroutine(Update_Coroutine());
    }
    private void OnDisable()
    {
        currentLifeTime = 0.0f;
        StopAllCoroutines();
    }

    IEnumerator Update_Coroutine()
    {
        while(true)
        {
            if (currentLifeTime >= LifeTime)
            {
                this.gameObject.SetActive(false);
            }

            currentLifeTime += Time.deltaTime;
            transform.Translate(Direction * Speed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(Damage);
            this.gameObject.SetActive(false);
        }
    }
}
