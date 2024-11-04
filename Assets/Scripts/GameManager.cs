using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemySpawner;
    public GameObject playerShip;
    public GameObject scoreUIText;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    public GameManagerState GMState;

    public void Start()
    {
        UpdateGameManagerState();
    }

    public void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                break;

            case GameManagerState.Gameplay:
                scoreUIText.GetComponent<GameScore>().Score = 0;
                playerShip.GetComponent<PlayerControl>().Init();
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                break;

            case GameManagerState.GameOver:
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
