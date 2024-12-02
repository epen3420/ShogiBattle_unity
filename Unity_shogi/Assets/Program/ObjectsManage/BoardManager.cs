using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BoardManager : MonoBehaviour
{
    private Bounds boardBounds;


    /// <summary>
    /// AddressableからBoardを読み込みInstantiateする関数
    /// </summary>
    public IEnumerator InstantiateBoard()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("Board");
        yield return handle;

        GameObject boardObj;
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

        boardBounds = boardObj.GetComponent<Collider>().bounds;
    }

    public Vector3[] GenerateCirclePositions(int generateCount)
    {
        // ボードの半径を計算
        float radius = Mathf.Min(boardBounds.extents.x, boardBounds.extents.z) - 0.5f;

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
            positions[i] = position + boardBounds.center;
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
