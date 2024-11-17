using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoDataBase", menuName = "Scriptable Objects/PlayerInfoDataBase")]

public class PlayerInfoDataBase : ScriptableObject
{
    public static PlayerInfoDataBase instance;

    public void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public List<PlayerDatas> playerDatas = new List<PlayerDatas>();
}

[Serializable]
public class PlayerDatas
{
    public int playerID;
    public int komaSets;
    public int currentKomaInKomaSets;
}
