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
  private UIManager uIManager;


  void Start()
  {
    uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    mainSceneManager_obj = GameObject.FindWithTag("MainSceneManager");
    mainSceneManager = mainSceneManager_obj.GetComponent<MainSceneManager>();
    inputManager_obj = GameObject.Find("InputManager");
    uIManager.ButtonTitle.gameObject.SetActive(false);
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
        GameEnd(false);
      if (mainSceneManager.Now_Enemy < 5)
        mainSceneManager.Now_Enemy++;
      if (mainSceneManager.Now_ally > 0)
        mainSceneManager.Now_ally--;
      Destroy(EnemyKoma);
    }
    if (RoundLooser == EnemyKoma)
    {
      if (mainSceneManager.Now_ally == 5)
        GameEnd(true);
      if (mainSceneManager.Now_ally < 5)
        mainSceneManager.Now_ally++;
      if (mainSceneManager.Now_Enemy > 0)
        mainSceneManager.Now_Enemy--;
      Destroy(AllyKoma);
    }
    mainSceneManager.isPlayerTurn = true;
    mainSceneManager.ObjectsSet();
  }

  private void GameEnd(bool Winner)
  {
    inputManager_obj.SetActive(false);
    uIManager.ButtonTitle.gameObject.SetActive(true);
    uIManager.TextTurnInfo.gameObject.SetActive(false);
    uIManager.WinnerInfo(Winner);
    mainSceneManager_obj.SetActive(false);
  }
}
