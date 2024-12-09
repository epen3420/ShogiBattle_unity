using Cysharp.Threading.Tasks;
using UnityEngine;

public class RoundStartState : IGameState
{
    public async UniTask EnterState(GameManager gameManager)
    {
        Debug.Log("Round Starting...");
        await gameManager.InitRound();
        gameManager.DisplayRoundInfo(); // ラウンド情報を表示
        await UniTask.Delay(1000); // 簡易的な待機処理

        gameManager.SetState(new RoundPlayState()); // 次の状態へ
    }

    public UniTask ExitState(GameManager gameManager)
    {
        Debug.Log("Exiting Round Start...");
        return UniTask.CompletedTask;
    }
}
