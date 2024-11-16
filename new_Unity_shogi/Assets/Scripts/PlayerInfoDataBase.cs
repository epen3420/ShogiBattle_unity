/* using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoDataBase", menuName = "Scriptable Objects/PlayerInfoDataBase")]

public class PlayerInfoDataBase : ScriptableObject
{
    public List<PlayerDatas> playerDatas = new List<PlayerDatas>();

    public PlayerInfoDataBase instance;

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
    }
}

[System.Serializable]
public class PlayerDatas
{
    public int playerID;
    public int komaSets;
    public int currentKomaInKomaSets;
}
 */
