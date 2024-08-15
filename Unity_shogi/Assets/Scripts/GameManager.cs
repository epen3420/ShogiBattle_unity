using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  private MainSceneManager mainSceneManager;
  private GameObject mainSceneManager_obj;
  private GameObject inputManager_obj;


  void Start()
  {
    mainSceneManager_obj = GameObject.FindWithTag("MainSceneManager");
    mainSceneManager = mainSceneManager_obj.GetComponent<MainSceneManager>();
    inputManager_obj = GameObject.Find("InputManager");
  }

  /* private void StartManager(bool Active)
  {
    mainSceneManager_obj.SetActive(Active);
    inputManager_obj.SetActive(Active);
  } */

  public void RoundEnd(GameObject RoundLooser)
  {
    GameObject AllyKoma = mainSceneManager.Now_ally_Koma;
    GameObject EnemyKoma = mainSceneManager.Now_Enemy_Koma;
    Destroy(mainSceneManager.Board);
    if (RoundLooser == AllyKoma)
    {
      if (mainSceneManager.Now_Enemy == 5)
        Debug.Log("winner" + EnemyKoma);
      if (mainSceneManager.Now_Enemy < 5)
        mainSceneManager.Now_Enemy++;
      if (mainSceneManager.Now_ally > 0)
        mainSceneManager.Now_ally--;
      Destroy(EnemyKoma);
    }
    if (RoundLooser == EnemyKoma)
    {
      if (mainSceneManager.Now_ally == 5)
        Debug.Log("winner" + AllyKoma);
      if (mainSceneManager.Now_ally < 5)
        mainSceneManager.Now_ally++;
      if (mainSceneManager.Now_Enemy > 0)
        mainSceneManager.Now_Enemy--;
      Destroy(AllyKoma);
    }
    mainSceneManager.isPlayerTurn = true;
    mainSceneManager.ObjectsSet();
  }

  /* private void ChangeKoma(GameObject Winner,GameObject Looser)
  {

    if (Winner < 5)
    {
      Winner++;
    }
    if (Looser > 0)
    {
      Looser--;
    }
    if (Winner == 6)
    {
      //GameEnd();
    }

  } */

  //SwitchRound(){}

}
