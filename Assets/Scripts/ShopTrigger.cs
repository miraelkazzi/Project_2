using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public Shop shop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shop.OpenShop();
        }
    }
}