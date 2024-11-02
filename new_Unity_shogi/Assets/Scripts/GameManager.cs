using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    private int maxPlayerCount = 1;
    // ここをDictionarｙにして名前（KomaType）で指定できるようにしたいかも
    private List<List<string>> playersKomasList = new List<List<string>>();
    private List<GameObject> currentPlayersKomaList = new List<GameObject>();

    [SerializeField]
    private KomaDataBase komaDataBase;


    private void Awake()
    {
        var playerInfo = JsonManager.LoadFromLocal<PlayerInfo>("playerInfo");
        SetPlayersKomasList(playerInfo);
        StartCoroutine(GameManage());
    }

    private IEnumerator GameManage()
    {
        yield return SetPlayersKomaList(nowKomaNum);

        yield return InstantiateObj(currentPlayersKomaList, Vector3.zero, Quaternion.identity);
        /*
        初期化処理
            オブジェクトを設置

         ラウンド終了を検知

        リセット処理

        もし最後まで行ってなかったら
        GameManager();
        */
    }

    private IEnumerator InstantiateObj(List<GameObject> objList, Vector3 pos, Quaternion rotate)
    {
        List<GameObject> tempObj = new List<GameObject>();
        for (int i = 0; i < objList.Count; i++)
        {
            var obj = Instantiate(objList[i], pos, rotate);
            tempObj.Add(obj);
        }
        yield return tempObj;
    }

    private IEnumerator SetPlayersKomaList(int[] komaNum)
    {
        for (int i = 0; i < maxPlayerCount; i++)
        {
            var currentPlayerKomasList = playersKomasList[i];
            string objPath = currentPlayerKomasList[komaNum[i]];
            var handle = Addressables.LoadAssetAsync<GameObject>(objPath);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                currentPlayersKomaList.Add(handle.Result);
            }
        }
    }

    private void SetPlayersKomasList(PlayerInfo playerInfo)
    {
        for (int i = 0; i < maxPlayerCount; i++)
        {
            List<string> komaList = new List<string>();
            var komaSet = komaDataBase.komaSetsList[playerInfo.playerDatas[i].komaSets];
            for (int j = 0; j < komaSet.komaType.Count; j++)
            {
                for (int k = 0; k < komaDataBase.komaDatasList.Count; k++)
                {
                    if (komaSet.komaType[j] == komaDataBase.komaDatasList[k].name)
                    {
                        string objPath = komaDataBase.komaDatasList[k].name.ToString();
                        komaList.Add(objPath);
                        break;
                        /* var handle = Addressables.LoadAssetAsync<GameObject>(objPath);
                        yield return handle;
                        if (handle.Status == AsyncOperationStatus.Succeeded)
                        {
                            komaList.Add(handle.Result);
                            break;
                        } */
                    }
                }
            }
            playersKomasList.Add(komaList);
        }
    }
}
