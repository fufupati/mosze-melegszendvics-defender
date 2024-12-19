
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreUITextGO;
    GameObject CurrencyUITextGO;
    public GameObject ExplosionGO;
   float speed;
    public void Start()
    {
        speed=2f;

        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
        CurrencyUITextGO = GameObject.FindGameObjectWithTag("CurrencyTextTag");
    }


    public void Update()
    {
        Vector2 position = transform.position;

        position=new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position= position;

        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

         if(transform.position.y < min.y){
            Destroy(gameObject);
         }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            CurrencyUITextGO.GetComponent<Currency>().Score += 100;

            Destroy(gameObject);
        }
    }

    public void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate (ExplosionGO);
        explosion.transform.position = transform.position;
    }
}

