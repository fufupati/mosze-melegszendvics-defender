
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    float speed;
    public GameObject ExplosionGO;
    GameObject scoreUITextGO;

    public void Start()
    {
        speed=2f;
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }


    public void Update()
    {
        Vector2 position = transform.position;

        position=new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position= position;

        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

         if(transform.position.y < min.y){
            DestroyImmediate(gameObject);
         }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            PlayExplosion();
            Destroy(gameObject);
        }
    }

    public void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}

