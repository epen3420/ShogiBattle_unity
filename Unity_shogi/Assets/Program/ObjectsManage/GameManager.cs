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

    private PlayerDatas gameWinner;


    private IEnumerator Start()
    {
        yield return boardManager.InstantiateBoard();

        playersManager.BoardManager = boardManager;

        yield return playersManager.GenerateKomaDictionary();

        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return RoundStarting();

        yield return RoundPlaying();

        yield return RoundEnding();

        gameWinner = playersManager.ObserveGameWinner();
        if (gameWinner != null)
        {
            Debug.Log(gameWinner);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        yield return playersManager.InstantiateKoma();

        turnManager.EnableKomaInput(0);
    }

    private IEnumerator RoundPlaying()
    {
        while (!playersManager.IsDeadAll())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        playersManager.Init();
        playersManager.SetGradeAllKoma();

        yield return null;
    }
}
