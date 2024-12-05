using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    const int MaxLives= 3;
    int lives;
    public GameObject ExplosionGO;
    public GameObject GameManagerGO;
    public Text LivesUIText;

    public void Init(){
        lives = MaxLives;
        transform.position = new Vector2(0,0);
        gameObject.SetActive(true);
        LivesUIText.text = lives.ToString();
    }

    public GameObject PlayerBulletGo;
    public GameObject BulletPosition01;
    public float speed;
    
    public void ShootBullet()
    {
        GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGo);
        bullet01.transform.position = BulletPosition01.transform.position;
    }

    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
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
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || (col.tag == "BossBulletTag"))
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
}
