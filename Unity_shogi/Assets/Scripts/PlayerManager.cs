/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerManager : MonoBehaviour
{
    private Vector3[] pos;
    private Quaternion[] quaternions;
    private int playerCount = 2;
    private Dictionary<KomaType, GameObject> komasDictionary = new Dictionary<KomaType, GameObject>();
    private PlayerInfo playerInfo;
    private GameObject[] playersKoma;
    private GameObject boardObj;


    [SerializeField]
    private KomaDataBase komaDataBase;


    private void Awake()
    {
        playerInfo = JsonManager.LoadFromLocal<PlayerInfo>("playerInfo");
    }

    private IEnumerator Start()
    {
        yield return GenerateKomaDictionary();

        yield return InstantiateBoard();

        GeneratePos();
    }

    private IEnumerator init()
    {
        yield return InstantiateKoma();
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
            KomaType generateKomaType = PlayerKomaType(i, playerInfo.playerDatas[i].currentKomaInKomaSets);
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

    private void GeneratePos()
    {
        Bounds bounds = boardObj.GetComponent<Collider>().bounds;
        float radius = bounds.max.x - bounds.center.x - 0.5f;

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
 */
