using UnityEngine;
using Cysharp.Threading.Tasks;

public class AllPlayerManager : MonoBehaviour
{
    private PlayerInfoDataBase playerInfoDB;
    private PlayerManager[] playerManagers;
    private int playerCount = 0;

    [SerializeField]
    private BoardManager boardManager;


    private void Start()
    {
        playerInfoDB = PlayerInfoDataBase.instance;
        playerCount = playerInfoDB.playerCount;
        SetChildObjects(true);
        playerManagers = new PlayerManager[playerCount];
        playerManagers = GetComponentsInChildren<PlayerManager>();
    }

    public async UniTask InstantiateAllPlayerKoma()
    {
        Vector3[] playersPos = boardManager.GenerateCirclePositions(playerCount);
        Quaternion[] playersRot = boardManager.GenerateCircleRotations(playerCount);

        var tasks = new UniTask<GameObject>[playerCount];
        for (int i = 0; i < playerCount; i++)
        {
            tasks[i] = playerManagers[i].GetNextKomaPrefab();
        }
        var prefabs = await UniTask.WhenAll(tasks);

        for (int i = 0; i < tasks.Length; i++)
        {
            playerManagers[i].InstantiateKoma(prefabs[i], playersPos[i], playersRot[i]);
        }
    }

    private void SetChildObjects(bool isEnable)
    {
        for (int i = 0; i < playerCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isEnable);
        }
    }
}
