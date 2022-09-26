using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    private static GameObject _panelCurrentScoreUI;
    private void Start()
    {
        _panelCurrentScoreUI = gameObject;
    }

    public static void ChangeScore(int indexInvader, int score)
    {
        Transform invaderUI = _panelCurrentScoreUI.transform.GetChild(indexInvader);
        Text textUI = invaderUI.GetChild(1).GetComponent<Text>();

        int currentScore = Convert.ToInt32(textUI.text);
        textUI.text = (currentScore + score).ToString();
    }
}
