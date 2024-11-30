using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BoardManager : MonoBehaviour
{
    private Bounds bounds;
    private GameObject boardObj;


    private void Awake()
    {
        bounds = boardObj.GetComponent<Collider>().bounds;
    }

    /// <summary>
    /// AddressableからBoardを読み込みInstantiateする関数
    /// </summary>
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

    /// <summary>
    /// generateCountに応じてBoard内に円形状に配置するようなTransformを作る関数
    /// </summary>
    /// <param name="generateCount"></param>
    /// <returns></returns>
    public Transform[] GenerateCircleTransform(int generateCount)
    {
        // ボードの半径を計算
        float radius = Mathf.Min(bounds.extents.x, bounds.extents.z) - 0.5f;

        var transforms = new Transform[generateCount];
        for (int i = 0; i < generateCount; i++)
        {
            // 新しいGameObjectを生成して仮のTransformを設定
            GameObject tempObj = new GameObject($"Koma_{i + 1}");
            Transform tempTransform = tempObj.transform;
            Destroy(tempObj);

            // 座標と回転を計算して設定
            float deg = 360f * (i + 1) / generateCount;
            float radian = Mathf.Deg2Rad * (deg + 90);
            tempTransform.position = new Vector3(radius * Mathf.Cos(radian), 1, radius * Mathf.Sin(radian));

            // Boardの中心を考慮
            tempTransform.position += bounds.center;

            // 生成するオブジェクトの方向
            tempTransform.rotation = Quaternion.Euler(0.0f, -deg, 0.0f);

            // 配列にtempTransformを保存
            transforms[i] = tempTransform;
        }
        return transforms;
    }
}
