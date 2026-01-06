using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupItemNoti : MonoBehaviour
{
    public static PickupItemNoti Instance { get; private set; }

    public TMP_Text notification;
    public int maxPopup = 4;
    public float popupDuration = 3f;

    private readonly Queue<TMP_Text> activePopup = new();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances");
            Destroy(gameObject);
        }
    }

    public void ShowItemPopup(string itemName, int quantity)
    {
        TMP_Text newPopup = Instantiate(notification, transform);
        newPopup.text = "Picked up " + itemName + " x " + quantity.ToString();

        activePopup.Enqueue(newPopup);
        if(activePopup.Count > maxPopup)
        {
            Destroy(activePopup.Dequeue());
        }

        StartCoroutine(FadeOutAndDestroy(newPopup));
    }

    private IEnumerator FadeOutAndDestroy(TMP_Text popup)
    {
        yield return new WaitForSeconds(popupDuration);
        if(popup == null) yield break;

        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        for(float timePassed = 0f; timePassed < 1f; timePassed += Time.deltaTime)
        {
            if (popup == null) yield break;
            canvasGroup.alpha = 1f - timePassed;
            yield return null;
        }

        Destroy(popup);
    }
}
