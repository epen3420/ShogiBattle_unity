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
