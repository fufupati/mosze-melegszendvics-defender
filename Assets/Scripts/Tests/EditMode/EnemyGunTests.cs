using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class EnemyGunTests
{
    private GameObject enemyGunGO;
    private EnemyGun enemyGun;
    private GameObject playerGO;
    private GameObject enemyBulletGO;

    [SetUp]
    public void Setup()
    {
        // Create a GameObject for the EnemyGun
        enemyGunGO = new GameObject();
        enemyGun = enemyGunGO.AddComponent<EnemyGun>();

        // Create a GameObject for the player
        playerGO = new GameObject("PlayerGO");
        playerGO.transform.position = new Vector3(5, 0, 0); // Place the player somewhere

        // Create a mock bullet prefab
        enemyBulletGO = new GameObject();
        enemyBulletGO.AddComponent<EnemyBullet>(); // Ensure EnemyBullet component is present
        enemyGun.EnemyBulletGO = enemyBulletGO; // Assign the bullet prefab to the EnemyGun

        // Set the position of the EnemyGun
        enemyGunGO.transform.position = Vector3.zero; // Position the EnemyGun at the origin
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(enemyGunGO);
        Object.DestroyImmediate(playerGO);
        Object.DestroyImmediate(enemyBulletGO);
    }

    [UnityTest]
    public IEnumerator FireEnemyBullet_CreatesBullet()
    {
        // Act
        enemyGun.FireEnemyBullet();

        // Wait for the next frame to allow the bullet to instantiate
        yield return null;

        // Assert
        Assert.AreEqual(2, GameObject.FindObjectsOfType<EnemyBullet>().Length, "Bullet was not created!");
    }
}