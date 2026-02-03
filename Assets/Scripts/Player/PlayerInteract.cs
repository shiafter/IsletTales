using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private IInteractable currentInteract;
    public void SetInteract(IInteractable interactable)
    {
        currentInteract = interactable;
    }
    public void ClearInteract(IInteractable interactable)
    {
        if(currentInteract == interactable)
        {
            currentInteract = null;
        }
    }
    public void Interact()
    {
        if(currentInteract != null && currentInteract.CanInteract())
        {
            currentInteract.Interact();
        }
    }
}
