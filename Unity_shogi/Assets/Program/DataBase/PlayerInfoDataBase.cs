using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoDataBase", menuName = "Scriptable Objects/PlayerInfoDataBase")]
public class PlayerInfoDataBase : ScriptableObject
{
    public static PlayerInfoDataBase instance;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public int playerCount;
    public List<PlayerDatas> playerDatas = new List<PlayerDatas>();
}

[Serializable]
public class PlayerDatas
{
    public int playerID;
    public int komaSets;
    public int currentKomaInKomaSets;
}
