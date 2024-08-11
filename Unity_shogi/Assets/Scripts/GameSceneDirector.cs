using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameSceneDirector : MonoBehaviour
{
    //オブジェクト参照
    [SerializeField] GameObject[] AllyKoma_obj = new GameObject[6];
    [SerializeField] GameObject[] EnemyKoma_obj = new GameObject[6];
    [SerializeField] GameObject prefabBoard;
    //オブジェクト定義
    private GameObject Board;
    //スワイプ時のマウスの状態
    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;
    private Vector3 SwipeDirection;
    //スワイプの強さ
    private float SwipeForceMultiplier = 10f;
    //定義
    private Rigidbody rb;
    private Camera mainCamera;
    //どっちのターンか
    private bool Player_which = true;
    //スワイプされたかどうか
    private bool hasSwiped = false;
    //スワイプ後の待機秒数
    private float Wait_Swipe = 2.0f;

    void Start()
    {
        InitialSet();
    }

    void Update()
    {
        //スワイプされてない場合スワイプ処理
        if (!hasSwiped)
        {
            Swipe_Koma();
        }
    }
    public void InitialSet()
    {
        // 将棋盤設置
        Vector3 pos = new Vector3(0, 0, 0);
        Board = Instantiate(prefabBoard, pos, Quaternion.identity);
        // 駒の初期配置
        Vector3 OnePpos = new Vector3(0, 1, -4);
        AllyKoma_obj[0] = Instantiate(AllyKoma_obj[0], OnePpos, Quaternion.Euler(0, 180, 0));
        Vector3 TwoPpos = new Vector3(0, 1, 4);
        EnemyKoma_obj[0] = Instantiate(EnemyKoma_obj[0], TwoPpos, Quaternion.identity);
        //カメラ設定
        mainCamera = Camera.main;
        //ターン設定
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
            rb = AllyKoma_obj[0].GetComponent<Rigidbody>();
        }
        else
        {
            //2Pカメラ設定
            mainCamera.gameObject.transform.position = new Vector3(0.0f, 8.0f, 3.5f);
            mainCamera.gameObject.transform.rotation = Quaternion.Euler(70f, 180f, 0f);
            //2P駒のRigidbody
            rb = EnemyKoma_obj[0].GetComponent<Rigidbody>();
        }
    }

    public void Swipe_Koma()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseEndPos = Input.mousePosition;

            Ray rayStart = mainCamera.ScreenPointToRay(mouseStartPos);
            Ray rayEnd = mainCamera.ScreenPointToRay(mouseEndPos);

            if (Physics.Raycast(rayStart, out RaycastHit hitStart) && Physics.Raycast(rayEnd, out RaycastHit hitEnd))
            {
                Vector3 worldStartPos = hitStart.point;
                Vector3 worldEndPos = hitEnd.point;

                SwipeDirection = (worldStartPos - worldEndPos).normalized;
                float SwipeForce = (worldEndPos - worldStartPos).magnitude * SwipeForceMultiplier;

                rb.AddForce(SwipeDirection * SwipeForce, ForceMode.Impulse);
                //スワイプされた
                hasSwiped = true;
                //数秒間待つ
                StartCoroutine("DelaySwitchTurn");
            }
        }
    }

    private IEnumerator DelaySwitchTurn()
    {
        //数秒間待つ
        yield return new WaitForSeconds(Wait_Swipe);
        //待った後、ターン交代
        SwitchTurn();
    }
    /* public void Dead()
    {
        //死亡判定のためIsDeadスクリプト取得
        IsDead IsDead_script;
        GameObject WorldRange_obj = GameObject.Find("WorldRange");
        IsDead_script = WorldRange_obj.GetComponent<IsDead>();
        //死亡したら当たったオブジェクトの削除
        Destroy(IsDead_script);

        EndRound();
    } */

    private void SwitchTurn()
    {
        //プレイヤー交代
        Player_which = !Player_which;
        //スワイプされたかの初期化
        hasSwiped = false;
        //ターン設定
        SetTurn();
    }
    public void EndRound()
    {
        Destroy(AllyKoma_obj[0]);
        Destroy(EnemyKoma_obj[0]);
        Destroy(Board);
    }
}
