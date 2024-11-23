using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private bool isDragging = false;
    private IKomaAction komaAction;

    [SerializeField]
    private PlayerInputManager playerInputManager;
    [Header("Dragの倍率")]
    [SerializeField]
    private float dragMultiplier = 0.02f;


    private void Awake()
    {
        komaAction = GetComponent<IKomaAction>();
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // クリック開始
            startPos = Mouse.current.position.ReadValue();
            isDragging = true;
        }
        else if (context.canceled && isDragging)
        {
            // クリック終了
            endPos = Mouse.current.position.ReadValue();
            isDragging = false;

            // 方向ベクトル計算
            Vector2 dragVector = -(endPos - startPos) * dragMultiplier;
            Vector3 moveVector = new Vector3(dragVector.x, 0, dragVector.y);

            komaAction.Move(moveVector);
        }
    }
}
