using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayersManager : MonoBehaviour
{
    private Dictionary<int, GameObject> playersKoma;
    private Dictionary<KomaType, GameObject> komasDictionary = new Dictionary<KomaType, GameObject>();
    private PlayerInfoDataBase playerInfoDB;

    private BoardManager boardManager;
    public BoardManager BoardManager
    {
        set { boardManager = value; }
    }
    [SerializeField]
    private KomaDataBase komaDataBase;


    private void OnEnable()
    {
        KomaHP.OnKomaDeath += ObserveSurvivingPlayer;
    }

    private void OnDisable()
    {
        KomaHP.OnKomaDeath -= ObserveSurvivingPlayer;
    }

    private void ObserveSurvivingPlayer(GameObject koma)
    {
        DownGradeKoma(playersKoma.);
    }

    private void SetGradeKoma(int playerID, int upNum)
    {
        int currentKoma = playerInfoDB.playerDatas[playerID].currentKomaInKomaSets;
        if (0 < currentKoma && 6 > currentKoma)
        {
            currentKoma += upNum;
            playerInfoDB.playerDatas[playerID].currentKomaInKomaSets = currentKoma;
        }
    }

    private void UpGradeKoma(int playerID)
    {
        SetGradeKoma(playerID, 1);
    }

    private void DownGradeKoma(int playerID)
    {
        SetGradeKoma(playerID, -1);
    }

    public IEnumerator InitKoma()
    {
        playerInfoDB = PlayerInfoDataBase.instance;

        yield return GenerateKomaDictionary();
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
        var komasTransform = boardManager.GenerateCircleTransform(playerCount);

        playersKoma = new Dictionary<int, GameObject>();
        for (int i = 0; i < playerCount; i++)
        {
            KomaType generateKomaType = PlayerKomaType(i, playerInfoDB.playerDatas[i].currentKomaInKomaSets);
            GameObject playerKoma = Instantiate(komasDictionary[generateKomaType], komasTransform[i]);
            yield return playerKoma;

            playersKoma.Add(i, playerKoma);

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
}
