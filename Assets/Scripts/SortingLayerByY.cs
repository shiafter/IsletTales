using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerByY : MonoBehaviour
{
    private SpriteRenderer render;
    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        render.sortingOrder = (int)(-transform.position.y * 100);
    }
}
