using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public bool canMove, isMoving, isShooting, isDying;

    public ForceMode forceMode;

    public Animator playerAnimator;

    private Rigidbody playerRigidbody;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private PlayerStatus playerStatus;
    private Camera mainCamera;
    private Plane infinitePlane = new Plane(Vector3.up, 0);

    private enum MovementSystem { Rigid, Fluid}
    [SerializeField] MovementSystem movementSystem;

    private void Awake()
    {
        canMove = true;
        isDying = false;
        mainCamera = Camera.main;

        playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerStatus = GetComponent<PlayerStatus>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Movement.performed += Movement_performed;
        playerInputActions.Player.Movement.canceled += Movement_canceled;
        playerInputActions.Player.Shooting.performed += Shot_performed;
        playerInputActions.Player.Shooting.canceled += Shot_canceled;
        playerInputActions.Player.Interacting.performed += Interaction_performed;
        playerInputActions.Player.Escape.performed += Escape_performed;
    }    

    private void FixedUpdate()
    {
        if (!isDying && canMove)
        {
            Vector2 movementVector2 = playerInputActions.Player.Movement.ReadValue<Vector2>();
            Vector3 movementVector3 = new Vector3(movementVector2.x, 0, movementVector2.y);

            if (movementSystem == MovementSystem.Fluid)
            {
                float velocityX = Vector3.Dot(movementVector3, transform.forward) * 1.41f;
                float velocityZ = Vector3.Dot(movementVector3, transform.right) * 1.41f;

                playerAnimator.SetFloat("VelocityX", velocityX, 0.1f, Time.fixedDeltaTime);
                playerAnimator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.fixedDeltaTime);

                //playerRigidbody.velocity = movementVector3.normalized * playerSpeed * Time.fixedDeltaTime;
                //playerRigidbody.AddForce(movementVector3.normalized * playerSpeed * Time.fixedDeltaTime, forceMode);
                playerRigidbody.MovePosition(playerRigidbody.position + movementVector3.normalized * playerSpeed * Time.fixedDeltaTime);

                HandleRotation();
            }
            else if(movementSystem == MovementSystem.Rigid)
            {
                if (isMoving)
                {
                    Quaternion playerRotation = Quaternion.LookRotation(movementVector3, Vector3.up);

                    playerAnimator.SetFloat("VelocityX", 1, 0.1f, Time.fixedDeltaTime);
                    playerAnimator.SetFloat("VelocityZ", 0, 0.1f, Time.fixedDeltaTime);
                    playerRigidbody.MovePosition(playerRigidbody.position + movementVector3 * playerSpeed * Time.fixedDeltaTime);
                    playerRigidbody.rotation = playerRotation;
                }
                else
                {
                    playerAnimator.SetFloat("VelocityX", 0, 0.1f, Time.fixedDeltaTime);
                }
            }   

            if (isShooting)
            {
                playerStatus.FireShot();
            }
        }
    }

    public void StopMovement()
    {
        canMove = false;
    }

    public void ResumeMovement()
    {
        canMove = true;
    }

    void HandleRotation()
    {
        float distance;
        Vector2 mouseScreenPositon = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = new Vector3();
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPositon);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 1000))
        {
            mouseWorldPosition = hitData.point;
        }
        else
        {
            infinitePlane.Raycast(ray, out distance);
            mouseWorldPosition = ray.GetPoint(distance);
        }

        Vector3 targetDirection = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(-targetDirection.z, targetDirection.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    public void ChangeWeaponType(Weapon.WeaponType newWeaponType)
    {

        switch(newWeaponType)
        {
            case Weapon.WeaponType.Empty:
                {
                    playerAnimator.SetInteger("Weapon", 0);
                    Debug.Log($"Weapon type = {newWeaponType}, passing integer 0");
                    break;
                }
            case Weapon.WeaponType.Pistol:
                {
                    playerAnimator.SetInteger("Weapon", 1);
                    Debug.Log($"Weapon type = {newWeaponType}, passing integer 1");
                    break;
                }
        }
    }

    public void Die()
    {
        isDying = true;
        playerAnimator.SetTrigger("Death");
        playerAnimator.SetInteger("DeathVariant", UnityEngine.Random.Range(1, 5));
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        isMoving = true;
    }

    private void Movement_canceled(InputAction.CallbackContext obj)
    {
        isMoving = false;
    }

    private void Shot_performed(InputAction.CallbackContext obj)
    {
        isShooting = true;        
    }

    private void Shot_canceled(InputAction.CallbackContext obj)
    {
        isShooting = false;
    }

    private void Interaction_performed(InputAction.CallbackContext obj)
    {
        playerStatus.InteractionPerformed();
    }

    private void Escape_performed(InputAction.CallbackContext obj)
    {
        playerStatus.EscapePressed();
    }
}
