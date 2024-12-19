using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
  
  public GameObject AsteroidGO;

  float maxSpawnRate = 5f;
    void Start()
    {
      

      
    }

   
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint ( new Vector2(0,0));

       
        Vector2 max = Camera.main.ViewportToWorldPoint ( new Vector2(1,1));


        GameObject Asteroid= (GameObject)Instantiate (AsteroidGO);
        Asteroid.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
   
        ScheduleNextEnemySpawn();


   
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;

        if(maxSpawnRate >1f) {
            spawnInSeconds = Random.Range(1f, maxSpawnRate);
        }else 
                spawnInSeconds=1f;

        Invoke ("SpawnEnemy", spawnInSeconds);


    }

   

    public void ScheduleEnemySpawner()
    {
float maxSpawnRate = 10f;

      Invoke("SpawnEnemy", maxSpawnRate); 

    }

    public void UnscheduleEnemySpawner()
    {
      CancelInvoke("SpawnEnemy");
      
    }

}