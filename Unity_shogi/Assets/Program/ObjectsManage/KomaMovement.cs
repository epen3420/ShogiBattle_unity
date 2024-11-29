using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KomaMovement : MonoBehaviour, IKomaAction
{
    private Rigidbody rb;
    private bool isDragged = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 停止しているかどうかを確認
        if (isDragged && Mathf.Approximately(rb.linearVelocity.magnitude, 0))
        {
            TurnManager.instance.SwitchTurn(1);
            isDragged = false;
        }
    }

    public void Move(Vector3 moveVector)
    {
        isDragged = true;
        rb.AddForce(moveVector, ForceMode.Impulse);
    }
}
