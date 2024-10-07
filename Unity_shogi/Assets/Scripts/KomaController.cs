using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaController : MonoBehaviour/* , IGradeKoma */
{
    private Rigidbody rb;
    private float hitForceMultiplier = 10f; // スワイプの強さ

    [SerializeField]
    private int komaGrade = 0;


    private void Start()
    {
        //Rigidbodyの取得
        rb = GetComponent<Rigidbody>();
    }

    public void HitKoma(Vector3 distance)
    {
        //スワイプの方向の算出
        Vector3 hitDirection = -distance.normalized;
        //スワイプの力の算出
        float hitForce = distance.magnitude * hitForceMultiplier;
        //Rigidbodyに力を加える
        rb.AddForce(hitDirection * hitForce, ForceMode.Impulse);
    }

    /*     public void UpGradeKoma(int upGradeNum)
        {
            if (komaGrade < 6)
                komaGrade += upGradeNum;
        }

        public void DownGradeKoma(int downGradeNum)
        {
            if (komaGrade > 0)
                komaGrade -= downGradeNum;
        } */
}
