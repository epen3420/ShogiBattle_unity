using Cysharp.Threading.Tasks;
using UnityEngine;

public class RoundEndState : IGameState
{
    public async UniTask EnterState(GameManager gameManager)
    {
        Debug.Log("Round Ending...");
        await UniTask.Delay(1000); // 簡易的な待機処理

        if (gameManager.IsGameOver())
        {
            Debug.Log("Game Over!"); // ゲーム終了処理
        }
        else
        {
            gameManager.SetState(new RoundStartState()); // 次のラウンドへ
        }
    }

    public UniTask ExitState(GameManager gameManager)
    {
        Debug.Log("Exiting Round End...");
        return UniTask.CompletedTask;
    }
}
