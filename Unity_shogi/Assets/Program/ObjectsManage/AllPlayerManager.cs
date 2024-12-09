using UnityEngine;
using Cysharp.Threading.Tasks;

public class AllPlayerManager : MonoBehaviour
{
    private PlayerManager[] playerManagers;
    private InputManager[] inputManagers;
    private ITurnChangeable[] turnChangeables;
    private int playerCount = 0;
    private int currentPlayer = -1;
    [SerializeField]
    private BoardManager boardManager;


    private void Start()
    {
        playerCount = PlayerInfoDataBase.instance.playerCount;
    }

    public void Init()
    {
        currentPlayer = -1;
    }

    public async UniTask InstantiateAllPlayerKoma()
    {
        Vector3[] playersPos = boardManager.GenerateCirclePositions(playerCount);
        Quaternion[] playersRot = boardManager.GenerateCircleRotations(playerCount);

        var tasks = new UniTask<GameObject>[playerCount];
        for (int i = 0; i < playerManagers.Length; i++)
        {
            tasks[i] = playerManagers[i].GetNextKomaPrefab();
        }
        var prefabs = await UniTask.WhenAll(tasks);

        for (int i = 0; i < tasks.Length; i++)
        {
            playerManagers[i].InstantiateKoma(prefabs[i], playersPos[i], playersRot[i]);
        }
    }

    public bool KomaIsStopping()
    {
        return turnChangeables[currentPlayer].IsStopping();
    }

    public void TurnChange()
    {
        currentPlayer = (currentPlayer + 1) % playerCount;
        for (int i = 0; i < inputManagers.Length; i++)
        {
            bool isEnable = (currentPlayer == i);
            inputManagers[i].enabled = isEnable;
        }
        currentPlayer++;
    }

    public void SetChildObjects(bool isEnable)
    {
        for (int i = 0; i < playerCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isEnable);
        }

        playerManagers = new PlayerManager[playerCount];
        playerManagers = GetComponentsInChildren<PlayerManager>();
        inputManagers = new InputManager[playerCount];
        inputManagers = GetComponentsInChildren<InputManager>();
        turnChangeables = new ITurnChangeable[playerCount];
        turnChangeables = GetComponentsInChildren<ITurnChangeable>();

        for (int i = 0; i < playerCount; i++)
        {
            playerManagers[i].PlayerDatas = PlayerInfoDataBase.instance.playerDatas[i];
        }
    }
}
