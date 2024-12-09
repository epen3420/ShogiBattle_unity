using Cysharp.Threading.Tasks;
using UnityEngine;

public class RoundPlayState : IGameState
{
    public async UniTask EnterState(GameManager gameManager)
    {
        Debug.Log("Round Playing...");
        await gameManager.StartRound(); // 駒の動作などを管理
        await UniTask.WaitUntil(() => gameManager.CanTurnChange());
        gameManager.ExecuteTurnChange();
        await UniTask.WaitUntil(() => gameManager.IsPlayerWin());
    }

    public UniTask ExitState(GameManager gameManager)
    {
        Debug.Log("Exiting Round Play...");
        return UniTask.CompletedTask;
    }
}
