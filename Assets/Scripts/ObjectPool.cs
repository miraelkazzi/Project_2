using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : Component, IPoolable
{
    readonly T prefab;
    readonly Transform parent;
    readonly Queue<T> available = new Queue<T>();
    readonly HashSet<T> active = new HashSet<T>();

    public int CountActive => active.Count;
    public int CountInactive => available.Count;

    public ObjectPool(T prefab, Transform parent, int prewarmCount)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < prewarmCount; i++)
        {
            T instance = CreateInstance();
            instance.gameObject.SetActive(false);
            available.Enqueue(instance);
        }
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        T instance = available.Count > 0 ? available.Dequeue() : CreateInstance();
        instance.transform.SetPositionAndRotation(position, rotation);
        instance.gameObject.SetActive(true);
        active.Add(instance);
        instance.OnGetFromPool();
        return instance;
    }

    public void Return(T instance)
    {
        if (!active.Remove(instance)) return;

        instance.OnReturnFromPool();
        instance.gameObject.SetActive(false);
        available.Enqueue(instance);
    }

    public void ReturnAll()
    {
        foreach (T instance in active)
        {
            instance.OnReturnFromPool();
            instance.gameObject.SetActive(false);
            available.Enqueue(instance);
        }

        active.Clear();
    }

    T CreateInstance()
    {
        T instance = Object.Instantiate(prefab, parent);
        return instance;
    }
}