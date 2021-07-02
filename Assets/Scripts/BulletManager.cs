using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private static BulletManager instance = null;

    private Dictionary<string, List<GameObject>> ObjectPool = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, GameObject> ObjectOrigin = new Dictionary<string, GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public static BulletManager Instance()
    {
        if (instance == null)
            return null;
        return instance;
    }

    public void CreateObject(string _name, GameObject _object, int _createCount = 5)
    {
        if (ObjectOrigin.ContainsKey(_name) == false)
        {
            ObjectOrigin.Add(_name , _object);
        }

        List<GameObject> bulletList = new List<GameObject>();
        for (int i = 0; i < _createCount; ++i)
        {
            GameObject obj = GameObject.Instantiate(_object);
            obj.SetActive(false);
            bulletList.Add(obj);
        }

        ObjectPool.Add(_name, bulletList);
    }

    public List<GameObject> GetObjectPool(string _name) //원하는 오브젝트를 반환 한다
    {

        if (ObjectPool.ContainsKey(_name)) //원하는 오브젝트가 있다면
        {
            List<GameObject> bulletList_1;
            List<GameObject> bulletList_2 = new List<GameObject>();
            ObjectPool.TryGetValue(_name , out bulletList_1);

            if (bulletList_1.Count >= 5)
            {
                for (int i = 0; i < 5; ++i)
                {
                    bulletList_2.Add(bulletList_1[i]);
                }
                
            }
            else
            {
                int count = 5 - bulletList_1.Count;

                for (int i = 0; i < bulletList_1.Count; ++i)
                {
                    bulletList_2.Add(bulletList_1[i]);
                }
                //for (int i = 0; i < count; ++i)
                //{
                //    GameObject obj = GameObject.Instantiate(_object);
                //    obj.SetActive(false);
                //    bulletList.Add(obj);
                //}
                //bulletList_2();
            }
            
            return bulletList_2;

        }

        return null;
    }

    public bool CheckObject(string _name) //오브젝트가 있는지 없는지 판단
    {
        return ObjectPool.ContainsKey(_name);
    }
    private GameObject GetObjectOrigin(string _name)
    {
        GameObject obj;
        ObjectOrigin.TryGetValue(_name, out obj);
        return obj;
    }

    

}
