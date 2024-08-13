using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class KomaController : MonoBehaviour
{
    //スクリプトInputManagerを参照するための命名
    private InputManager inputManager;
    //スワイプの強さ
    private float SwipeForceMultiplier = 10f;
    //ゼロ判定用
    private Vector3 ZeroVec = new Vector3(0.0f, 0.0f, 0.0f);
    private bool _CanInput;
    //スワイプ状況
    public bool CanSwipe;

    void Start()
    {
        //スワイプ状況の初期化
        CanSwipe = true;
        //InputManagerの参照
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update()
    {
        _CanInput = inputManager.CanInput;
    }
    public void SwipeKoma(Rigidbody rb)
    {
        Debug.Log("x");
        //ユーザー入力が終わっていなかったら先に行かない
        if (_CanInput) return;
        Debug.Log("ys");
        //InputManagerのスワイプの距離を参照
        Vector3 SwipeDistance = inputManager.GetSwipe();
        //スワイプ距離が0だったら先に行かない
        if (SwipeDistance == ZeroVec) return;
        Debug.Log("z");
        //スワイプの方向の算出
        Vector3 SwipeDirection = -SwipeDistance.normalized;
        //スワイプの力の算出
        float SwipeForce = SwipeDistance.magnitude * SwipeForceMultiplier;
        //Rigidbodyに力を加える
        rb.AddForce(SwipeDirection * SwipeForce, ForceMode.Impulse);
        //スワイプ済み
        CanSwipe = false;
    }
}
