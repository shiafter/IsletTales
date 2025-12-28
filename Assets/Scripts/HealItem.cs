using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth controller = other.GetComponent<PlayerHealth>();

        if(controller != null)
        {
            controller.Heal(1);
            Destroy(gameObject);
        }
    }
}
