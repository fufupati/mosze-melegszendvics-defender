using NUnit.Framework;
using UnityEngine;

public class StarGeneratorTests
{
    private GameObject starGeneratorObject;
    private StarGenerator starGenerator;
    private GameObject starPrefab;

    [SetUp]
    public void SetUp()
    {
        starGeneratorObject = new GameObject();
        starGenerator = starGeneratorObject.AddComponent<StarGenerator>();

        // Create a mock Star GameObject with SpriteRenderer and Star component
        starPrefab = new GameObject("StarPrefab");
        starPrefab.AddComponent<SpriteRenderer>();
        starPrefab.AddComponent<Star>();

        starGenerator.StarGO = starPrefab;
        starGenerator.MaxStars = 5;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(starGeneratorObject);
        Object.DestroyImmediate(starPrefab);
    }

    [Test]
    public void Start_GeneratesMaxStarsWithRandomProperties()
    {
        // Run Start method
        starGenerator.Start();

        // Check if MaxStars number of stars have been created
        Assert.AreEqual(starGenerator.MaxStars, starGeneratorObject.transform.childCount);

        // Validate each star's properties
        for (int i = 0; i < starGenerator.MaxStars; i++)
        {
            GameObject star = starGeneratorObject.transform.GetChild(i).gameObject;
            SpriteRenderer spriteRenderer = star.GetComponent<SpriteRenderer>();

            // Check if the star color is correctly set from starColors
            Color expectedColor = starGenerator.starColors[i % starGenerator.starColors.Length];
            Assert.AreEqual(expectedColor, spriteRenderer.color);

            // Check if speed is set in the expected range
            Star starComponent = star.GetComponent<Star>();
            Assert.IsTrue(starComponent.speed <= -0.5f && starComponent.speed >= -1.5f);
        }
    }
}
