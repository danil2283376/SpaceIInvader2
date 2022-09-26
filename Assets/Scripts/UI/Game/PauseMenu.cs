using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseGamePanel;
    [SerializeField] private GameObject _buttonPlayGame;
    //[SerializeField] private GameObject _healthBar;
    [SerializeField] private Text _winOrLoseText;

    private void Start()
    {
        HealthPlayer.DiePlayer += PlayerLoseText;
        Invaders.WinHandler += PlayerWinText;
    }

    public void PlayGame()
    {
        PausedHandler.ChangeStatePaused();
    }

    public void ExitMainMenu()
    {
        PausedHandler.ChangeStatePaused();
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void PlayerLoseText() 
    {
        _winOrLoseText.text = "Lose :(";
        _buttonPlayGame.SetActive(false);
    }

    private void PlayerWinText()
    {
        _winOrLoseText.text = "Win :)";
        _buttonPlayGame.SetActive(false);
    }
    private void OnDestroy()
    {
        HealthPlayer.DiePlayer -= PlayerLoseText;
        Invaders.WinHandler -= PlayerWinText;
    }
}
