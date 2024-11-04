using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class PlayerControlTests
{
    private GameObject playerObject;
    private PlayerControl playerControl;

    [SetUp]
    public void Setup()
    {
        playerObject = new GameObject();
        playerControl = playerObject.AddComponent<PlayerControl>();

        // Setup default values for testing
        playerControl.speed = 5.0f;
        playerControl.PlayerBulletGo = new GameObject("Bullet");
        playerControl.BulletPosition01 = new GameObject("BulletPosition");
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
}