using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public EnemySpawner spawner;
    public int waveToStart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (spawner.GetCurrentWave() == waveToStart - 1)
            {
                spawner.StartNextWave();
                gameObject.SetActive(false);
            }
        }
    }
}