using Cysharp.Threading.Tasks;

public interface IGameState
{
    UniTask EnterState(GameManager gameManager);
    UniTask ExitState(GameManager gameManager);
}
