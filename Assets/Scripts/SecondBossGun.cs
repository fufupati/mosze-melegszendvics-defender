using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossGun : MonoBehaviour
{
    public GameObject SecondBossBulletGO;
    
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
            StartCoroutine(BurstFireRoutine());
        }
    }

    void StopShooting()
    {
        if (isShooting)
        {
            StopCoroutine(BurstFireRoutine());
            isShooting = false;
        }
    }

    IEnumerator BurstFireRoutine()
    {
        while (isShooting)
        {
            for (int i = 0; i < 3; i++) 
            {
                FireBossBullet();
                yield return new WaitForSeconds(0.2f); 
            }

            yield return new WaitForSeconds(1f); 
        }
    }

    void FireBossBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null)
        {
            GameObject bullet = Instantiate(SecondBossBulletGO);
            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<SecondBossBullet>().SetDirection(direction);
        }
    }
}
