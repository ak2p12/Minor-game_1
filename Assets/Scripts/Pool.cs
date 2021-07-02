using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject PoolObject; //생성할 객체
    [HideInInspector] public float ActiveCount; //현재 활성화 된 객체 수
    public float CreateCount; //초기 생성 개수
    public float MaxCreateCount; //생성객체 개수 초과 시 생설 될 객체 수
    //[HideInInspector] public List<GameObject> PoolList = new List<GameObject>; //객체 모음

    // Start is called before the first frame update
    void Start()
    {

        GameObject pool = GameObject.Instantiate(PoolObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
