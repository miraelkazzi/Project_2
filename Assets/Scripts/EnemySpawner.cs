using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
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

    public GameObject winPanel;

    private int currentWave = 0;
    private int enemiesAlive = 0;

    private bool canStartNextWave = false;
    private bool roomUnlocked = false;

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (currentWave < maxWaves)
        {
            if (currentWave == 0)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            else
            {
                if ((currentWave == 2 || currentWave == 4) && !roomUnlocked)
                {
                    Debug.Log("Waiting for trigger for Wave " + (currentWave + 1));
                    yield return new WaitUntil(() => canStartNextWave);
                    canStartNextWave = false;
                    roomUnlocked = true;
                }
                else
                {
                    yield return new WaitForSeconds(timeBetweenWaves);
                }
            }

            currentWave++;
            Debug.Log("===== STARTING WAVE " + currentWave + " =====");

            yield return StartCoroutine(SpawnWave());

            yield return new WaitUntil(() => enemiesAlive <= 0);

            Debug.Log("===== WAVE " + currentWave + " CLEARED =====");

            if (currentWave == maxWaves)
            {
                if (winPanel != null)
                    winPanel.SetActive(true);

                SoundManager.Instance.PlaySFX(SoundManager.Instance.winSound);

                Time.timeScale = 0f;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                var controller = FindFirstObjectByType<StarterAssets.FirstPersonController>();
                if (controller != null)
                    controller.enabled = false;

                var gun = FindFirstObjectByType<GunShoot>();
                if (gun != null)
                    gun.enabled = false;

                yield break;
            }

            if (currentWave == 4)
            {
                roomUnlocked = false;
            }
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform point = spawnPoints[i];

            WaveSpawnPoint wavePoint = point.GetComponent<WaveSpawnPoint>();

            if (wavePoint == null) continue;
            if (wavePoint.waveNumber != currentWave) continue;

            int enemiesForThisPoint = Random.Range(minEnemiesPerPoint, maxEnemiesPerPoint + 1);
            enemiesForThisPoint += (currentWave - 1) * increasePerWave;

            for (int j = 0; j < enemiesForThisPoint; j++)
            {
                SpawnEnemyAtPoint(point, wavePoint);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }

    void SpawnEnemyAtPoint(Transform point, WaveSpawnPoint wavePoint)
    {
        Vector3 spawnPosition;
        bool foundPosition = TryGetValidSpawnPosition(point.position, out spawnPosition);

        if (!foundPosition)
        {
            spawnPosition = point.position;
        }

        if (wavePoint.enemyPrefabs == null || wavePoint.enemyPrefabs.Length == 0) return;

        int maxIndex = Mathf.Min(currentWave, wavePoint.enemyPrefabs.Length) - 1;
        int randomIndex = Random.Range(0, maxIndex + 1);

        GameObject enemyObj = Instantiate(wavePoint.enemyPrefabs[randomIndex], spawnPosition, point.rotation);

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

    public bool IsWaveCleared()
    {
        return enemiesAlive <= 0;
    }

    public void StartNextWave()
    {
        Debug.Log("Trigger started next wave");
        canStartNextWave = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}