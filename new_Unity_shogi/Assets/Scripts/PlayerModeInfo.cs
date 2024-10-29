using System.Collections.Generic;
using UnityEngine;

public class PlayerModeInfo : MonoBehaviour
{
    public void SetPlayerMode(int playerMode)
    {
        PlayerInfo playerInfo = new PlayerInfo
        {
            playerDatas = new List<PlayerDatas>()
        };
    }
}

public class PlayerInfo
{
    public List<PlayerDatas> playerDatas = new List<PlayerDatas>();
}

public class PlayerDatas
{
    int id;
}
