using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject[] spawner;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] eliteEnemyPrefab;

    [Header("Settings")] [SerializeField] private int enemyCount;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int eliteEnemyChance = 20;

    private void Start()
    {
        StartCoroutine(nameof(StartSpawn));
    }

    private IEnumerator StartSpawn()
    {
        while (enemyCount < maxEnemies)
        {
            int randomSpawner = UnityEngine.Random.Range(0, spawner.Length);
            var enemy = Instantiate(ChooseEnemy(), spawner[randomSpawner].transform.position, Quaternion.identity);
            enemy.transform.SetParent(spawner[randomSpawner].transform);
            enemyCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    /*
    private int EliteEnemyChance()
    {
        int isElite = Random.Range(0, 100);
        if (isElite <= eliteEnemyChance)
        {
            return 1;
        }

        return 0;
    } */

    private GameObject ChooseEnemy()
    {
        int isElite = Random.Range(0, 100);

        if (isElite <= eliteEnemyChance)
        {
            return eliteEnemyPrefab[Random.Range(0, eliteEnemyPrefab.Length)];
        }
        else
        {
            return enemyPrefab;
        }
    }

}
