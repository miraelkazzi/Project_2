using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value = 1;

    public float rotateSpeed = 100f;
    public float floatSpeed = 2f;
    public float floatHeight = 0.25f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);

        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.AddCoins(value);
        }

        Destroy(gameObject);
    }
}