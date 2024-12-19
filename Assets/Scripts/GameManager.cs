using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemySpawner;
    public GameObject playerShip;
    public GameObject scoreUIText;
    public GameObject bossShip;
    public GameObject gameTitle;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject SecondbossShip;
    public GameObject Asteroid;

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
                bossShip.GetComponent<bossSpawner>().ResetBoss();
                playButton.SetActive(true);
                gameTitle.SetActive(true);
                gameOver.SetActive(false);
                SecondbossShip.SetActive(false);
                break;

            case GameManagerState.Gameplay:
                SecondbossShip.SetActive(true);
                playButton.SetActive(false);
                gameTitle.SetActive(false);
                scoreUIText.GetComponent<GameScore>().Score = 0;
                playerShip.GetComponent<PlayerControl>().Init();
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                Asteroid.GetComponent<AsteroidSpawner>().ScheduleEnemySpawner();

                // Boss resetelése a játékmenet elején
                bossShip.GetComponent<bossSpawner>().ResetBoss();
                SecondbossShip.GetComponent<SecondBossSpawner>().ResetSecondBoss();

                break;

            case GameManagerState.GameOver:
                gameOver.SetActive(true);
                SecondbossShip.SetActive(false);
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                Asteroid.GetComponent<AsteroidSpawner>().UnscheduleEnemySpawner();
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
