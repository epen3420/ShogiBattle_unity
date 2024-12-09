using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KomaMovement : MonoBehaviour, IKomaAction, ITurnChangeable
{
    private Rigidbody rb;
    private bool isMoving = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 停止しているかどうかを確認
        if (isMoving && Mathf.Approximately(rb.linearVelocity.magnitude, 0))
        {
            isMoving = false;
        }
    }

    public bool IsStopping()
    {
        return !isMoving;
    }

    public void Move(Vector3 moveVector)
    {
        isMoving = true;
        rb.AddForce(moveVector, ForceMode.Impulse);
    }
}
