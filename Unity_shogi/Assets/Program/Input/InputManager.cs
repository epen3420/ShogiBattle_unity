using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private IKomaAction iKomaAction;
    private GameInput gameInput;
    private Vector2 startPos;
    private Vector2 endPos;
    private bool isDragging = false;

    [Header("Dragの倍率")]
    [SerializeField]
    private float dragMultiplier = 0.02f;


    private void Awake()
    {
        iKomaAction = GetComponent<IKomaAction>();
    }

    // 有効化
    private void OnEnable()
    {
        gameInput = new GameInput();
        // Actionのコールバックを登録
        gameInput.Player.OnDrag.started += OnDrag;
        gameInput.Player.OnDrag.performed += OnDrag;
        gameInput.Player.OnDrag.canceled += OnDrag;

        // InputActionを有効化
        // これをしないと入力を受け取れないことに注意
        gameInput?.Enable();
    }

    // 無効化
    private void OnDisable()
    {
        // Actionのコールバックを解除
        gameInput.Player.OnDrag.started -= OnDrag;
        gameInput.Player.OnDrag.performed -= OnDrag;
        gameInput.Player.OnDrag.canceled -= OnDrag;

        // 自身が無効化されるタイミングなどで
        // Actionを無効化する必要がある
        gameInput?.Disable();
    }

    /// <summary>
    /// ドラッグを検知してIKomaActionからMoveを実行
    /// </summary>
    /// <param name="context"></param>
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

            iKomaAction.Move(moveVector);
        }
    }
}
