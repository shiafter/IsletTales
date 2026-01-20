using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    private Equipment equipment;

    private void Awake()
    {
        equipment = GetComponentInParent<Equipment>();
    }
    public void TriggerAttack()
    {
        if (equipment == null)
        {
            Debug.LogError("Equipment not found in parent");
            return;
        }
        Debug.Log("Deal Dmg");
        equipment.DetectCollider();
    }
    public void EndAnimation()
    {
        Debug.Log("Reset attack");
        equipment.ResetAction();
    }
}
