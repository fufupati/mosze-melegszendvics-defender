using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class EnemyControlTests
{
    private GameObject enemyObject;
    private EnemyControl enemyControl;

    [SetUp]
    public void Setup()
    {
        enemyObject = new GameObject();
        enemyControl = enemyObject.AddComponent<EnemyControl>();

        // Ensure the main camera is present for viewport calculations
        if (Camera.main == null)
        {
            new GameObject("MainCamera", typeof(Camera)).tag = "MainCamera";
        }
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(enemyObject);
    }

    [Test]
    public void EnemyControl_InitializesWithCorrectSpeed()
    {
        // Act
        enemyControl.Start();

        // Assert
        Assert.AreEqual(2f, enemyControl.GetType().GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(enemyControl),
            "EnemyControl should initialize speed to 2f");
    }

    [Test]
    public void EnemyControl_MovesDownwardInUpdate()
    {
        // Arrange
        enemyControl.Start();
        Vector2 initialPosition = enemyControl.transform.position;

        // Act
        enemyControl.Update();

        // Assert
        Assert.Less(enemyControl.transform.position.y, initialPosition.y, "EnemyControl should move downwards in Update");
    }

    [Test]
    public void EnemyControl_DestroySelfWhenOutOfScreenBounds()
    {
        // Arrange
        enemyControl.Start();
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        enemyControl.transform.position = new Vector2(0, min.y - 1); // Position below the lower screen bound

        // Act
        enemyControl.Update(); // Update to trigger the destruction logic

        // Assert
        Assert.IsTrue(enemyControl == null || enemyControl.gameObject == null, "EnemyControl should be destroyed when it goes out of screen bounds");
    }
}