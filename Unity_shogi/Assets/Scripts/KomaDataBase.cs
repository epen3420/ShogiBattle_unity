using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/KomaDataBase")]
public class KomaDataBase : ScriptableObject
{
    public List<KomaObjectDatas> komaObjectDatas;

    public List<KomaObjectDatas> KomaDatas()
    {
        return komaObjectDatas;
    }
}

[System.Serializable]
public class KomaObjectDatas
{
    public string name;
    public GameObject prefab;

    public float weight;
    public KomaType komaType;
}

public enum KomaType
{
    None = -1,
    Ou,
    Gyoku,
    Hisya,
    Kaku,
    Kin,
    Gin,
    Kei,
    Kyou,
    Hu,

    //成
    RyuOu,
    RyuMa,
    NariKin,
    NariKei,
    NariKyou,
    ToKin
}
