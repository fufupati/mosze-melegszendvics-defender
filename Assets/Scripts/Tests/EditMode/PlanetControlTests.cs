using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlanetControllerTests
{
    private GameObject planetControllerObject;
    private PlanetController planetController;
    private List<GameObject> planets;

    [SetUp]
    public void SetUp()
    {
        planetControllerObject = new GameObject();
        planetController = planetControllerObject.AddComponent<PlanetController>();

        planets = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            GameObject planet = new GameObject($"Planet{i}");
            Planet planetComponent = planet.AddComponent<Planet>();
            planetComponent.isMoving = false;
            planets.Add(planet);
        }

        planetController.Planets = planets.ToArray();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(planetControllerObject);
        foreach (var planet in planets)
        {
            Object.DestroyImmediate(planet);
        }
    }

    [Test]
    public void Start_EnqueuesInitialPlanets()
    {
        planetController.Start();

        Assert.AreEqual(3, planetController.availablePlanets.Count);
        Assert.Contains(planets[0], planetController.availablePlanets.ToArray());
        Assert.Contains(planets[1], planetController.availablePlanets.ToArray());
        Assert.Contains(planets[2], planetController.availablePlanets.ToArray());
    }

    [Test]
    public void MovePlanetDown_DequeuesPlanetAndStartsMoving()
    {
        planetController.Start();

        planetController.MovePlanetDown();

        GameObject dequeuedPlanet = planets[0];
        Assert.AreEqual(dequeuedPlanet, planetController.Planets[0]);
        Assert.IsTrue(dequeuedPlanet.GetComponent<Planet>().isMoving);
    }

    [Test]
    public void EnqueuePlantes_RepositionsAndEnqueuesPlanet_WhenBelowScreenAndNotMoving()
    {
        planetController.Start();

        planets[0].transform.position = new Vector2(0, -1); // Set position below the screen
        planets[0].GetComponent<Planet>().isMoving = false;

        planetController.EnqueuePlantes();

        Assert.Contains(planets[0], planetController.availablePlanets.ToArray());
    }
}
