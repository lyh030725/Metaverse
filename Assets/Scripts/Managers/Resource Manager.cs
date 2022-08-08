using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int idx = name.LastIndexOf('/');
            if (idx >= 0)
                name = name.Substring(idx + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if(go != null)
                return go as T;
        }
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        //1. original 이미 들고 있으면 바로 사용
        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;

        }

        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        //2. 혹시 풀링된 애가 있을까 ?
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        //int index = go.name.IndexOf("(Clone)");

        //if(index > 0)
        //{
        //    go.name = go.name.Substring(0, index);
        //}



        return go;
    }

    public void Destroy(GameObject go)
    {
        if(go == null)
        {
            return;
        }

        Poolable poolable = go.GetComponent<Poolable>();

        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        // 만약에 풀링이 필요하다 -> 풀링 매니저에게 부탁
        Object.Destroy(go); 
    }

}
