using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public Text LivesUIText;
    const int MaxLives = 3;
    int lives;

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
    }

    public GameObject PlayerBulletGo;
    public GameObject BulletPosition01;
    public GameObject GameManagerGO;
    public GameObject ExplosionGO;
    public float speed;

    private float lastFireTime = 0f;
    public float fireCooldown = 1.5f; // 1.5 seconds default cooldown

    // Reference to the Currency script
    public Currency playerCurrency;

    // Add a public method to increase speed
    public void IncreaseSpeed()
    {
        if (playerCurrency.Score >= 500)  // Check if currency is at least 500
        {
            if (playerCurrency.DeductPoints(500))  // Deduct 500 from currency
            {
                speed = Mathf.Min(10f, speed + 1f); // Increase speed but limit it to a max value (e.g., 10)
                Debug.Log("Speed increased! Current speed: " + speed);
            }
        }
        else
        {
            Debug.Log("Not enough currency to increase speed.");
        }
    }

    void Start()
    {
    }

    public void ShootBullet()
    {
        GetComponent<AudioSource>().Play();
        GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGo);
        bullet01.transform.position = BulletPosition01.transform.position;
    }

    public void Update()
    {
        // Fire bullet when spacebar is pressed
        if (Input.GetKeyDown("space") && Time.time > lastFireTime + fireCooldown)
        {
            lastFireTime = Time.time;
            ShootBullet();
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    public void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.225f;
        min.y = min.y + 0.225f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || (col.tag == "BossBulletTag") || (col.tag == "Asteroid") || (col.tag == "SecondBossBulletTag"))
        {
            PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();

            if (lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            }
        }
    }

    public void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }


    public void IncreaseFireRate()
    {
        if (playerCurrency.Score >= 500)  
        {
            if (playerCurrency.DeductPoints(500))  
            {
                fireCooldown = Mathf.Max(0.1f, fireCooldown - 0.2f); 
            }
        }
        else
        {
            Debug.Log("Not enough currency to increase fire rate.");
        }
    }
}
