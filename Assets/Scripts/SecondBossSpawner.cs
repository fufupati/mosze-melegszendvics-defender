using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondBossSpawner : MonoBehaviour
{
    public float descentDistance = 1.0f;
    public float descentSpeed = 1.0f;
    public GameObject scoreUITextGo;
    public GameObject ExplosionGO;
    public GameObject BigExplosionGO;

    public GameObject enemySpawner;
    public GameObject Asteroid;
    public GameObject CurrencyUITextGO;

    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private bool hasDescended = false;
    private int hitCount = 0;
    private bool isDestroyed = false;

    void Start()
    {
        scoreUITextGo = GameObject.FindGameObjectWithTag("ScoreTextTag");
        initialPosition = transform.position;
        targetPosition = transform.position;
        transform.position = new Vector3(targetPosition.x, targetPosition.y + descentDistance + 5.0f, targetPosition.z);
        ResetSecondBoss();
    }

    void Update()
    {
        int currentScore = scoreUITextGo.GetComponent<GameScore>().Score;

        if (currentScore >= 10000 && !hasDescended && !isDestroyed)
        {
            hasDescended = true;
        }

        if (hasDescended && !isDestroyed)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, descentSpeed * Time.deltaTime);
        }
    }

   void OnTriggerEnter2D(Collider2D col)
{
    if ((col.tag == "PlayerShipTag" || col.tag == "PlayerBulletTag") && !isDestroyed)
    {
        PlayExplosion();
        scoreUITextGo.GetComponent<GameScore>().Score += 200;
        CurrencyUITextGO.GetComponent<Currency>().Score += 200;

        hitCount++;

        if (hitCount >= 30)
        {
            isDestroyed = true;
            hasDescended = false;
            Invoke("PlayBigExplosion", 0f);
            Invoke("PlayBigExplosion", 0.5f);
            Invoke("PlayBigExplosion", 1f);
            

            enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
            Asteroid.GetComponent<AsteroidSpawner>().UnscheduleEnemySpawner();

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            gameObject.SetActive(false);

           
            GameObject.Find("GameSceneContainer").GetComponent<FadeOut>().StartFadeOut();
        }
    }
}

    void PlayExplosion()
    {
        GameObject explosion = Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
        Destroy(explosion, 2.0f);
    }

    void PlayBigExplosion()
    {
        GameObject explosion = Instantiate(BigExplosionGO);
        explosion.transform.position = transform.position;
        Destroy(explosion, 3.0f);
    }

    public void ResetSecondBoss()
    {
        hitCount = 0;
        hasDescended = false;
        isDestroyed = false;

        transform.position = new Vector3(initialPosition.x, initialPosition.y + descentDistance + 5.0f, initialPosition.z);
        gameObject.SetActive(true);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void LoadGameOverScene()
    {
     
        SceneManager.LoadScene("GameOverScene");  
    }
}
