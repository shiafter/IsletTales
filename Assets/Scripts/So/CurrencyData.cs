using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CurrencyData : ScriptableObject
{
    public enum CurrencyType
    {
        Silver,
        Gold,
        Ores
    }
    public CurrencyType currencyType;
    public Sprite icon;
}
