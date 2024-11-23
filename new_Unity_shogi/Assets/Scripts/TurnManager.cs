using UnityEngine;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    private PlayerInput[] playerInputs;
    private int currentPlayer = 0;
    private int playerCount = 0;

    private void Start()
    {
        playerInputs = new PlayerInput[playerCount];
        playerInputs = GetComponentsInChildren<PlayerInput>(true);
        for (int i = 0; i < playerInputs.Length; i++)
        {
            Debug.Log(playerInputs[i].gameObject.name);
        }
        playerCount = PlayerInfoDataBase.instance.playerCount;
    }

    public void SwitchTurn(int skipCount)
    {
        currentPlayer = (currentPlayer + skipCount) % playerCount;
        playerInputs[currentPlayer].enabled = true;
    }
}
