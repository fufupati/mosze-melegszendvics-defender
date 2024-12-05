using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    float speed;

    public void Start()
    {
        speed = 8f;
    }

    
    public void Update()
    {
        if (this == null)
        {
            return; // Exit if this object has been destroyed
        }

        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        
        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint ( new Vector2 (1, 1));

        if(transform.position.y > max.y)
        {
            DestroyImmediate(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.tag == "EnemyShipTag")
        {
            Destroy(gameObject);
        }

        if(col.tag == "BossShipTag")
        {
            Destroy(gameObject);
        }
    }
}
