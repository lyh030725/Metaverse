using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILoder<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager 
{
   public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

   public void Init()
    {

        StatDict = Managers.Data.LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    Loder LoadJson<Loder,Key,Value> (string path) where Loder : ILoder<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loder>(textAsset.text);
    }
}