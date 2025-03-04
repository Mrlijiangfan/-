using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPool : MonoBehaviour
{
    public static CharacterPool characterPool;
    private Dictionary<string, Queue<GameObject>> poolDict;//对象池

    private void Awake()
    {
        if (characterPool == null) characterPool = this;
        else Destroy(gameObject);
        poolDict = new Dictionary<string, Queue<GameObject>>();
    }
    
    // 初始化对象池
    public void InitPool(GameObject prefab, int size) {
        string key = prefab.tag;
        if (!poolDict.ContainsKey(key)) {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < size; i++) {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            poolDict.Add(key, queue);
        }
    }

    // 获取对象
    public GameObject GetObject(GameObject prefab) 
    {
        string key = prefab.tag;
        if (poolDict.ContainsKey(key)) 
        {
            if (poolDict[key].Count > 0) 
            {
                GameObject obj = poolDict[key].Dequeue();
                return obj;
            } 
            //else 
            //{
            //     // 动态扩展池容量
            //     GameObject newObj = Instantiate(prefab);
            //     newObj.SetActive(false);
            //     return newObj;
            //}
        }
        return null;
    }

    // 回收对象
    public void ReturnObject(GameObject obj) {
        string key = obj.tag;
        if (poolDict.ContainsKey(key)) {
            obj.SetActive(false);
            poolDict[key].Enqueue(obj);
        }
    }

    //子弹发射
    public void Shoot(GameObject bullet, Vector3 position, Quaternion rotation)
    {
        GameObject obj = GetObject(bullet);
        if(obj == null) return;
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
    }
}
