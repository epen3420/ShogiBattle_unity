using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  private bool RoundWinner;
  private int Next_Koma;
  private MainSceneManager mainSceneManager;


  void Start()
  {
    mainSceneManager = GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>();
    mainSceneManager.Now_ally = Next_Koma;
  }

  void Update()
  {
    /* if ()
    {
      RoundWinner = mainSceneManager.isPlayerTurn;
      Debug.Log(RoundWinner);
      StartCoroutine(Gameloop());
    } */
  }

  private IEnumerator Gameloop()
  {
    /* mainSceneManager.RoundProcess = true;
    yield return new WaitForSeconds(0.5f);
    IsWinner();
    yield return new WaitForSeconds(0.5f); */
    yield return new WaitForSeconds(0.5f);
    Next_Koma++;
    Reset();
  }

  /*   private void IsWinner()
    {
      RoundWinner = !mainSceneManager.isPlayerTurn;
      if (RoundWinner)
      {
        mainSceneManager.Now_ally++;
      }
      else
      {
        mainSceneManager.Now_Enemy++;
      }
    } */

  private void Reset()
  {
    SceneManager.sceneLoaded += GameSceneLoaded;

    SceneManager.LoadScene("MainScene");
  }

  private void GameSceneLoaded(Scene next, LoadSceneMode mode)
  {
    var gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    gameManager.Next_Koma = Next_Koma;
    SceneManager.sceneLoaded -= GameSceneLoaded;
  }
}
