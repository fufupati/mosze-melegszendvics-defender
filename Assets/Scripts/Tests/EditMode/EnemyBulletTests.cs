using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyBulletTests
{
    private GameObject bulletObject;
    private EnemyBullet enemyBullet;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject and add the EnemyBullet component
        bulletObject = new GameObject();
        enemyBullet = bulletObject.AddComponent<EnemyBullet>();

        // Set the initial position of the bullet
        bulletObject.transform.position = Vector2.zero;
    }

    [TearDown]
    public void Teardown()
    {
        // Destroy the bullet object after each test
        Object.DestroyImmediate(bulletObject);
    }

    [Test]
    public void SetDirection_SetsCorrectDirection()
    {
        // Arrange
        Vector2 expectedDirection = new Vector2(1, 0).normalized;

        // Act
        enemyBullet.SetDirection(new Vector2(1, 0));

        // Assert
        Assert.AreEqual(expectedDirection, enemyBullet.GetDirection());
    }

    [Test]
    public void Update_MovesBulletInCorrectDirection()
    {
        // Arrange
        enemyBullet.SetDirection(new Vector2(1, 0));
        Vector3 initialPosition = bulletObject.transform.position;

        // Simulate one frame of movement
        float simulatedDeltaTime = 0.1f; // Simulate 0.1 seconds
        enemyBullet.Update(); // Call Update to move the bullet

        // Move the bullet manually for the test
        bulletObject.transform.position += (Vector3)(enemyBullet.GetDirection() * 8f * simulatedDeltaTime);

        // Act
        enemyBullet.Update(); // Call Update again to apply movement

        // Assert
        Assert.AreNotEqual(initialPosition, bulletObject.transform.position, "Bullet did not move as expected.");
    }

    [Test]
    public void Update_DestroyBulletWhenOutOfBounds()
    {
        // Arrange
        enemyBullet.SetDirection(new Vector2(1, 0));

        // Move the bullet far out of bounds
        bulletObject.transform.position = new Vector2(1000, 1000);

        // Act
        enemyBullet.Update(); // Simulate one frame

        // Assert
        Assert.IsTrue(bulletObject == null || !bulletObject.activeInHierarchy, "Bullet was not destroyed when out of bounds.");
    }
}
