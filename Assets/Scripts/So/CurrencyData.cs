using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CurrencyData : ItemData
{
    public enum CurrencyType
    {
        None,
        Silver,
        Gold,
        Ores
    }
    public CurrencyType currencyType;
}
