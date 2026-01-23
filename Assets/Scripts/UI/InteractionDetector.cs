using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;
    private PlayerInputHandler playerInput;
    private void Start()
    {
        interactionIcon.SetActive(false);
        playerInput = GetComponentInParent<PlayerInputHandler>();
        if(playerInput != null)
        {
            playerInput.OnInteract.AddListener(HandleInteract);
        }
        
    }
    private void OnDestroy()
    {
        if(playerInput != null)
        {
            playerInput.OnInteract.RemoveListener(HandleInteract);
        }
    }
    public void HandleInteract()
    {
        Debug.Log("HANDLER PRESSED");
        interactableInRange?.Interact();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
