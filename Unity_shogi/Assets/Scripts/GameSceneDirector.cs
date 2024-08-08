using UnityEngine;

public class GameSceneDirector : MonoBehaviour
{
    int boardWidth;
    int boardHeight;
    [SerializeField] GameObject prefabBoard;
    [SerializeField] GameObject prefabOu;
    [SerializeField] GameObject prefabGyoku;
    // Start is called before the first frame update
    void Start()
    {
        //将棋盤設置
        Vector3 pos = new Vector3(0, 0, 0);
        GameObject Board = Instantiate(prefabBoard, pos, Quaternion.identity);

        //駒の初期配置
        Vector3 OnePpos = new Vector3(0, 1, -4);
        GameObject Ou = Instantiate(prefabOu, OnePpos, Quaternion.Euler(0, 180, 0));

        Ou.AddComponent<Rigidbody>();

        Vector3 TwoPpos = new Vector3(0, 1, 4);
        GameObject Gyoku = Instantiate(prefabGyoku, TwoPpos, Quaternion.Euler(0, 0, 0));

        Gyoku.AddComponent<Rigidbody>();
    }



}
