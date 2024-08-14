using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //スワイプ位置の変数
    private Vector3 GetMouseStartPos;
    private Vector3 GetMouseEndPos;
    //Ray時の位置の変数
    private Vector3 WorldStartPos;
    private Vector3 WorldEndPos;
    //カメラの定義
    private Camera mainCamera;
    //スクリプトMainSceneManagerを参照するための命名
    private MainSceneManager mainSceneManager;


    void Start()
    {
        //カメラ,MainSceneManagerの取得
        mainCamera = Camera.main;
        mainSceneManager = GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>();
    }

    void Update()
    {
        //マウスクリック時
        if (Input.GetMouseButtonDown(0))
        {
            //マウススワイプ開始時の位置の取得
            GetMouseStartPos = Input.mousePosition;
        }
        //マウスクリックを離した時
        if (Input.GetMouseButtonUp(0))
        {
            //マウススワイプ終了時の位置の取得
            GetMouseEndPos = Input.mousePosition;
            //MainSceneManagerにスワイプの間隔を知らせる
            mainSceneManager.ShootKoma(SwipeMouse());
        }
    }

    public Vector3 SwipeMouse()
    {
        //Rayとしてスワイプ開始時、終了時の位置を変換
        Ray rayStart = mainCamera.ScreenPointToRay(GetMouseStartPos);
        Ray rayEnd = mainCamera.ScreenPointToRay(GetMouseEndPos);

        if (Physics.Raycast(rayStart, out RaycastHit hitStart) && Physics.Raycast(rayEnd, out RaycastHit hitEnd))
        {
            //スワイプの開始と終了をRayとして取得
            WorldStartPos = hitStart.point;
            WorldEndPos = hitEnd.point;
        }
        //結果としてスワイプの間隔を返す
        return WorldEndPos - WorldStartPos;
    }
}
