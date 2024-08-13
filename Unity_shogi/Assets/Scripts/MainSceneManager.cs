using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    //Camera,Rigidbodyの取得
    private Camera mainCamera;
    private Rigidbody rb;
    //prefabの盤、駒を参照
    [SerializeField] GameObject Board;
    [SerializeField] GameObject[] AllyKomas;
    [SerializeField] GameObject[] EnemyKomas;
    //駒の入れ替えのため駒の状況
    public int Now_ally;
    public int Now_Enemy;
    //オブジェクトの位置参照
    private Vector3 BoardPos = new Vector3(0, 0, 0);
    private Vector3 AllyPos = new Vector3(0.0f, 1.0f, -4.0f);
    private Vector3 EnemyPos = new Vector3(0.0f, 1.0f, 4.0f);
    //どっちのターンか
    public bool Player_which = true;
    //スクリプトKomaController,InputManagerを参照するための命名
    private KomaController komaController;
    private InputManager inputManager;
    //
    public bool RoundEnd = false;


    void Start()
    {
        Player_which = true;
        //スクリプトInputManagerの取得
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        //カメラの取得
        mainCamera = Camera.main;
        //初期設置
        SetObjects();
    }

    void Update()
    {
        //スワイプしてなくて、ユーザー入力が終わっていたら
        if (komaController.CanSwipe == true && RoundEnd == false)
        {
            //スワイプ処理
            komaController.SwipeKoma(rb);
        }
        if (komaController.CanSwipe == false && RoundEnd == false)
        {
            SwitchTurn();
        }
    }

    private void SetObjects()
    {
        //盤、駒の設置
        Board = Instantiate(Board, BoardPos, Quaternion.Euler(0, 180, 0));
        AllyKomas[Now_ally] = Instantiate(AllyKomas[Now_ally], AllyPos, Quaternion.Euler(0, 180, 0));
        EnemyKomas[Now_Enemy] = Instantiate(EnemyKomas[Now_Enemy], EnemyPos, Quaternion.identity);
        //

        SetTurn();
        //AddComponent();
    }

    public void SwitchTurn()
    {
        //プレイヤーの交代
        Player_which = !Player_which;
        //ユーザー入力をリセット
        inputManager.CanInput = true;
        //スワイプ状況のリセット
        komaController.CanSwipe = true;
        SetTurn();
    }

    private void SetTurn()
    {
        if (Player_which)
        {
            //1Pカメラ設定
            mainCamera.gameObject.transform.position = new Vector3(0.0f, 8.0f, -3.5f);
            mainCamera.gameObject.transform.rotation = Quaternion.Euler(70f, 0f, 0f);
            //1P駒のRigidbody
            rb = AllyKomas[Now_ally].GetComponent<Rigidbody>();
            //KomaControllerを1Pの駒に付ける
            AllyKomas[Now_ally].AddComponent<KomaController>();
            //付けた1PのKomaControllerを参照
            komaController = AllyKomas[Now_ally].GetComponent<KomaController>();
            RoundEnd = false;
        }
        else
        {
            //2Pカメラ設定
            mainCamera.gameObject.transform.position = new Vector3(0.0f, 8.0f, 3.5f);
            mainCamera.gameObject.transform.rotation = Quaternion.Euler(70f, 180f, 0f);
            //2P駒のRigidbody
            rb = EnemyKomas[Now_Enemy].GetComponent<Rigidbody>();
            //KomaControllerを2Pの駒に付ける
            EnemyKomas[Now_Enemy].AddComponent<KomaController>();
            //付けた2PのKomaControllerを参照
            komaController = EnemyKomas[Now_Enemy].GetComponent<KomaController>();
            RoundEnd = false;
        }
    }


    private void AddComponent()
    {
        /* AllyKomas[Now_ally].AddComponent<KomaController>();
        EnemyKomas[Now_Enemy].AddComponent<KomaController>();*/

    }



    //アタッチされているオブジェクトにColiderがついている。
    //範囲外にオブジェクト（駒）が出たら検知
    private void OnTriggerExit(Collider DeadKoma)
    {
        //範囲外に出たオブジェクトを削除
        Destroy(DeadKoma.gameObject);
        RoundEnd = true;
        End();
    }

    private void End()
    {
        if (Player_which)
        {
            Destroy(AllyKomas[Now_ally]);
            if (Now_ally < 6)
                Now_ally++;
            if (Now_Enemy > 0)
                Now_Enemy--;
        }
        else
        {
            Destroy(EnemyKomas[Now_Enemy]);

            if (Now_ally > 0)
                Now_ally--;
            if (Now_Enemy < 6)
                Now_Enemy++;
        }
        Destroy(Board);
        Player_which = true;
        inputManager.CanInput = true;
        komaController.CanSwipe = true;
        SetObjects();
    }
}
