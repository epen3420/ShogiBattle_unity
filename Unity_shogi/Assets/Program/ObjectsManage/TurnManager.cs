using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private int currentPlayer = 0;


    /// <summary>
    /// playerKoma以外のInputManagerを無効にする関数
    /// </summary>
    /// <param name="playerKoma"></param>
    public void EnableKomaInput(int playerKoma)
    {
        var playerInputs = GetComponentsInChildren<InputManager>(true);
        for (int i = 0; i < playerInputs.Length; i++)
        {
            if (playerKoma == i)
            {
                playerInputs[i].enabled = true;
            }
            else
            {
                playerInputs[i].enabled = false;
            }
        }
    }

    public void SwitchTurn(int skipCount)
    {
        int playerCount = PlayerInfoDataBase.instance.playerCount;
        currentPlayer = (currentPlayer + skipCount) % playerCount;
        EnableKomaInput(currentPlayer);
    }
}
