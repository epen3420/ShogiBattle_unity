using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    private const int maxPlayerCount = 4;
    private int playerCount = 2;
    private Dictionary<KomaType, GameObject> komasDictionary = new Dictionary<KomaType, GameObject>();
    private PlayerInfo playerInfo;

    [SerializeField]
    private KomaDataBase komaDataBase;
    [SerializeField]
    private Transform PlayersKomasParent;

    private void Awake()
    {
        playerInfo = JsonManager.LoadFromLocal<PlayerInfo>("playerInfo");
    }

    private IEnumerator Start()
    {
        yield return StartCoroutine(GenerateKomaDictionary());

        yield return StartCoroutine(InstantiateBoard());

        StartCoroutine(GameManage());
    }

    private IEnumerator GameManage()
    {
        yield return InstantiateKoma(Vector3.zero, Quaternion.identity);

        /*初期化処理
            オブジェクトを設置

        ラウンド終了を検知

        リセット処理

        もし最後まで行ってなかったら
        GameManager();*/
    }

    /// <summary>
    /// playerInfo.jsonに保存されたcurrentKomaをposとrotationを指定して生成する関数
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    private IEnumerator InstantiateKoma(Vector3 pos, Quaternion rotation)
    {
        for (int i = 0; i < playerCount; i++)
        {
            KomaType generateKomaType = PlayerKomaType(i, playerInfo.playerDatas[i].currentKomaInKomaSets);
            GameObject playerKoma = Instantiate(komasDictionary[generateKomaType], pos, rotation);
            yield return playerKoma;
            playerKoma.transform.SetParent(PlayersKomasParent);
            Debug.Log($"Instantiated koma: {playerKoma.name}");
        }
        Debug.Log("Finished instantiating all koma.");
    }

    // Task ↑↓共通にできそうじゃね
    private IEnumerator InstantiateBoard()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("Board");
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            yield return Instantiate(handle.Result);
        }
    }

    /// <summary>
    /// KomaTypeをKeyに、対応するValueにPrefabをするDictionaryを作る関数
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateKomaDictionary()
    {
        foreach (var komaData in komaDataBase.komaDatasList)
        {
            string objPath = komaData.name.ToString();
            var handle = Addressables.LoadAssetAsync<GameObject>(objPath);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                komasDictionary[komaData.name] = handle.Result;
            }
        }
        Debug.Log($"Generated KomaDictionary");
    }


    // プレイヤーIDと駒セットのListの要素番号を入れることでKomaTypeを返す関数
    private KomaType PlayerKomaType(int playerID, int komaNumInKomaSets)
    {
        var playerInfo = JsonManager.LoadFromLocal<PlayerInfo>("playerInfo");
        return komaDataBase.komaSetsList[playerInfo.playerDatas[playerID].komaSets].komaType[komaNumInKomaSets];
    }


    private void SetGradeKoma(int upPlayerID, int upNum)
    {
        for (int i = 0; i < playerInfo.playerDatas.Count; i++)
        {
            if (playerInfo.playerDatas[i].playerID == upPlayerID)
            {

                playerInfo.playerDatas[i].currentKomaInKomaSets += upNum;
                break;
            }
        }
        JsonManager.Save(playerInfo, "playerInfo");
    }
}
