using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KomaDataBase", menuName = "Scriptable Objects/KomaDataBase")]
public class KomaDataBase : ScriptableObject
{
    public List<KomaDatas> komaDatasList = new List<KomaDatas>();
    public List<KomaSets> komaSetsList = new List<KomaSets>();
}

[Serializable]
public class KomaDatas
{
    public KomaType name;
    public float weight;
}

[Serializable]
public class KomaSets
{
    public List<KomaType> komaType = new List<KomaType>();
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
    Hu
}
