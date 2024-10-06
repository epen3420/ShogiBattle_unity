using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeObjects : MonoBehaviour
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
    public GameObject NowAllyKoma
    {
        get { return nowAllyKoma; }
    }
    private GameObject nowEnemyKoma;
    public GameObject NowEnemyKoma
    {
        get { return nowEnemyKoma; }
    }


    private void Start()
    {
        SetObjects(0, 0);
    }

    private void SetObjects(int nowAlly, int nowEnemy)
    {
        //盤、駒の設置
        board = Instantiate(prefabBoard, boardPos, boardRotate);
        nowAllyKoma = Instantiate(allyKomas[nowAlly], allyPos, allyRotate);
        nowAllyKoma.GetComponent<InputManager>().IsPlayer = true;
        nowEnemyKoma = Instantiate(enemyKomas[nowEnemy], enemyPos, enemyRotate);
        nowEnemyKoma.GetComponent<InputManager>().IsPlayer = false;
    }
}
