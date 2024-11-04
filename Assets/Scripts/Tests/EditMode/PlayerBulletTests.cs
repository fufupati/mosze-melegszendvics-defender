using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

[TestFixture]
public class PlayerBulletTests
{
    private GameObject bulletObject;
    private PlayerBullet playerBullet;

    [SetUp]
    public void Setup()
    {
        bulletObject = new GameObject();
        playerBullet = bulletObject.AddComponent<PlayerBullet>();

        // Set the main camera (required for viewport calculations)
        if (Camera.main == null)
        {
            new GameObject("MainCamera", typeof(Camera)).tag = "MainCamera";
        }
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(bulletObject);
    }

    [Test]
    public void BulletMovesUpwardsInUpdate()
    {
        // Arrange
        playerBullet.Start();  // Initialize speed
        Vector2 initialPosition = playerBullet.transform.position;

        // Act
        playerBullet.Update();

        // Assert
        Assert.Greater(playerBullet.transform.position.y, initialPosition.y, "Bullet should move upwards in Update");
    }

    [Test]
    public void BulletDestroysItselfWhenOutOfScreenBounds()
    {
        // Arrange
        playerBullet.Start(); // Initialize speed

        // Set the bullet’s position just beyond the upper screen boundary
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        playerBullet.transform.position = new Vector2(0, max.y + 0.1f);  // Set out of bounds

        // Act
        playerBullet.Update(); // Trigger the boundary check and destroy logic

        // Assert
        Assert.IsTrue(playerBullet == null || playerBullet.gameObject == null, "Bullet should be destroyed when it goes out of the upper screen bounds");
    }
}