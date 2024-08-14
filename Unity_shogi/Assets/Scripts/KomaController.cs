using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class KomaController : MonoBehaviour
{
    //スクリプトInputManagerを参照するための命名
    private InputManager inputManager;
    //スワイプ状況
    public bool CanSwipe;
    //スワイプの強さ
    private float SwipeForceMultiplier = 10f;


    void Start()
    {
        CanSwipe = true;
        //InputManagerの参照
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    public void SwipeKoma(Rigidbody rb)
    {
        //InputManagerのスワイプの距離を参照
        Vector3 SwipeDistance = inputManager.SwipeMouse();
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
