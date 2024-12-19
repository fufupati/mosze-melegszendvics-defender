using UnityEngine;

public class bossSpawner : MonoBehaviour
{
    public float descentDistance = 1.0f;
    public float descentSpeed = 1.0f;
    public GameObject scoreUITextGo;
    public GameObject ExplosionGO;
    public GameObject BigExplosionGO;

    public GameObject CurrencyUITextGO;

    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private bool hasDescended = false;
    private int hitCount = 0;
    private bool isDestroyed = false;

    public void Start()
    {
        scoreUITextGo = GameObject.FindGameObjectWithTag("ScoreTextTag");
        initialPosition = transform.position;
       
        targetPosition = transform.position; 
        transform.position = new Vector3(targetPosition.x, targetPosition.y + descentDistance + 5.0f, targetPosition.z);
        ResetBoss();
    }

    public void Update()
    {
        int currentScore = scoreUITextGo.GetComponent<GameScore>().Score;

        if (currentScore >= 1000 && !hasDescended && !isDestroyed)
        {
            hasDescended = true;
        }

        if (hasDescended && !isDestroyed)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, descentSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag" || col.tag == "PlayerBulletTag") && !isDestroyed)
        {
            PlayExplosion();
            scoreUITextGo.GetComponent<GameScore>().Score += 100;
            CurrencyUITextGO.GetComponent<Currency>().Score += 100;
            

            hitCount++;

            if (hitCount >= 15)
            {
                isDestroyed = true;
                hasDescended = false;
                Invoke("PlayBigExplosion", 0f);
                Invoke("PlayBigExplosion", 0.5f);
               
               
                scoreUITextGo.GetComponent<GameScore>().Score += 1000;
                CurrencyUITextGO.GetComponent<Currency>().Score += 1000;
                

                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
                
                gameObject.SetActive(false); 
            }
        }
    }

    public void PlayExplosion()
    {
        GameObject explosion = Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
        Destroy(explosion, 2.0f);
    }

    public void PlayBigExplosion()
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
     
        transform.position = new Vector3(initialPosition.x, initialPosition.y + descentDistance + 5.0f, initialPosition.z);
        gameObject.SetActive(true); 
    
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
