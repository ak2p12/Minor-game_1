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

    public void CreateObject(string _name, GameObject _object, int _createCount = 10)
    {
        if (ObjectOrigin.ContainsKey(_name) == false) //원본이 없다면 원본 보관
        {
            ObjectOrigin.Add(_name , _object);
        }

        if (ObjectPool.ContainsKey(_name) == false) //리스트가 없다면 새로 생성
        {
            List<GameObject> bulletList = new List<GameObject>();
            for (int i = 0; i < _createCount; ++i)
            {
                GameObject obj = GameObject.Instantiate(_object);
                obj.SetActive(true);
                obj.SetActive(false);
                bulletList.Add(obj);
            }

            ObjectPool.Add(_name, bulletList);
        }
        else //리스트는 존재하지만 객체가 없다면
        {
            if (ObjectPool[_name].Count >=_createCount)
            {
                return;
            }
            else
            {
                int count = _createCount - ObjectPool[_name].Count;

                for (int i = 0; i < count; ++i)
                {
                    GameObject obj = GameObject.Instantiate(_object);
                    obj.SetActive(true);
                    obj.SetActive(false);
                    ObjectPool[_name].Add(obj);
                }
            }
            
        }
    }

    public bool GetObjectPool(string _name , List<GameObject> _inPoolList) //원하는 오브젝트를 반환 한다
    {

        if (ObjectPool.ContainsKey(_name)) //원하는 오브젝트가 있다면
        {
            List<GameObject> bulletList_1; //가지고있는 풀 리스트
            ObjectPool.TryGetValue(_name , out bulletList_1);

            if (bulletList_1.Count >= 5) //가지고있는 풀 리스트가 5개 이상 있을경우
            {
                for (int i = 0; i < 5; ++i)
                {
                    _inPoolList.Add(bulletList_1[i]); //객체를 뺀다
                    
                }
                ObjectPool[_name].RemoveRange(0, 5); //빼고 난뒤 리스트에서 제거
                return true;
            }
            else
            {
                int count = 5 - bulletList_1.Count;

                for (int i = 0; i < bulletList_1.Count; ++i)
                {
                    _inPoolList.Add(bulletList_1[i]);
                }

                ObjectPool[_name].RemoveRange(0, bulletList_1.Count);

                CreateObject(_name, GetObjectOrigin(_name), count);

                for (int i = 0; i < count; ++i)
                {
                    _inPoolList.Add(ObjectPool[_name][i]);
                }

                ObjectPool[_name].RemoveRange(0, count);
                return true;
            }
        }
        else
        {
            CreateObject(_name , GetObjectOrigin(_name));

            List<GameObject> bulletList_1; //가지고있는 풀 리스트
            ObjectPool.TryGetValue(_name, out bulletList_1);

            if (bulletList_1.Count >= 5) //가지고있는 풀 리스트가 5개 이상 있을경우
            {
                for (int i = 0; i < 5; ++i)
                {
                    _inPoolList.Add(bulletList_1[i]); //객체를 뺀다

                }
                ObjectPool[_name].RemoveRange(0, 5); //빼고 난뒤 리스트에서 제거
                return true;
            }
            else
            {
                int count = 5 - bulletList_1.Count;

                for (int i = 0; i < bulletList_1.Count; ++i)
                {
                    _inPoolList.Add(bulletList_1[i]);
                }

                ObjectPool[_name].RemoveRange(0, bulletList_1.Count);

                CreateObject(_name, GetObjectOrigin(_name), count);

                for (int i = 0; i < count; ++i)
                {
                    _inPoolList.Add(bulletList_1[i]);
                }

                ObjectPool[_name].RemoveRange(0, count);
                return true;
            }
        }

    }
    public void ReturnObjectPool(string _name , List<GameObject> _poolList)
    {
        if (ObjectPool.ContainsKey(_name)) //원하는 오브젝트가 있다면
        {
            for (int i = 0; i < _poolList.Count; ++ i)
            {
                ObjectPool[_name].Add(_poolList[i]);
            }
            _poolList.Clear();
        }
        else
        {
            List<GameObject> bulletList = new List<GameObject>();
            ObjectPool.Add(_name, bulletList);

            for (int i = 0; i < _poolList.Count; ++i)
            {
                ObjectPool[_name].Add(_poolList[i]);
            }
            _poolList.Clear();
        }
    }
    public bool CheckObject(string _name) //오브젝트가 있는지 없는지 판단
    {
        return ObjectPool.ContainsKey(_name);
    }
    private GameObject GetObjectOrigin(string _name)
    {
        GameObject obj;
        bool check = ObjectOrigin.TryGetValue(_name, out obj);

        if (check)
            return obj;

        return null;

    }

    

}
