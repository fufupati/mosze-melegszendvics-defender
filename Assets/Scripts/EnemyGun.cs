using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    public GameObject EnemyBulletGO;
    
    public void Start()
    {

        Invoke("FireEnemyBullet", 1f);
        
    }

    
    void Update()
    {
        
    }

    public void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find ("PlayerGO");

        if (playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}