using System.Collections.Generic;
using UnityEngine;

public class InitObjects : MonoBehaviour
{
    private List<KomaObjectDatas> allyKomaObjects = new List<KomaObjectDatas>();
    private int[] allyKomas = { 0, 2, 4, 6, 7, 8 };
    private List<KomaObjectDatas> enemyKomaObjects = new List<KomaObjectDatas>();
    private int[] enemyKomas = { 1, 3, 5, 6, 7, 8 };

    [SerializeField]
    private KomaDataBase komaDatas;


    private void Start()
    {
        var komaObjects = komaDatas.KomaDatas();
        for (int i = 0; i < 6; i++)
        {
            int allyNum = allyKomas[i];
            allyKomaObjects.Add(komaObjects[allyNum]);
            int enemyNum = enemyKomas[i];
            enemyKomaObjects.Add(komaObjects[enemyNum]);
        }

        var currentAllyKoma = Instantiate(allyKomaObjects[0].prefab);
        var currentEnemyKoma = Instantiate(enemyKomaObjects[0].prefab);
    }


}
