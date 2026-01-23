using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;
    public UnityEvent OnInteract;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition, interact;

    private void Update()
    {
        OnMovementInput?.Invoke(movement.action.ReadValue<Vector2>().normalized);
        OnPointerInput?.Invoke(GetPointerInput());
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    private void OnEnable()
    {
        movement.action.Enable();
        attack.action.Enable();
        pointerPosition.action.Enable();
        interact.action.Enable();

        attack.action.performed += PerformAttack;
        interact.action.performed += PerformInteract;

    }
    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
        interact.action.performed -= PerformInteract;

        movement.action.Disable();
        attack.action.Disable();
        pointerPosition.action.Disable();
        interact.action.Disable();
    }
    private void PerformAttack(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }
    private void PerformInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (ShopManager.instance != null && ShopManager.instance.isOpening)
        {
           ShopManager.instance.CloseShop();
            return;
            
        }
        OnInteract?.Invoke();
    }
}
