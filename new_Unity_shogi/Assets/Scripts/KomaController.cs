using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KomaController : MonoBehaviour, IKomaAction
{
    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 moveVector)
    {
        rb.AddForce(moveVector, ForceMode.Impulse);

        Debug.Log($"Koma move vector: {moveVector}");
    }
}
