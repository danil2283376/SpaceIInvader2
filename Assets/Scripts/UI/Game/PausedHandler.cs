using UnityEngine;

public class PausedHandler : MonoBehaviour
{
    public delegate void PauseGameHandler();
    public static event PauseGameHandler OnPauseGame;
    [HideInInspector] public static bool StatePauseGame = false;
    //[SerializeField] private GameObject _healthBar;

    private void Start()
    {
        //OnPauseGame += SetTimeScale;
        HealthPlayer.DiePlayer += ChangeStatePaused;
        Invaders.WinHandler += ChangeStatePaused;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            ChangeStatePaused();
    }

    public static void ChangeStatePaused() 
    {
        SetTimeScale();
        StatePauseGame = !StatePauseGame;
        OnPauseGame();
    }

    private static void SetTimeScale()
    {
        if (StatePauseGame)
        {
            Time.timeScale = 1f;
        }
        else
            Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        HealthPlayer.DiePlayer -= ChangeStatePaused;
        Invaders.WinHandler -= ChangeStatePaused;
    }
}
