using UnityEngine;

partial class KomaInfoManager : MonoBehaviour
{
    private int playerID;
    private int komaSet;
    private int komaGrade;

    private PlayerDatas playerInfo;
    public PlayerDatas PlayerInfo
    {
        set
        {
            playerInfo = value;
            InitParameter(playerInfo);
        }
    }

    private void InitParameter(PlayerDatas datas)
    {
        playerID = datas.playerID;
        komaSet = datas.komaSets;
        komaGrade = datas.currentKomaInKomaSets;
    }
}
