using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class KomaController : MonoBehaviour
{
    //スワイプの強さ
    private float SwipeForceMultiplier = 10f;
    //アタッチ先のRigidbody参照
    private Rigidbody rb;


    void Start()
    {
        //Rigidbodyの取得
        rb = this.GetComponent<Rigidbody>();
    }

    public void SwipeKoma(Vector3 SwipeDistance)
    {
        //スワイプの方向の算出
        Vector3 SwipeDirection = -SwipeDistance.normalized;
        //スワイプの力の算出
        float SwipeForce = SwipeDistance.magnitude * SwipeForceMultiplier;
        //Rigidbodyに力を加える
        rb.AddForce(SwipeDirection * SwipeForce, ForceMode.Impulse);
    }
}
