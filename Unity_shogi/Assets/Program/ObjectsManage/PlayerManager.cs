using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerManager : MonoBehaviour
{
    private int currentKomaIndex = 0;
    private int maxIndex;
    // インプットマネージャーのスクリプトを取得するリスト
    private PlayerDatas playerDatas;
    public PlayerDatas PlayerDatas
    {
        set
        {
            playerDatas = value;
            Init();
        }
    }
    [SerializeField]
    private KomaDataBase komaDatas;

    private void Init()
    {
        maxIndex = komaDatas.komaSetsList[playerDatas.komaSets].komaType.Count;
    }

    public async UniTask<GameObject> GetNextKomaPrefab()
    {
        KomaType komaType = komaDatas.komaSetsList[playerDatas.komaSets].komaType[currentKomaIndex];
        try
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(komaType.ToString());
            GameObject loadedPrefab = await handle.Task;

            if (loadedPrefab == null)
            {
                Debug.LogError($"Failed to load asset for KomaType: {komaType}");
                return null;
            }
            return loadedPrefab;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Exception occurred while loading KomaType {komaType}: {ex.Message}");
            return null;
        }
    }

    public void InstantiateKoma(GameObject prefab, Vector3 pos, Quaternion rotation)
    {
        GameObject instance = Instantiate(prefab, pos, rotation);
        instance.transform.SetParent(this.transform);
    }

    private void SetGradeKoma(bool isSurvive)
    {
        if (currentKomaIndex > maxIndex - 1)
        {
            Debug.Log($"{transform.name}win");
        }
        else if (isSurvive)
        {
            currentKomaIndex++;
        }
        else
        {
            currentKomaIndex--;
        }
        playerDatas.currentKomaInKomaSets = Mathf.Clamp(currentKomaIndex, 0, maxIndex);
    }
}
