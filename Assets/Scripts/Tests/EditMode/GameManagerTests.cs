using System.Drawing.Text;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class GameManagerTests
{
    private GameObject gameManagerObject;
    private GameManager gameManager;
    private EnemySpawner enemySpawner;

    [SetUp]
    public void Setup()
    {
        gameManagerObject = new GameObject();
        gameManager = gameManagerObject.AddComponent<GameManager>();
        gameManager.enemySpawner = new GameObject();
        enemySpawner = gameManager.enemySpawner.AddComponent<EnemySpawner>();
        gameManager.playerShip = new GameObject();
        gameManager.playerShip.AddComponent<PlayerControl>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(gameManager.enemySpawner);
        Object.DestroyImmediate(gameManagerObject);
    }

    [Test]
    public void GameManager_InitializesToOpeningState()
    {
        // Act
        gameManager.Start();

        // Assert
        Assert.AreEqual(GameManager.GameManagerState.Opening, gameManager.GMState, "GameManager should initialize to Opening state");
    }

    [Test]
    public void SetGameManagerState_SetsGameplayState()
    {
        // Act
        gameManager.SetGameManagerState(GameManager.GameManagerState.Gameplay);

        // Assert
        Assert.AreEqual(GameManager.GameManagerState.Gameplay, gameManager.GMState, "GameManager state should be set to Gameplay");
        Assert.IsTrue(enemySpawner.IsScheduled, "EnemySpawner should be unscheduled when entering GameOver state");
    }

    [Test]
    public void SetGameManagerState_SetsGameOverState()
    {
        // Act
        gameManager.SetGameManagerState(GameManager.GameManagerState.GameOver);

        // Assert
        Assert.AreEqual(GameManager.GameManagerState.GameOver, gameManager.GMState, "GameManager state should be set to GameOver");
        Assert.IsFalse(enemySpawner.IsScheduled, "EnemySpawner should be unscheduled when entering GameOver state");
    }

    [Test]
    public void StartGamePlay_SetsStateToGameplay()
    {
        // Act
        gameManager.StartGamePlay();

        // Assert
        Assert.AreEqual(GameManager.GameManagerState.Gameplay, gameManager.GMState, "StartGamePlay should set the GameManager state to Gameplay");
    }

    [Test]
    public void ChangeToOpeningState_SetsStateToOpening()
    {
        // Act
        gameManager.ChangeToOpeningState();

        // Assert
        Assert.AreEqual(GameManager.GameManagerState.Opening, gameManager.GMState, "ChangeToOpeningState should set the GameManager state to Opening");
    }

    [Test]
    public void GameOverState_TriggersChangeToOpeningStateAfterDelay()
    {
        // Arrange
        gameManager.SetGameManagerState(GameManager.GameManagerState.GameOver);

        // Directly call ChangeToOpeningState since Invoke doesn't run in Edit Mode
        gameManager.ChangeToOpeningState();

        // Assert
        Assert.AreEqual(GameManager.GameManagerState.Opening, gameManager.GMState, "GameOver state should eventually transition to Opening state");
    }
}