using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  
    public GameObject EnemyGo;
    
    public bool IsScheduled { get; private set; }

    float maxSpawnRate = 5f;

    void Start()
    {
      InvokeRepeating("IncreaseSpawnRate", 0f,30f);
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint ( new Vector2(0,0));

       
        Vector2 max = Camera.main.ViewportToWorldPoint ( new Vector2(1,1));


        GameObject anEnemy= (GameObject)Instantiate (EnemyGo);
        anEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
   
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;

        if(maxSpawnRate >1f) {
            spawnInSeconds = Random.Range(1f, maxSpawnRate);
        }
        else 
            spawnInSeconds=1f;

        Invoke ("SpawnEnemy", spawnInSeconds);


    }

    void IncreaseSpawnRate() {
        if(maxSpawnRate > 1f)
                maxSpawnRate--;

        if(maxSpawnRate == 1f)
               CancelInvoke("maxSpawnRate");
    }

    public void ScheduleEnemySpawner()
    {
        float maxSpawnRate = 5f;

        Invoke("SpawnEnemy", maxSpawnRate); 

        IsScheduled = true;

        InvokeRepeating("IncreaseSpawnRate", 0f,30f);
    }

    public void UnscheduleEnemySpawner()
    {
        IsScheduled = false;
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }

}
