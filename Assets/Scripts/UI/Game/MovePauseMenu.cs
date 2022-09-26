using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePauseMenu : MonoBehaviour
{
    private Vector2 positionOutScreen;
    private Vector2 positionInScreen = Vector2.zero;
    private RectTransform rectTransformPanel;
    private void Start()
    {
        rectTransformPanel = gameObject.GetComponent<RectTransform>();
        positionOutScreen = rectTransformPanel.anchoredPosition;
        PausedHandler.OnPauseGame += MovePausePanel;
        Invaders.WinHandler += MovePausePanel;
    }

    private void MovePausePanel()
    {
        if (PausedHandler.StatePauseGame == true)
            rectTransformPanel.anchoredPosition = positionInScreen;
        else
            rectTransformPanel.anchoredPosition = positionOutScreen;
    }

    private void OnDestroy()
    {
        PausedHandler.OnPauseGame -= MovePausePanel;
        Invaders.WinHandler -= MovePausePanel;
    }
}
