using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSwitcher : MonoBehaviour
{
    private InputManager inputManager;

    [SerializeField] private InitilizeObjects initilizeObjects;
    [SerializeField] private bool isAlly = true;


    private void Update()
    {
        TurnSwitch();
    }

    public void TurnSwitch()
    {
        GameObject allyKoma = initilizeObjects.NowAllyKoma;
        var allyKomaInputManager = allyKoma.GetComponent<InputManager>();
        GameObject enemyKoma = initilizeObjects.NowEnemyKoma;
        var enemyKomaInputManager = enemyKoma.GetComponent<InputManager>();
        if (isAlly)
        {
            allyKomaInputManager.enabled = true;
            enemyKomaInputManager.enabled = false;
        }
        else
        {
            allyKomaInputManager.enabled = false;
            enemyKomaInputManager.enabled = true;
        }
    }
}
