using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentRotation : MonoBehaviour
{
    public Equipment equipment;
    private void LateUpdate()
    {
        Vector3 rot = transform.localEulerAngles;
        rot.z *= equipment.facingLeft;
        transform.localEulerAngles = rot;
    }
}
