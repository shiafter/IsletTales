using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DragManager : MonoBehaviour
{
    public static DragManager instance;

    public Image dragItemImage;
    public Canvas UICanvas;

    public ItemSlot sourceSlot;

    private void Awake()
    {
        instance = this;
        dragItemImage.gameObject.SetActive(false);
    }

    public void EnableDrag(Sprite sprite, ItemSlot slot)
    {
        sourceSlot = slot;

        dragItemImage.sprite = sprite;
        dragItemImage.gameObject.SetActive(true);
        dragItemImage.transform.SetAsLastSibling();
    }

    public void DragItem(Vector2 screenPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(UICanvas.transform as RectTransform, screenPos, UICanvas.worldCamera, out Vector2 localPos);
        dragItemImage.rectTransform.localPosition = localPos;
    }

    public void DisableDrag()
    {
        sourceSlot = null;
        dragItemImage?.gameObject.SetActive(false);
    }
}
