using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneDirector : MonoBehaviour
{
    int boardWidth;
    int boardHeight;
    [SerializeField] GameObject prefabTile;
    // Start is called before the first frame update
    void Start()
    {
        boardWidth = 9;
        boardHeight = 9;
        for (int i = 0; i < boardWidth; i++)
        {
            for (int j = 0; j < boardHeight; j++)
            {
                float x = i - boardWidth / 2;
                float y = j - boardHeight / 2;

                Vector3 pos = new Vector3(x, 0, y);
                GameObject tile = Instantiate(prefabTile, pos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
