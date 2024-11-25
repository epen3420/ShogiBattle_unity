using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BoardManager : MonoBehaviour
{
    private GameObject boardObj;
    public GameObject GetBoardObj
    {
        get
        {
            if (boardObj == null)
            {
                Debug.LogWarning("Board has not been instantiated yer.");
            }
            return boardObj;
        }
    }


    /// <summary>
    /// AddressableからBoardを読み込みInstantiateする関数
    /// </summary>
    /// <returns></returns>
    public IEnumerator InstantiateBoard()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("Board");
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            boardObj = Instantiate(handle.Result);
        }
        else
        {
            Debug.LogError("Failed to load the board");
            yield break;
        }

        boardObj.transform.SetParent(this.transform);
    }
}
