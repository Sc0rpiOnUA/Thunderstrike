using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public bool isMoving;

    private Rigidbody playerRigidbody;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Movement.performed += Movement_performed;
        playerInputActions.Player.Movement.canceled += Movement_canceled;
    }

    private void Movement_canceled(InputAction.CallbackContext obj)
    {
        isMoving = false;
    }

    private void FixedUpdate()
    {       
        if(isMoving)
        {
            Vector2 movementVector2 = playerInputActions.Player.Movement.ReadValue<Vector2>();
            Vector3 movementVector3 = new Vector3(movementVector2.x, 0, movementVector2.y);
            Quaternion playerRotation = Quaternion.LookRotation(movementVector3, Vector3.up);

            playerRigidbody.MovePosition(playerRigidbody.position + movementVector3 * playerSpeed * Time.fixedDeltaTime);
            playerRigidbody.rotation = playerRotation;
        }        
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        isMoving = true;
    }
}
