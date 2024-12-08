using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private IGameState currentState;

    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    private AllPlayerManager allPlayerManager;

    public async void Start()
    {
        SetState(new RoundStartState());
        await currentState.EnterState(this);
    }

    public void SetState(IGameState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;

        currentState.EnterState(this);
    }

    // 状態遷移中に呼ばれるメソッド
    public void DisplayRoundInfo() { /* ラウンド情報表示 */ }
    public async UniTask InitRound()
    {
        await boardManager.InstantiateBoard();
        allPlayerManager.SetChildObjects(true);
        await allPlayerManager.InstantiateAllPlayerKoma();
        allPlayerManager.TurnChange();
        allPlayerManager.Init();
    }
    public async UniTask StartRound()
    {
    }
    public bool IsPlayerWin() { /* 勝敗判定 */ return false; }
    public bool IsGameOver() { /* ゲーム終了判定 */ return false; }
    public void AdvancePiece() { /* 駒を進める */ }
    public void RetreatPiece() { /* 駒を戻す */ }
}
