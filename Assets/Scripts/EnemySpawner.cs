using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public int startingEnemies = 3;
    public int increasePerWave = 2;
    public int maxWaves = 5;

    public float timeBetweenWaves = 5f;
    public float timeBetweenSpawns = 1f;

    private int currentWave = 0;
    private int enemiesAlive = 0;

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (currentWave < maxWaves)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;

            int enemiesToSpawn = startingEnemies + (currentWave - 1) * increasePerWave;

            Debug.Log("Wave " + currentWave + " started");

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            yield return new WaitUntil(() => enemiesAlive <= 0);

            Debug.Log("Wave " + currentWave + " cleared");
        }

        Debug.Log("All waves completed");
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemyObj = Instantiate(enemyPrefab, point.position, point.rotation);

        enemiesAlive++;

        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.spawner = this;
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }
}