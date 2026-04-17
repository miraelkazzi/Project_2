using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public int minEnemiesPerPoint = 4;
    public int maxEnemiesPerPoint = 6;

    public int increasePerWave = 1;
    public int maxWaves = 5;

    public float timeBetweenWaves = 5f;
    public float timeBetweenSpawns = 0.8f;

    public float spawnRadius = 6f;
    public float minDistanceBetweenEnemies = 2.5f;
    public int maxSpawnTries = 20;

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
            Debug.Log("Wave " + currentWave + " started");

            yield return StartCoroutine(SpawnWave());

            yield return new WaitUntil(() => enemiesAlive <= 0);

            Debug.Log("Wave " + currentWave + " cleared");
        }

        Debug.Log("All waves completed");
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int enemiesForThisPoint = Random.Range(minEnemiesPerPoint, maxEnemiesPerPoint + 1);
            enemiesForThisPoint += (currentWave - 1) * increasePerWave;

            for (int j = 0; j < enemiesForThisPoint; j++)
            {
                SpawnEnemyAtPoint(spawnPoints[i]);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }

    void SpawnEnemyAtPoint(Transform point)
    {
        Vector3 spawnPosition;
        bool foundPosition = TryGetValidSpawnPosition(point.position, out spawnPosition);

        if (!foundPosition)
        {
            spawnPosition = point.position;
        }

        GameObject enemyObj = Instantiate(enemyPrefab, spawnPosition, point.rotation);
        enemiesAlive++;

        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.spawner = this;
        }
    }

    bool TryGetValidSpawnPosition(Vector3 center, out Vector3 validPosition)
    {
        for (int i = 0; i < maxSpawnTries; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0f,
                Random.Range(-spawnRadius, spawnRadius)
            );

            Vector3 candidate = center + randomOffset;

            NavMeshHit navHit;
            if (!NavMesh.SamplePosition(candidate, out navHit, 3f, NavMesh.AllAreas))
            {
                continue;
            }

            Collider[] nearby = Physics.OverlapSphere(navHit.position, minDistanceBetweenEnemies);
            bool tooClose = false;

            for (int j = 0; j < nearby.Length; j++)
            {
                if (nearby[j].GetComponent<Enemy>() != null)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                validPosition = navHit.position;
                return true;
            }
        }

        validPosition = center;
        return false;
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}