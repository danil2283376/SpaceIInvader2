using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ClickPlay() 
    {
        SceneManager.LoadScene("Game");
    }

    public void ClickExit() 
    {
        Application.Quit();
    }
}
