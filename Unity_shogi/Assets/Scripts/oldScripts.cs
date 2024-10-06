using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
  //prefabの盤、駒の定義
  [SerializeField] GameObject prefabBoard;
  [SerializeField] GameObject[] AllyKomas;
  [SerializeField] GameObject[] EnemyKomas;
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
  //盤面にある駒の参照
  public GameObject Now_Ally_Koma;
  public GameObject Now_Enemy_Koma;
  public GameObject Board;
  //駒の入れ替えのため駒の状況
  public int Now_ally;
  public int Now_Enemy;


  void Start()
  {
    //盤、駒の設置
    Board = Instantiate(prefabBoard, BoardPos, BoardRotate);
    Now_Ally_Koma = Instantiate(AllyKomas[Now_ally], AllyPos, AllyRotate);
    Now_Enemy_Koma = Instantiate(EnemyKomas[Now_Enemy], EnemyPos, EnemyRotate);
  }

  /* void ObjectsSet()
  {
    //盤、駒の設置
    Board = Instantiate(prefabBoard, BoardPos, BoardRotate);
    Now_Ally_Koma = Instantiate(AllyKomas[Now_ally], AllyPos, AllyRotate);
    Now_Enemy_Koma = Instantiate(EnemyKomas[Now_Enemy], EnemyPos, EnemyRotate);
  } */

  void SwitchTurn()
  {

  }

  void UpGradeKoma(GameObject koma)
  {
    if (koma == Now_Ally_Koma)
      Now_ally++;
    if (koma == Now_Enemy_Koma)
      Now_Enemy++;
  }

  void DownGradeKoma(GameObject koma)
  {
    if (koma == Now_Ally_Koma)
      Now_ally--;
    if (koma == Now_Enemy_Koma)
      Now_Enemy--;
  }
}


public class ObjectsController : MonoBehaviour
{
  //prefabの盤、駒の定義
  [SerializeField] GameObject prefabBoard;
  [SerializeField] GameObject[] allyKomas;
  [SerializeField] GameObject[] enemyKomas;
  //オブジェクト位置の定義
  private Vector3 boardPos = new Vector3(0, 0, 0);
  private Quaternion boardRotate = Quaternion.identity;
  private Vector3 allyPos = new Vector3(0.0f, 1.0f, -4.0f);
  private Quaternion allyRotate = Quaternion.Euler(0, 180, 0);
  private Vector3 enemyPos = new Vector3(0.0f, 1.0f, 4.0f);
  private Quaternion enemyRotate = Quaternion.identity;

  //盤面にある駒の参照
  private GameObject board;
  private GameObject nowAllyKoma;
  private GameObject nowEnemyKoma;

  private Rigidbody rb;
  private float swipeForceMultiplier = 10f;

  [SerializeField] private InitializeObjects initializeObjects;
  [SerializeField] private TurnSwitcher turnSwitcher;
  [SerializeField] private bool isAlly;
  [SerializeField] private int komaGrade = 0;


  private void Awake()
  {
    rb = GetComponent<Rigidbody>();
  }

  private void Start()
  {
    SetObjects(0, 0);
    if (initializeObjects.NowAllyKoma == gameObject)
    {
      isAlly = true;
    }
    else
    {
      isAlly = false;
    }
    GameObject.Find("TurnSwitcher").GetComponent<TurnSwitcher>().TurnSwitch(true);
  }

  private void SetObjects(int nowAlly, int nowEnemy)
  {
    //盤、駒の設置
    board = Instantiate(prefabBoard, boardPos, boardRotate);
    nowAllyKoma = Instantiate(allyKomas[nowAlly], allyPos, allyRotate);
    nowEnemyKoma = Instantiate(enemyKomas[nowEnemy], enemyPos, enemyRotate);
  }


  public void Move(Vector3 swipeDistance)
  {
    //スワイプの方向の算出
    Vector3 swipeDirection = -swipeDistance.normalized;
    //スワイプの力の算出
    float swipeForce = swipeDistance.magnitude * swipeForceMultiplier;
    //Rigidbodyに力を加える
    rb.AddForce(swipeDirection * swipeForce, ForceMode.Impulse);

    turnSwitcher.TurnSwitch(isAlly);
  }

  void UpGradeKoma()
  {
    if (komaGrade < 6)
      komaGrade++;
  }

  void DownGradeKoma()
  {
    if (komaGrade > 0)
      komaGrade--;
  }
}


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

  public void ShootKoma(Vector3 vector3)
  {
    //スワイプの間隔を受け取りSwipe処理を行う
    NowControlling.GetComponent<KomaController>().HitKoma(vector3);
    //スワイプが終わったらターンを切り替える
    SwitchTurn();
    //Invoke("SwitchTurn", 2.0f);//ここは死ぬのに2秒間かかった場合SwitchTurnの条件分岐でミスる
  }


  //アタッチされているオブジェクトにColiderがついている。
  //範囲外にオブジェクト（駒）が出たら検知
  private void OnTriggerExit(Collider fallobj)
  {
    //範囲外に出たオブジェクトを削除
    Destroy(fallobj.gameObject);
    gameManager.RoundEnd(fallobj.gameObject);
  }
}


public class GameManager : MonoBehaviour
{
  private MainSceneManager mainSceneManager;
  private GameObject mainSceneManager_obj;
  private GameObject inputManager_obj;
  private UIManager uIManager;


  void Start()
  {
    uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    mainSceneManager_obj = GameObject.FindWithTag("MainSceneManager");
    mainSceneManager = mainSceneManager_obj.GetComponent<MainSceneManager>();
    inputManager_obj = GameObject.Find("InputManager");
    uIManager.ButtonTitle.gameObject.SetActive(false);
  }

  /* private void StartManager(bool Active)
  {
    mainSceneManager_obj.SetActive(Active);
    inputManager_obj.SetActive(Active);
  } */

  public void RoundEnd(GameObject RoundLooser)
  {
    GameObject AllyKoma = mainSceneManager.Now_ally_Koma;
    GameObject EnemyKoma = mainSceneManager.Now_Enemy_Koma;
    Destroy(mainSceneManager.Board);
    if (RoundLooser == AllyKoma)
    {
      if (mainSceneManager.Now_Enemy == 5)
        GameEnd(false);
      if (mainSceneManager.Now_Enemy < 5)
        mainSceneManager.Now_Enemy++;
      if (mainSceneManager.Now_ally > 0)
        mainSceneManager.Now_ally--;
      Destroy(EnemyKoma);
    }
    if (RoundLooser == EnemyKoma)
    {
      if (mainSceneManager.Now_ally == 5)
        GameEnd(true);
      if (mainSceneManager.Now_ally < 5)
        mainSceneManager.Now_ally++;
      if (mainSceneManager.Now_Enemy > 0)
        mainSceneManager.Now_Enemy--;
      Destroy(AllyKoma);
    }
    mainSceneManager.isPlayerTurn = true;
    mainSceneManager.ObjectsSet();
  }

  private void GameEnd(bool Winner)
  {
    inputManager_obj.SetActive(false);
    uIManager.ButtonTitle.gameObject.SetActive(true);
    uIManager.TextTurnInfo.gameObject.SetActive(false);
    uIManager.WinnerInfo(Winner);
    mainSceneManager_obj.SetActive(false);
  }
}
