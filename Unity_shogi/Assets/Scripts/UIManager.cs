using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text TextTurnInfo;


    public void TurnInfo(bool isTurn)
    {
        if (isTurn)
        {
            TextTurnInfo.text = "1Pの番です";
        }
        else { TextTurnInfo.text = "2Pの番です"; }
    }
}
