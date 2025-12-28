using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth controller = other.GetComponent<PlayerHealth>();

        if (controller != null)
        {
            controller.IncreaseMaxHealth(1);
            Destroy(gameObject);
        }
    }
}

