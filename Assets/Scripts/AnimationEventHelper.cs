using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTrigger, OnAttackPerformed;
    private WeaponParent weaponParent;

    private void Awake()
    {
        weaponParent = GetComponentInParent<WeaponParent>();
    }
    public void TriggerEvent()
    {
        OnAnimationEventTrigger?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }

}
