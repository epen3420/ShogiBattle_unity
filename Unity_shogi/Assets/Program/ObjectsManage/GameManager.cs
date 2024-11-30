using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    private KomaManager komaManager;
    [SerializeField]
    private TurnManager turnManager;
    [SerializeField]
    private PlayersManager playersManager;


    private IEnumerator Start()
    {
        yield return boardManager.InstantiateBoard();

        // komaManager.SetBoardManager = boardManager;
        playersManager.BoardManager = boardManager;
        // yield return komaManager.InitKomaManager();
        yield return playersManager.InitKoma();

        // yield return komaManager.InstantiateKoma();
        yield return playersManager.InstantiateKoma();
        turnManager.EnableKomaInput(0);
    }
}
