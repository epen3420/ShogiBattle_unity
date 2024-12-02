using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayersManager : MonoBehaviour
{
    private Dictionary<KomaType, GameObject> komasDictionary;
    private Dictionary<PlayerDatas, bool> komasDead;
    public PlayerInfoDataBase playerInfoDB;

    private BoardManager boardManager;
    public BoardManager BoardManager
    {
        set { boardManager = value; }
    }
    [SerializeField]
    private KomaDataBase komaDataBase;


    private void OnEnable()
    {
        playerInfoDB = PlayerInfoDataBase.instance;
    }

    public void Init()
    {
        for (int i = 0; i < playerInfoDB.playerCount; i++)
        {
            playerInfoDB.playerDatas[i].isDead = false;
        }

        foreach (Transform child in transform)
        {
            //自分の子供をDestroyする
            Destroy(child.gameObject);
        }
    }

    public PlayerDatas ObserveGameWinner()
    {
        for (int i = 0; i < playerInfoDB.playerCount; i++)
        {
            var datas = playerInfoDB.playerDatas[i];
            if (datas.currentKomaInKomaSets > 5)
            {
                return datas;
            }
        }
        return null;
    }

    public bool IsDeadAll()
    {
        int isDeadCount = 0;
        komasDead = new Dictionary<PlayerDatas, bool>();
        for (int i = 0; i < playerInfoDB.playerCount; i++)
        {
            var datas = playerInfoDB.playerDatas[i];
            if (datas.isDead)
            {
                komasDead.Add(datas, false);
                isDeadCount++;
            }
            else
            {
                komasDead.Add(datas, true);
            }
        }
        return playerInfoDB.playerCount - 1 <= isDeadCount;
    }

    public void SetGradeAllKoma()
    {
        for (int i = 0; i < playerInfoDB.playerCount; i++)
        {
            SetGradeKoma(playerInfoDB.playerDatas[i], komasDead[playerInfoDB.playerDatas[i]]);
        }
    }

    private void SetGradeKoma(PlayerDatas datas, bool isSurvive)
    {
        int currentKoma = datas.currentKomaInKomaSets;
        if (isSurvive)
        {
            currentKoma++;
        }
        else
        {
            currentKoma--;
        }
        datas.currentKomaInKomaSets = Mathf.Clamp(currentKoma, 0, 6);
    }

    /// <summary>
    /// playerInfo.jsonに保存されたcurrentKomaをposとrotationを指定して生成する関数
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public IEnumerator InstantiateKoma()
    {
        int playerCount = playerInfoDB.playerCount;
        Vector3[] komasPos = boardManager.GenerateCirclePositions(playerCount);
        Quaternion[] komasRot = boardManager.GenerateCircleRotations(playerCount);

        for (int i = 0; i < playerCount; i++)
        {
            var playerDatas = playerInfoDB.playerDatas[i];
            KomaType generateKomaType = PlayerKomaType(i, playerDatas.currentKomaInKomaSets);
            GameObject playerKoma = Instantiate(komasDictionary[generateKomaType], komasPos[i], komasRot[i]);
            yield return playerKoma;

            var playerKomaInfo = playerKoma.GetComponent<KomaHP>();
            playerKomaInfo.Datas = playerDatas;

            playerKoma.transform.SetParent(this.transform);

            Debug.Log($"Instantiated koma: {playerKoma.name}");
        }
        Debug.Log("Finished instantiating all koma.");
    }

    private KomaType PlayerKomaType(int playerID, int komaNumInKomaSets)
    {
        return komaDataBase.komaSetsList[playerInfoDB.playerDatas[playerID].komaSets].komaType[komaNumInKomaSets];
    }

    /// <summary>
    /// KomaTypeをKeyに、対応するValueにPrefabをするDictionaryを作る関数
    /// </summary>
    /// <returns></returns>
    public IEnumerator GenerateKomaDictionary()
    {
        komasDictionary = new Dictionary<KomaType, GameObject>();
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
}
