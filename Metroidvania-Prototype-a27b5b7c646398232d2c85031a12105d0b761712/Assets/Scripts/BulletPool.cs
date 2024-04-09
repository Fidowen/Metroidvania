using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject[] bullets;
    public int poolSizePrefab = 5;
    private List<GameObject>[] objPool;

    private void Start()
    {
        initiaLizeObjPool();
    }
    private void initiaLizeObjPool()//初始化
    {
        objPool = new List<GameObject>[bullets.Length];
        for (int i = 0; i < bullets.Length; i++)
        {
            objPool[i] = new List<GameObject>();
            for (int j = 0; j < poolSizePrefab; j++)
            {
                GameObject newObj = Instantiate(bullets[i]);
                newObj.SetActive(false);
                objPool[i].Add(newObj);
            }
        }
    }
    public GameObject GetBulletObject(int index)// 从对象池中获取指定类型的对象
    {
        foreach (GameObject obj in objPool[index]) 
        {
            if (!obj.activeInHierarchy) {  return obj; }
            
        }
        GameObject  newObj = Instantiate(bullets[index]);
        newObj.SetActive(false);
        objPool[index].Add(newObj);
        return newObj;

    }


}
