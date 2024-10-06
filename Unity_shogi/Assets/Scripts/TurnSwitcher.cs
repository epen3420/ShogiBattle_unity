using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSwitcher : MonoBehaviour
{
    [SerializeField] private InitializeObjects initializeObjects;


    public void TurnSwitch(bool isAlly)
    {
        GameObject allyKoma = initializeObjects.NowAllyKoma;
        var allyKomaInputManager = allyKoma.GetComponent<InputManager>();
        GameObject enemyKoma = initializeObjects.NowEnemyKoma;
        var enemyKomaInputManager = enemyKoma.GetComponent<InputManager>();
        allyKomaInputManager.enabled = isAlly;
        enemyKomaInputManager.enabled = !isAlly;
    }
}
