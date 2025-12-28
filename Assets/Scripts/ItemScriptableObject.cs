using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemScriptableObject : ScriptableObject
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
            GameObject.Find("Player").GetComponent<PlayerHealth>().Heal(amount);
        }
        if (statToChange == StatToChange.MaxHealth)
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().IncreaseMaxHealth(amount);
        }
    }
}
