using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject bossShip;

    public GameObject SecondbossShip;
    public GameObject GameOverGO;
    public GameObject scoreUITextGo;
    public GameObject TimeCounterGO;
    public GameObject GameTitleGO;
    public GameObject Upgradebutton1;
    public GameObject Upgradebutton2;

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
        GMState = GameManagerState.Opening;
        UpdateGameManagerState();
    }

    public void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                GameTitleGO.SetActive(true);
                Upgradebutton1.SetActive(true);
                 Upgradebutton2.SetActive(true);
                 SecondbossShip.SetActive(false);
                 bossShip.SetActive(false);

                break;

            case GameManagerState.Gameplay:
              SecondbossShip.SetActive(true);
                 bossShip.SetActive(true);
                scoreUITextGo.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                playerShip.GetComponent<PlayerControl>().Init();
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                Asteroid.GetComponent<AsteroidSpawner>().ScheduleEnemySpawner();
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
                GameTitleGO.SetActive(false);
                Upgradebutton1.SetActive(false);
                 Upgradebutton2.SetActive(false);

                
                bossShip.GetComponent<bossSpawner>().ResetBoss();
                SecondbossShip.GetComponent<SecondBossSpawner>().ResetSecondBoss();

                break;

            case GameManagerState.GameOver:
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                Asteroid.GetComponent<AsteroidSpawner>().UnscheduleEnemySpawner();
                SecondbossShip.SetActive(false);
                 bossShip.SetActive(false);
                GameOverGO.SetActive(true);
                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();

        if (state == GameManagerState.Opening)
        {
            bossShip.GetComponent<bossSpawner>().ResetBoss();
            SecondbossShip.GetComponent<SecondBossSpawner>().ResetSecondBoss();
        }
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
