using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    internal static GameState Instance;
    
    [SerializeField] internal UnityEvent onStopGame;
    [SerializeField] internal UnityEvent onStartGame;
    [SerializeField] internal UnityEvent onPauseGame;
    [SerializeField] internal UnityEvent onUnPauseGame;
    [SerializeField] internal UnityEvent onExitGame;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SnakeManager.currentState != SnakeManager.CurrentState.Stop)
        {
            if (Time.timeScale == 0)
            {
                UnPauseGame();
            }
            else PauseGame();
        }
    }

    public void StartGame()
    {
        SnakeManager.currentState = SnakeManager.CurrentState.Start;
        onStartGame?.Invoke();
        Debug.Log("Game start...");
    }
    public void StopGame()
    {
        SnakeManager.currentState = SnakeManager.CurrentState.Stop;
        onStopGame?.Invoke();
        Debug.Log("Game stop...");
    }
    public void PauseGame()
    {
        SnakeManager.currentState = SnakeManager.CurrentState.Pause;
        onPauseGame?.Invoke();
        Debug.Log("Game pause...");
    }

    public void UnPauseGame()
    {
        SnakeManager.currentState = SnakeManager.CurrentState.Start;
        onUnPauseGame?.Invoke();
        Debug.Log("Game unpause...");
    }

    public void ExitGame()
    {
        onExitGame?.Invoke();
        Debug.Log("Game exit...");
        Application.Quit();
        Thread.CurrentThread.Abort();
    }
}
