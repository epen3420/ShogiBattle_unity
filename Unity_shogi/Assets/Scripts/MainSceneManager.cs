using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    //Cameraの取得
    private Camera mainCamera;
    //prefabの盤、駒の定義
    [SerializeField] GameObject prefabBoard;
    [SerializeField] GameObject[] AllyKomas;
    [SerializeField] GameObject[] EnemyKomas;
    //盤面にある駒の参照
    public GameObject Now_ally_Koma;
    public GameObject Now_Enemy_Koma;
    private GameObject NowControlling;
    public GameObject Board;
    private bool isStop;
    //駒の入れ替えのため駒の状況
    public int Now_ally;
    public int Now_Enemy;
    //オブジェクト位置の定義
    private Vector3 BoardPos = new Vector3(0, 0, 0);
    private Quaternion BoardRotate = Quaternion.identity;
    private Vector3 AllyPos = new Vector3(0.0f, 1.0f, -4.0f);
    private Quaternion AllyRotate = Quaternion.Euler(0, 180, 0);
    private Vector3 AllyCameraPos = new Vector3(0.0f, 8.0f, -3.5f);
    private Quaternion AllyCameraRotate = Quaternion.Euler(70f, 0f, 0f);
    private Vector3 EnemyPos = new Vector3(0.0f, 1.0f, 4.0f);
    private Quaternion EnemyRotate = Quaternion.identity;
    private Vector3 EnemyCameraPos = new Vector3(0.0f, 8.0f, 3.5f);
    private Quaternion EnemyCameraRotate = Quaternion.Euler(70f, 180f, 0f);
    //true:1P, false:2P
    public bool isPlayerTurn;
    //スクリプトKomaControllerを参照するための命名
    private GameManager gameManager;
    private UIManager uIManager;
    private Coroutine _switchturn;


    void Start()
    {
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        uIManager.TextWinnerInfo.gameObject.SetActive(false);
        //カメラの取得
        mainCamera = Camera.main;
        //プレイヤーターンを初期化
        isPlayerTurn = true;
        //初期設置
        ObjectsSet();
    }

    void Update()
    {
        if (Now_ally_Koma.GetComponent<Rigidbody>().IsSleeping() && Now_Enemy_Koma.GetComponent<Rigidbody>().IsSleeping())
            isStop = true;
        else isStop = false;
    }

    public void ObjectsSet()
    {
        uIManager.TurnInfo(isPlayerTurn);
        //盤、駒の設置
        Board = Instantiate(prefabBoard, BoardPos, BoardRotate);
        Now_ally_Koma = Instantiate(AllyKomas[Now_ally], AllyPos, AllyRotate);
        Now_Enemy_Koma = Instantiate(EnemyKomas[Now_Enemy], EnemyPos, EnemyRotate);
        //ターンをセット
        SetTurn();
    }

    private void SetTurn()
    {
        if (isPlayerTurn)
        {
            //1Pカメラ設定
            mainCamera.gameObject.transform.position = AllyCameraPos;
            mainCamera.gameObject.transform.rotation = AllyCameraRotate;
            //付けた1PのKomaControllerを参照
            NowControlling = Now_ally_Koma;
        }
        else
        {
            //2Pカメラ設定
            mainCamera.gameObject.transform.position = EnemyCameraPos;
            mainCamera.gameObject.transform.rotation = EnemyCameraRotate;
            //付けた2PのKomaControllerを参照
            NowControlling = Now_Enemy_Koma;
        }
    }

    private IEnumerator SwitchTurn()
    {
        yield return new WaitUntil(() => isStop);
        yield return new WaitForSeconds(2.0f);

        //プレイヤーの交代
        isPlayerTurn = !isPlayerTurn;
        uIManager.TurnInfo(isPlayerTurn);
        //ターンをセット
        SetTurn();
    }

    public void ShootKoma(Vector3 swipeDistance)
    {
        if (swipeDistance == new Vector3(0, 0, 0)) return;//もう少し良くする
        //スワイプの間隔を受け取りSwipe処理を行う
        NowControlling.GetComponent<KomaController>().SwipeKoma(swipeDistance);
        //スワイプが終わったらターンを切り替える
        _switchturn = StartCoroutine(SwitchTurn());
    }


    //アタッチされているオブジェクトにColiderがついている。
    //範囲外にオブジェクト（駒）が出たら検知
    private void OnTriggerExit(Collider fallobj)
    {
        //範囲外に出たオブジェクトを削除
        Destroy(fallobj.gameObject);
        StopCoroutine(_switchturn);
        gameManager.RoundEnd(fallobj.gameObject);
    }
}
