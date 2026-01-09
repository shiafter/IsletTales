using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConsumableItem : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amount;


    public enum StatToChange
    {
        None,
        Health,
        MaxHealth
    }

    public void UseItem()
    {
        if(statToChange == StatToChange.Health)
        {
            PlayerHealth.instance.Heal(amount);
            SoundEffectManager.Play("Heal");
        }
        if (statToChange == StatToChange.MaxHealth)
        {
            PlayerHealth.instance.IncreaseMaxHealth(amount);
        }
    }
}
