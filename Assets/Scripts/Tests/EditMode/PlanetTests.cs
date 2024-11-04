using NUnit.Framework;
using UnityEngine;

public class PlanetTests
{
    private GameObject planetObject;
    private Planet planet;
    private SpriteRenderer spriteRenderer;

    [SetUp]
    public void SetUp()
    {
        planetObject = new GameObject();
        planet = planetObject.AddComponent<Planet>();
        spriteRenderer = planetObject.AddComponent<SpriteRenderer>();
        // Use a 4x4 texture and set Rect to 4x4 to match the texture size
        Texture2D texture = new Texture2D(4, 4);
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, 4, 4), Vector2.zero);


        planet.speed = 5f;
        planet.Awake(); // Manually call Awake to initialize min and max
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(planetObject);
    }

    [Test]
    public void Update_WhenMoving_ChangesPosition()
    {
        // Arrange
        planet.isMoving = true;
        Vector2 initialPosition = planetObject.transform.position;

        // Act
        planet.Update();

        // Assert
        Assert.AreNotEqual(initialPosition, planetObject.transform.position, "Position should change when planet is moving.");
    }

    [Test]
    public void Update_WhenBelowMinY_StopsMoving()
    {
        // Arrange
        planet.isMoving = true;
        planetObject.transform.position = new Vector2(0, planet.min.y - 1); // Below min Y

        // Act
        planet.Update();

        // Assert
        Assert.IsFalse(planet.isMoving, "Planet should stop moving when it goes below min Y.");
    }

    [Test]
    public void ResetPosition_SetsPositionToTop()
    {
        // Act
        planet.ResetPosition();

        // Assert
        Assert.AreEqual(planet.max.y, planetObject.transform.position.y, "Y position should be reset to max Y.");
        Assert.IsTrue(planetObject.transform.position.x >= planet.min.x && planetObject.transform.position.x <= planet.max.x, "X position should be within min and max bounds.");
    }
}