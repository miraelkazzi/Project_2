using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public EnemySpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawner.StartNextWave();
            gameObject.SetActive(false);
        }
    }
}