using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
   
    public Queue<GameObject> availablePlanets = new Queue<GameObject>();
    public GameObject[] Planets;
    public void Start()
    {
        availablePlanets.Enqueue(Planets[0]);
        availablePlanets.Enqueue(Planets[1]);
        availablePlanets.Enqueue(Planets[2]);

        InvokeRepeating("MovePlanetDown", 0 ,20f);
    }

    public void MovePlanetDown()
    {

        EnqueuePlantes();
        if(availablePlanets.Count == 0)
        return;

        GameObject aPlanet = availablePlanets.Dequeue();

        aPlanet.GetComponent<Planet> ().isMoving =true;
    }

    public void EnqueuePlantes()
    {
        foreach(GameObject aPlanet in Planets)
        {
            if((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                aPlanet.GetComponent<Planet>().ResetPosition();

                availablePlanets.Enqueue(aPlanet);
            }
        }
    }

}

