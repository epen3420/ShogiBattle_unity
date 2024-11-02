using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    private int maxPlayerCount = 1;
    // ここをDictionarｙにして名前（KomaType）で指定できるようにしたいかも
    private List<List<KomaType>> playersKomasList = new List<List<KomaType>>();
    private List<GameObject> currentPlayersKomaList = new List<GameObject>();

    [SerializeField]
    private KomaDataBase komaDataBase;


    private void Awake()
    {
        var playerInfo = JsonManager.LoadFromLocal<PlayerInfo>("playerInfo");
        // playersKomasList.Add(SetPlayersKomasList(playerInfo));
        for (int i = 0; i < komaDataBase.komaDatasList.Count; i++)
        {
            playersKomasList.Add(komaDataBase.komaSetsList[playerInfo.playerDatas[i].komaSets].komaType);
        }
        for (int i = 0; i < playersKomasList.Count; i++)
        {
            var player = playersKomasList[i];
            Debug.Log(player);
            Debug.Log(player[i]);
        }
        StartCoroutine(GameManage());
    }

    private IEnumerator GameManage()
    {
        // yield return SetPlayersKomaList(nowKomaNum);

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
            string objPath = currentPlayerKomasList[komaNum[i]].ToString();
            var handle = Addressables.LoadAssetAsync<GameObject>(objPath);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                currentPlayersKomaList.Add(handle.Result);
            }
        }
    }

    /// <summary>
    /// プレイヤーごとの選んだ駒セットの駒の名前をリストに
    /// </summary>
    /// <param name="playerInfo"></param>
    /// <returns></returns>
    private List<KomaType> SetPlayersKomasList(PlayerInfo playerInfo)
    {
        List<KomaType> komaList = new List<KomaType>();
        for (int i = 0; i < maxPlayerCount; i++)
        {
            var komaSet = komaDataBase.komaSetsList[playerInfo.playerDatas[i].komaSets];
            for (int j = 0; j < komaSet.komaType.Count; j++)
            {
                for (int k = 0; k < komaDataBase.komaDatasList.Count; k++)
                {
                    if (komaSet.komaType[j] == komaDataBase.komaDatasList[k].name)
                    {
                        KomaType objPath = komaDataBase.komaDatasList[k].name;
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
        }
        return komaList;
    }
}
