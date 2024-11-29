using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("カメラが移動する時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField]
    private float moveDuration = 2.0f;


    public IEnumerator MoveCamera(Transform[] komasTransform, int currentIndex, int nextIndex)
    {
        // 現在のターゲット間での移動
        Vector3 startPos = komasTransform[currentIndex].position;
        Vector3 endPos = komasTransform[nextIndex].position;
        Quaternion startRot = komasTransform[currentIndex].rotation;
        Quaternion endRot = komasTransform[nextIndex].rotation;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            // Lerp を使った滑らかな移動と回転
            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Lerp(endRot, startRot, t);

            yield return null;
        }

        // 次のターゲットへインデックスを更新
        currentIndex = nextIndex;
        nextIndex = (nextIndex + 1) % komasTransform.Length; // 配列をループする
    }

    private void UpdateIndex()
    {

    }
}
