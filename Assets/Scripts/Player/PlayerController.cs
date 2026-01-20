using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private Animator animator;

    public GameController gameController;

    private Equipment equipment;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerMovement = GetComponent<PlayerMovement>();
        equipment = GetComponentInChildren<Equipment>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        playerMovement.movementInput = MovementInput;
        equipment.PointerPosition = PointerInput;
        AnimatedCharacter();
    }

    private void AnimatedCharacter()
    {
        Vector2 lookDirection = PointerInput - (Vector2)transform.position;
        playerAnimation.RotateToPointer(lookDirection);
        playerAnimation.PlayAnimation(MovementInput);
    }
    public void PerformAttack()
    {
        if (gameController.IsUIBlockingInput)
            return;
        equipment.UseEquipment();
        animator.ResetTrigger("Action");
        animator.SetTrigger("Action");
        SoundEffectManager.Play("Swing");
    }
}
