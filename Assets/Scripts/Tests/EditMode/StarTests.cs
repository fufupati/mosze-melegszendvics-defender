using NUnit.Framework;
using UnityEngine;

public class StarTests
{
    private GameObject starObject;
    private Star star;

    [SetUp]
    public void SetUp()
    {
        starObject = new GameObject();
        star = starObject.AddComponent<Star>();

        // Set a default speed
        star.speed = 5f;

        // Initialize the camera with a mock position to avoid NullReferenceException
        if (Camera.main == null)
        {
            Camera camera = new GameObject("Main Camera").AddComponent<Camera>();
            camera.tag = "MainCamera";
        }
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(starObject);

        // Clean up any mock camera
        var camera = Camera.main;
        if (camera != null)
        {
            Object.DestroyImmediate(camera.gameObject);
        }
    }

    [Test]
    public void Update_MovesStarUpwards()
    {
        Vector2 initialPosition = starObject.transform.position;

        star.Update();

        Assert.Greater(starObject.transform.position.y, initialPosition.y, "The star should move upwards each update.");
    }

    [Test]
    public void Update_RepositionsStarAtTop_WhenBelowMinY()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Move the star below the minimum Y position
        starObject.transform.position = new Vector2(0, min.y - 1);

        star.Update();

        Assert.AreEqual(max.y, starObject.transform.position.y, "The star should reset to the top of the screen when below min Y.");
        Assert.IsTrue(starObject.transform.position.x >= min.x && starObject.transform.position.x <= max.x, "The X position should be within the screen bounds after repositioning.");
    }
}
