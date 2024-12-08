using Cysharp.Threading.Tasks;
using UnityEngine;

public class RoundPlayState : IGameState
{
    public async UniTask EnterState(GameManager gameManager)
    {
        Debug.Log("Round Playing...");
        await gameManager.StartRound(); // 駒の動作などを管理

        // 勝敗判定
        if (gameManager.IsPlayerWin())
        {
            gameManager.AdvancePiece(); // 次の駒に進む
            gameManager.SetState(new RoundEndState());
        }
        else
        {
            gameManager.RetreatPiece(); // 駒を戻す
            gameManager.SetState(new RoundEndState());
        }
    }

    public UniTask ExitState(GameManager gameManager)
    {
        Debug.Log("Exiting Round Play...");
        return UniTask.CompletedTask;
    }
}
