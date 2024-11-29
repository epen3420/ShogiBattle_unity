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


    private IEnumerator Start()
    {
        yield return boardManager.InstantiateBoard();

        komaManager.SetBoardManager = boardManager;
        yield return komaManager.InitKomaManager();

        yield return komaManager.InstantiateKoma();
        turnManager.EnableKomaInput(0);
    }
}
