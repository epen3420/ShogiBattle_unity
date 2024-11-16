using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerModeInfo : MonoBehaviour
{
    private int maxPlayerCount = 4;
    private GameObject[] playerMode;

    [SerializeField]
    private GameObject playerModeParentObj;

    [SerializeField] private PlayerInfoDataBase playerInfoDataBase;

    private PlayerInfoDataBase runTimePlayerInfoDB;
    private void Awake()
    {
        runTimePlayerInfoDB = Instantiate(playerInfoDataBase);

        // 駒のセットを選択するためのDropdownのオブジェクトを取得
        playerMode = new GameObject[maxPlayerCount];
        for (int i = 0; i < maxPlayerCount; i++)
        {
            playerMode[i] = playerModeParentObj.transform.GetChild(i).gameObject;
        }
    }

    /// <summary>
    /// playerCountの分だけDropdownのオブジェクトを有効化する関数
    /// </summary>
    /// <param name="playerCount"></param>
    public void SetPlayerModeDropDown(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            playerMode[i].SetActive(true);
        }
    }

    /// <summary>
    /// DropDownから入力を取得しJsonファイルで出力する関数
    /// </summary>
    /// <param name="thisObj"></param>
    public void SetPlayerMode(GameObject thisObj)
    {
        // DropDownからの入力を取得
        TMP_Dropdown thisDropDown = thisObj.GetComponent<TMP_Dropdown>();
        int selectedOption = thisDropDown.value;
        // 選択が0（NULL）だった場合返す
        if (selectedOption == 0) return;

        var playerInfoDB = SetPlayerInfo(thisObj, selectedOption);

        runTimePlayerInfoDB = playerInfoDB;
    }

    /// <summary>
    /// PlayerInfoを作りそれを返す関数
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="option"></param>
    /// <returns></returns>
    private PlayerInfoDataBase SetPlayerInfo(GameObject obj, int option)
    {
        PlayerInfoDataBase playerInfoDB = new PlayerInfoDataBase
        {
            playerDatas = new List<PlayerDatas>()
        };

        for (int i = 0; i < maxPlayerCount; i++)
        {
            if (obj == playerMode[i])
            {
                PlayerDatas playerDatas = new PlayerDatas
                {
                    playerID = i,
                    komaSets = option,
                    currentKomaInKomaSets = 0
                };
                playerInfoDB.playerDatas.Add(playerDatas);
            }
        }
        return playerInfoDB;
    }
}
