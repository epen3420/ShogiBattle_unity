using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class KomaManager : MonoBehaviour
{
    private Transform[] komasTransform;
    private int playerCount = 0;
    private Dictionary<KomaType, GameObject> komasDictionary = new Dictionary<KomaType, GameObject>();
    private PlayerInfoDataBase playerInfoDB;
    public List<GameObject> playersKoma;

    private BoardManager boardManager;
    public BoardManager SetBoardManager
    {
        set { boardManager = value; }
    }

    [SerializeField]
    private KomaDataBase komaDataBase;
    [SerializeField]
    private CameraController cameraController;


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
        DownGradeKoma(playersKoma.IndexOf(koma));
    }

    public IEnumerator InitKomaManager()
    {
        playerInfoDB = PlayerInfoDataBase.instance;
        playerCount = playerInfoDB.playerCount;

        yield return GenerateKomaDictionary();

        GenerateKomasTransform();
    }

    // プレイヤーIDと駒セットのListの要素番号を入れることでKomaTypeを返す関数
    private KomaType PlayerKomaType(int playerID, int komaNumInKomaSets)
    {
        return komaDataBase.komaSetsList[playerInfoDB.playerDatas[playerID].komaSets].komaType[komaNumInKomaSets];
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

    /// <summary>
    /// playerInfo.jsonに保存されたcurrentKomaをposとrotationを指定して生成する関数
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public IEnumerator InstantiateKoma()
    {
        playersKoma = new List<GameObject>();
        for (int i = 0; i < playerCount; i++)
        {
            KomaType generateKomaType = PlayerKomaType(i, playerInfoDB.playerDatas[i].currentKomaInKomaSets);
            GameObject playerKoma = Instantiate(komasDictionary[generateKomaType], komasTransform[i]);
            yield return playerKoma;
            playerKoma.transform.SetParent(this.transform);

            playersKoma.Add(playerKoma);
            Debug.Log($"Instantiated koma: {playerKoma.name}");
        }
        Debug.Log("Finished instantiating all koma.");
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

    private void GenerateKomasTransform()
    {
        GameObject boardObj = boardManager.GetBoardObj;
        Bounds bounds = boardObj.GetComponent<Collider>().bounds;
        float radius = Mathf.Min(bounds.extents.x, bounds.extents.z) - 0.5f;  // ボードの半径を計算

        komasTransform = new Transform[playerCount];
        for (int i = 0; i < playerCount; i++)
        {
            // 新しい GameObject を生成して Transform を設定
            GameObject komaObj = new GameObject($"Koma_{i + 1}");
            Transform komaTransform = komaObj.transform;

            // 座標と回転を計算して設定
            float deg = 360f * (i + 1) / playerCount;
            float radian = Mathf.Deg2Rad * (deg + 90);
            komaTransform.position = new Vector3(radius * Mathf.Cos(radian), 1, radius * Mathf.Sin(radian));
            komaTransform.position += bounds.center; // ボード中心を考慮
            komaTransform.rotation = Quaternion.Euler(0.0f, -deg, 0.0f);

            // 配列に Transform を保存
            komasTransform[i] = komaTransform;
        }
        Debug.Log("Generated komas position.");
    }
}
