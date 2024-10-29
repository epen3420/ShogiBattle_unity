using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T prefab;
    private readonly Queue<T> pool = new Queue<T>();
    private readonly Transform parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        // 初期サイズ分のオブジェクトをプールに準備する
        for (int i = 0; i < initialSize; i++)
        {
            T obj = CreateNewObject();
            pool.Enqueue(obj);
        }
    }

    // オブジェクトを取得する
    public T Get()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            // プールが空の場合、新たに生成する
            return CreateNewObject();
        }
    }

    // オブジェクトをプールに戻す
    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    // プール内のオブジェクトを全て破棄する
    public void Clear()
    {
        while (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            Object.Destroy(obj.gameObject);
        }
    }

    // 新しいオブジェクトを生成する
    private T CreateNewObject()
    {
        T newObj = Object.Instantiate(prefab, parent);
        newObj.gameObject.SetActive(false);
        return newObj;
    }
}
