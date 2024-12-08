using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BoardManager : MonoBehaviour
{
    private Bounds boardColBounds;


    /// <summary>
    /// AddressableからBoardを読み込みInstantiateする関数
    /// </summary>
    public async UniTask InstantiateBoard()
    {
        try
        {
            var handle = Addressables.LoadAssetAsync<GameObject>("Board");
            GameObject loadedPrefab = await handle.Task;

            if (loadedPrefab == null)
            {
                Debug.LogError($"Failed to load asset for Board");
                return;
            }

            GameObject instance = Instantiate(loadedPrefab, Vector3.zero, Quaternion.identity);

            instance.transform.SetParent(this.transform);

            boardColBounds = instance.GetComponent<Collider>().bounds;
            return;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Exception occurred while loading Board: {ex.Message}");
            return;
        }


    }

    public Vector3[] GenerateCirclePositions(int generateCount)
    {
        // ボードの半径を計算
        float radius = Mathf.Min(boardColBounds.extents.x, boardColBounds.extents.z) - 0.5f;

        var positions = new Vector3[generateCount];
        for (int i = 0; i < generateCount; i++)
        {
            // 座標を計算
            float deg = 360f * (i + 1) / generateCount;
            float radian = Mathf.Deg2Rad * (deg + 90);
            Vector3 position = new Vector3(
                radius * Mathf.Cos(radian),
                4,
                radius * Mathf.Sin(radian)
            );

            // Boardの中心を考慮
            positions[i] = position + boardColBounds.center;
        }
        return positions;
    }

    public Quaternion[] GenerateCircleRotations(int generateCount)
    {
        var rotations = new Quaternion[generateCount];
        for (int i = 0; i < generateCount; i++)
        {
            // 回転を計算
            float deg = 360f * (i + 1) / generateCount;
            rotations[i] = Quaternion.Euler(0.0f, -deg, 0.0f);
        }
        return rotations;
    }
}
