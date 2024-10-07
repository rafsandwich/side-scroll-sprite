using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private CustomInput input = null;
    private Rigidbody2D rb = null;
    private Animator animator = null;
    private Vector2 moveVector = Vector2.zero; // store value of movement presses

    private void Awake()
    {
        input = new CustomInput();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += MovePerformed; // subscribe to move performed / cancelled events
        input.Player.Movement.canceled += MoveCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= MovePerformed; // unsubscribe to move performed / cancelled events
        input.Player.Movement.canceled -= MoveCancelled;
    }
    private void FixedUpdate()
    {
        rb.velocity = moveVector * moveSpeed;
    }

    private void MovePerformed(InputAction.CallbackContext callbackContext)
    {
        moveVector = callbackContext.ReadValue<Vector2>();

        if (moveVector.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // make character face left / right based on input
        }

        animator.SetBool("isRunning", true);
    }

    private void MoveCancelled(InputAction.CallbackContext callbackContext)
    {
        moveVector = Vector2.zero;
        animator.SetBool("isRunning", false);
    }
}
