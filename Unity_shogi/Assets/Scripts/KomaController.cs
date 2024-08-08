using UnityEngine;

public class KomaController : MonoBehaviour
{
    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;
    private Vector3 flickDirection;
    private float flickForceMultiplier = 10f; // 弾く力の強さを調整するための値

    private Rigidbody rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスの開始位置を記録
            mouseStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // マウスの終了位置を記録
            mouseEndPos = Input.mousePosition;

            // レイキャストを使ってワールド座標を取得
            Ray rayStart = mainCamera.ScreenPointToRay(mouseStartPos);
            Ray rayEnd = mainCamera.ScreenPointToRay(mouseEndPos);
            if (Physics.Raycast(rayStart, out RaycastHit hitStart) && Physics.Raycast(rayEnd, out RaycastHit hitEnd))
            {
                Vector3 worldStartPos = hitStart.point;
                Vector3 worldEndPos = hitEnd.point;

                // 弾く方向と力を計算
                flickDirection = (worldStartPos - worldEndPos).normalized;
                float flickForce = (worldEndPos - worldStartPos).magnitude * flickForceMultiplier;

                // Rigidbodyに力を適用
                rb.AddForce(flickDirection * flickForce, ForceMode.Impulse);
            }
        }
    }
}
