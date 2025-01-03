using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class PlayerControlTests
{
    private GameObject playerObject;
    private PlayerControl playerControl;
    private GameObject explosionPrefab;
    private GameObject gameManagerObject;
    private GameManager gameManager;

    [SetUp]
    public void Setup()
    {
        playerObject = new GameObject();
        playerControl = playerObject.AddComponent<PlayerControl>();

        // Setup default values for testing
        playerControl.speed = 5.0f;
        playerControl.PlayerBulletGo = new GameObject("Bullet");
        playerControl.BulletPosition01 = new GameObject("BulletPosition");

        explosionPrefab = new GameObject();
        playerControl.ExplosionGO = explosionPrefab;

        gameManagerObject = new GameObject();
        gameManager = gameManagerObject.AddComponent<GameManager>();
        gameManager.enemySpawner = new GameObject();
        gameManager.enemySpawner.AddComponent<EnemySpawner>();

        playerControl.GameManagerGO = gameManagerObject;
        
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(playerObject);
    }

    [Test]
    public void Init_SetsPlayerToStartPositionAndMaxLives()
    {
        // Act
        playerControl.Init();

        // Assert
        Assert.AreEqual(new Vector3(0, 0, 0), playerControl.transform.position, "Player should start at position (0,0)");
        Assert.IsTrue(playerObject.activeSelf, "Player should be active after initialization");
    }

    [Test]
    public void Move_PlayerStaysWithinCameraBounds()
    {
        // Arrange
        playerControl.Init();
        Vector2 outsideBounds = new Vector2(100, 100);
        playerControl.transform.position = outsideBounds;

        // Act
        playerControl.Move(new Vector2(1, 1)); // Attempt to move in any direction

        // Assert
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.x -= 0.225f;
        min.x += 0.225f;
        max.y -= 0.225f;
        min.y += 0.225f;

        Vector2 clampedPosition = playerControl.transform.position;
        Assert.IsTrue(clampedPosition.x >= min.x && clampedPosition.x <= max.x, "Player's X position should be within camera bounds");
        Assert.IsTrue(clampedPosition.y >= min.y && clampedPosition.y <= max.y, "Player's Y position should be within camera bounds");
    }

    [Test]
    public void ShootBullet_CreatesBulletAtCorrectPosition()
    {
        // Arrange
        playerControl.Init();
        Vector3 expectedBulletPosition = playerControl.BulletPosition01.transform.position;

        // Act
        playerControl.ShootBullet();
        GameObject bullet = GameObject.Find("Bullet(Clone)");

        // Assert
        Assert.IsNotNull(bullet, "Bullet should be instantiated when shooting");
        Assert.AreEqual(expectedBulletPosition, bullet.transform.position, "Bullet should be instantiated at BulletPosition01's position");

        // Clean up
        Object.DestroyImmediate(bullet);
    }

    [Test]
    public void OnTriggerEnter2D_CollidesWithEnemy_DecrementsLivesAndPlaysExplosion()
    {
        playerControl.Init();
        int initialLives = playerControl.GetType().GetField("lives", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(playerControl) as int? ?? 0;

        GameObject enemyObject = new GameObject();
        enemyObject.tag = "EnemyShipTag";
        enemyObject.AddComponent<BoxCollider2D>();

        playerControl.OnTriggerEnter2D(enemyObject.GetComponent<Collider2D>());

        var instantiatedExplosion = GameObject.Find(explosionPrefab.name + "(Clone)");
        int currentLives = playerControl.GetType().GetField("lives", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(playerControl) as int? ?? 0;

        Assert.IsNotNull(instantiatedExplosion, "Explosion should be instantiated upon collision with enemy.");
        Assert.AreEqual(initialLives - 1, currentLives, "Lives should decrement upon collision with enemy.");

        Object.DestroyImmediate(instantiatedExplosion);
        Object.DestroyImmediate(enemyObject);
    }

    [Test]
    public void OnTriggerEnter2D_LosesAllLives_SetsGameOverState()
    {
        playerControl.Init();
        playerControl.GetType().GetField("lives", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(playerControl, 1);

        GameObject enemyObject = new GameObject();
        enemyObject.tag = "EnemyShipTag";
        enemyObject.AddComponent<BoxCollider2D>();

        playerControl.OnTriggerEnter2D(enemyObject.GetComponent<Collider2D>());

        Assert.IsFalse(playerObject.activeSelf, "Player should be inactive when lives reach zero.");
        Assert.AreEqual(GameManager.GameManagerState.GameOver, gameManager.GMState, "Game state should be set to GameOver when player loses all lives.");

        Object.DestroyImmediate(enemyObject);
    }
}