using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject[] spawner;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Settings")] [SerializeField] private int enemyCount;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float spawnInterval = 1f;

    private void Start()
    {
        StartCoroutine(nameof(StartSpawn));
    }

    private IEnumerator StartSpawn()
    {
        while (enemyCount < maxEnemies)
        {
            int randomSpawner = UnityEngine.Random.Range(0, spawner.Length);
            var enemy = Instantiate(enemyPrefab, spawner[randomSpawner].transform.position, Quaternion.identity);
            enemy.transform.SetParent(spawner[randomSpawner].transform);
            enemyCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
