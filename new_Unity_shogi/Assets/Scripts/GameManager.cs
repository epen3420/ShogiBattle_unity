using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    private const int maxPlayerCount = 4;
    private Vector3[] pos;
    private Quaternion[] quaternions;
    private int playerCount = 3;
    private Dictionary<KomaType, GameObject> komasDictionary = new Dictionary<KomaType, GameObject>();
    private GameObject[] playersKoma;
    private GameObject boardObj;
    private PlayerInfoDataBase playerInfoDB;
    [SerializeField]
    private KomaDataBase komaDataBase;


    private void Awake()
    {
        // 下をリストで受け取るだから上の宣言も変える
        playerInfoDB = PlayerInfoDataBase.instance;
    }

    private IEnumerator Start()
    {
        yield return StartCoroutine(GenerateKomaDictionary());

        yield return StartCoroutine(InstantiateBoard());

        GeneratePos();

        StartCoroutine(GameManage());
    }

    private IEnumerator GameManage()
    {
        yield return InstantiateKoma(pos, quaternions);

        /* 初期化処理
            オブジェクトを設置

        ラウンド終了を検知

        リセット処理

        もし最後まで行ってなかったら
        GameManager(); */
    }

    /// <summary>
    /// playerInfo.jsonに保存されたcurrentKomaをposとrotationを指定して生成する関数
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    private IEnumerator InstantiateKoma(Vector3[] pos, Quaternion[] rotation)
    {
        playersKoma = new GameObject[playerCount];
        for (int i = 0; i < playerCount; i++)
        {
            KomaType generateKomaType = PlayerKomaType(i, playerInfoDB.playerDatas[i].currentKomaInKomaSets);
            GameObject playerKoma = Instantiate(komasDictionary[generateKomaType], pos[i], rotation[i]);
            yield return playerKoma;
            playerKoma.transform.SetParent(this.transform);
            playersKoma[i] = playerKoma;
            Debug.Log($"Instantiated koma: {playerKoma.name}");
        }
        Debug.Log("Finished instantiating all koma.");
    }

    private IEnumerator InstantiateBoard()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("Board");
        yield return handle;
        boardObj = Instantiate(handle.Result);
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            yield return boardObj;
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
        return komaDataBase.komaSetsList[playerInfoDB.playerDatas[playerID].komaSets].komaType[komaNumInKomaSets];
    }


    private void SetGradeKoma(int upPlayerID, int upNum)
    {
        for (int i = 0; i < playerInfoDB.playerDatas.Count; i++)
        {
            if (playerInfoDB.playerDatas[i].playerID == upPlayerID)
            {

                playerInfoDB.playerDatas[i].currentKomaInKomaSets += upNum;
                break;
            }
        }
    }

    private void GeneratePos()
    {
        Bounds bounds = boardObj.GetComponent<Collider>().bounds;
        float radius = Mathf.Min(bounds.extents.x, bounds.extents.z) - 0.5f;  // ボードの半径を計算


        pos = new Vector3[playerCount];
        quaternions = new Quaternion[playerCount];
        for (int i = 0; i < playerCount; i++)
        {
            float deg = 360f * (i + 1) / playerCount;
            float radian = Mathf.Deg2Rad * (deg + 90);
            pos[i] = new Vector3(radius * Mathf.Cos(radian), 1, radius * Mathf.Sin(radian));
            pos[i] += bounds.center;
            quaternions[i] = Quaternion.Euler(0.0f, -deg, 0.0f);
        }
    }

}
