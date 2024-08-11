using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    //Camera,Rigidbodyの取得
    private Camera mainCamera;
    public Rigidbody rb;
    //prefabの盤、駒を参照
    [SerializeField] GameObject Board;
    [SerializeField] GameObject[] AllyKomas;
    [SerializeField] GameObject[] EnemyKomas;
    //駒の入れ替えのため駒の状況
    private int Now_ally;
    private int Now_Enemy;
    //オブジェクトの位置参照
    private Vector3 BoardPos = new Vector3(0, 0, 0);
    private Vector3 AllyPos = new Vector3(0.0f, 1.0f, -4.0f);
    private Vector3 EnemyPos = new Vector3(0.0f, 1.0f, 4.0f);
    //どっちのターンか
    private bool Player_which = true;


    void Start()
    {
        mainCamera = Camera.main;
        //初期設置
        ObjectsSet();
    }


    private void ObjectsSet()
    {
        //盤、駒の設置
        Board = Instantiate(Board, BoardPos, Quaternion.Euler(0, 180, 0));
        AllyKomas[Now_ally] = Instantiate(AllyKomas[Now_ally], AllyPos, Quaternion.Euler(0, 180, 0));
        EnemyKomas[Now_Enemy] = Instantiate(EnemyKomas[Now_Enemy], EnemyPos, Quaternion.identity);
        AddComponent();
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
        }
        else
        {
            //2Pカメラ設定
            mainCamera.gameObject.transform.position = new Vector3(0.0f, 8.0f, 3.5f);
            mainCamera.gameObject.transform.rotation = Quaternion.Euler(70f, 180f, 0f);
            //2P駒のRigidbody
            rb = EnemyKomas[Now_Enemy].GetComponent<Rigidbody>();
        }
    }

    private void AddComponent()
    {
        AllyKomas[Now_ally].AddComponent<KomaController>();
        EnemyKomas[Now_Enemy].AddComponent<KomaController>();
    }



    //アタッチされているオブジェクトにColiderがついている。
    //範囲外にオブジェクト（駒）が出たら検知
    private void OnTriggerExit(Collider DeadKoma)
    {
        //範囲外に出たオブジェクトを削除
        Destroy(DeadKoma);
    }
}
