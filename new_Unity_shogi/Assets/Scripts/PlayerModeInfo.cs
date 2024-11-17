using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerModeInfo : MonoBehaviour
{
    private int maxPlayerCount = 0;
    private GameObject[] playerMode;

    [SerializeField]
    private Transform playerModeParentObj;


    private void OnEnable()
    {
        GetPlayerModeObj();
    }

    /// <summary>
    /// 駒のセットを選択するためのDropdownのオブジェクトを取得
    /// </summary>
    private void GetPlayerModeObj()
    {
        int playerModeCount = playerModeParentObj.childCount;
        playerMode = new GameObject[playerModeCount];
        for (int i = 0; i < playerModeCount; i++)
        {
            playerMode[i] = playerModeParentObj.GetChild(i).gameObject;
        }
    }

    /// <summary>
    /// ボタンを押すとき、プレイヤー数とDropDownを設定する関数
    /// </summary>
    /// <param name="playerCount"></param>
    public void SetPlayerCount(int playerCount)
    {
        maxPlayerCount = playerCount;
        for (int i = 0; i < playerCount; i++)
        {
            playerMode[i].SetActive(true);
        }
    }

    public PlayerInfoDataBase playerInfo;
    /// <summary>
    /// ScriptableObjectを設定する関数
    /// </summary>
    public void SetPlayerInfo()
    {
        int[] dropdownOptions = GetDropDownOptions();
        if (dropdownOptions == null)
        {
            //エラー処理、再度入力させる
            Debug.Log("error");
            return;
        }
        playerInfo = GeneratePlayerInfo(dropdownOptions);
        Debug.Log($"Set PlayerInfo");
    }

    private int[] GetDropDownOptions()
    {
        int[] options = new int[maxPlayerCount];
        for (int i = 0; i < maxPlayerCount; i++)

        {
            var dropdown = playerMode[i].GetComponent<TMP_Dropdown>();
            int value = dropdown.value;
            if (value == 0) return null;
            options[i] = value;
        }
        return options;
    }

    private PlayerInfoDataBase GeneratePlayerInfo(int[] selectedOptions)
    {
        var playerInfoDB = ScriptableObject.CreateInstance<PlayerInfoDataBase>();
        playerInfoDB.playerDatas = new List<PlayerDatas>();

        for (int i = 0; i < maxPlayerCount; i++)
        {
            PlayerDatas playerDatas = new PlayerDatas
            {
                playerID = i,
                komaSets = selectedOptions[i] - 1,
                currentKomaInKomaSets = 0
            };
            playerInfoDB.playerDatas.Add(playerDatas);
        }
        return playerInfoDB;
    }
}
