using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 GetMouseStartPos; // スワイプ位置の変数
    private Vector3 GetMouseEndPos;
    private Camera mainCamera; // カメラの定義
    private KomaController komaController;

    [SerializeField]
    private bool isPlayer = false;
    public bool IsPlayer
    {
        set { isPlayer = value; }
    }


    private void Start()
    {
        mainCamera = Camera.main;
        komaController = GetComponent<KomaController>();
    }

    private void Update()
    {
        //マウスクリック時
        if (Input.GetMouseButtonDown(0))
        {
            //マウススワイプ開始時の位置の取得
            GetMouseStartPos = Input.mousePosition;
        }
        //マウスクリックを離した時
        else if (Input.GetMouseButtonUp(0))
        {
            //マウススワイプ終了時の位置の取得
            GetMouseEndPos = Input.mousePosition;
            //MainSceneManagerにスワイプの間隔を知らせる
            komaController.HitKoma(GetMouseEndPos - GetMouseStartPos);
        }
    }

    /// <summary>
    /// スクリーンを基準に二点の三次元座標の距離を測る関数
    /// </summary>
    /// <returns></returns>
    private Vector3 InputScreenDistance(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 screenStartPoint = mainCamera.WorldToScreenPoint(startPoint);
        Vector3 screenEndPoint = mainCamera.WorldToScreenPoint(endPoint);

        Vector3.Distance(screenStartPoint, screenEndPoint);
        return Vector3.zero;
    }
}
