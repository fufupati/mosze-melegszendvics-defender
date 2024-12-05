using UnityEngine;

public class bossSpawner : MonoBehaviour
{
    public float descentDistance = 1.0f;
    public float descentSpeed = 1.0f;
    public GameObject ExplosionGO;
    public GameObject BigExplosionGO;
    public int hitPoints = 50;
    public int scoreValue = 1000;
    public int hitScoreValue = 100;
    public int deathScoreValue = 1000;

    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private bool hasDescended = false;
    private int hitCount = 0;
    private bool isDestroyed = false;
    private GameObject scoreUITextGo;

    void Start()
    {
        scoreUITextGo = GameObject.FindGameObjectWithTag("ScoreTextTag");
        initialPosition = transform.position;
       
        targetPosition = transform.position; 
        transform.position = new Vector3(targetPosition.x, targetPosition.y + descentDistance + 5.0f, targetPosition.z);
        ResetBoss();
    }

    void Update()
    {
        int currentScore = scoreUITextGo.GetComponent<GameScore>().Score;

        if (currentScore >= scoreValue && !hasDescended && !isDestroyed)
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
            scoreUITextGo.GetComponent<GameScore>().Score += hitScoreValue;

            hitCount++;

            if (hitCount >= hitPoints)
            {
                isDestroyed = true;
                hasDescended = false;
                Invoke("PlayBigExplosion", 0f);
                Invoke("PlayBigExplosion", 0.5f);
                Invoke("PlayBigExplosion", 1f);
               
                scoreUITextGo.GetComponent<GameScore>().Score += deathScoreValue;
                
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
                
                gameObject.SetActive(false); // Boss eltűnik a képernyőről
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

    public void ResetBoss()
    {
        hitCount = 0;
        hasDescended = false;
        isDestroyed = false;
        // Set the boss back to the starting position (off-screen)
        transform.position = new Vector3(initialPosition.x, initialPosition.y + descentDistance + 5.0f, initialPosition.z);
        gameObject.SetActive(true); // Boss újra megjelenik
    
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
