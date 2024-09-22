using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    private Rigidbody rb;
    private float swipeForceMultiplier = 10f;

    [SerializeField] private int komaGrade = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 swipeDistance)
    {
        //スワイプの方向の算出
        Vector3 swipeDirection = -swipeDistance.normalized;
        //スワイプの力の算出
        float swipeForce = swipeDistance.magnitude * swipeForceMultiplier;
        //Rigidbodyに力を加える
        rb.AddForce(swipeDirection * swipeForce, ForceMode.Impulse);
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
