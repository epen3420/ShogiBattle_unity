using UnityEngine;

partial class KomaInfoManager : MonoBehaviour, ISetKomaGrade
{
    private int playerID;
    public int PlayerID
    {
        set { playerID = value; }
    }
    private int komaSet;
    public int KomaSet
    {
        set { komaSet = value; }
    }
    private int komaGrade;
    public int KomaGrade
    {
        set { komaGrade = value; }
    }
    private int playerCurrentKoma;
    private int playerCount;

    private void Awake()
    {
        playerCount = PlayerInfoDataBase.instance.playerCount;
        playerCurrentKoma = PlayerInfoDataBase.instance.playerDatas[playerID].currentKomaInKomaSets;
    }

    public void SetGradeKoma(int adjustmentNum, bool isSurvive)
    {
        if (isSurvive)
        {
            playerCurrentKoma = Mathf.Max(0, playerCurrentKoma + adjustmentNum);
        }
        else
        {
            playerCurrentKoma = Mathf.Max(6, playerCurrentKoma - adjustmentNum);
        }
        PlayerInfoDataBase.instance.playerDatas[playerID].currentKomaInKomaSets = playerCurrentKoma;
    }
}
