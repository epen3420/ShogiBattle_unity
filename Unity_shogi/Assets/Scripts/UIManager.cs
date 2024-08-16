using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Text TextTurnInfo;
    [SerializeField] public Text TextWinnerInfo;
    [SerializeField] public Button ButtonTitle;


    public void TurnInfo(bool isTurn)
    {
        if (isTurn)
        {
            TextTurnInfo.text = "1Pの番です";
        }
        else { TextTurnInfo.text = "2Pの番です"; }
    }
    public void WinnerInfo(bool isWinner)
    {
        TextWinnerInfo.gameObject.SetActive(true);
        if (isWinner)
        {
            TextWinnerInfo.text = "1Pの勝利";
        }
        else
        {

            TextWinnerInfo.text = "2Pの勝利";
        }
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

}
