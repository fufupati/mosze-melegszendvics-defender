using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    public GameObject EnemyBulletGO;
    
    private bool isShooting = false;

    void Start()
    {
        StartShooting();
    }

    void OnEnable()
    {
        StartShooting();
    }

    void OnDisable()
    {
        StopShooting();
    }

    void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            InvokeRepeating("FireEnemyBullet", 1f, 1f);
        }
    }

    void StopShooting()
    {
        if (isShooting)
        {
            CancelInvoke("FireEnemyBullet");
            isShooting = false;
        }
    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null)
        {
            GameObject bullet = Instantiate(EnemyBulletGO);
            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<BossBullet>().SetDirection(direction);
        }
    }
}
