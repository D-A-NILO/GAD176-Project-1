using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = 9.81f;

    
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Camera playerCamera; // Manually assign this in Unity Inspector

    [SerializeField] private int health = 100;

    private Rigidbody rb;
    private bool isGrounded;
    private float verticalLookRotation = 0f;
    private WeaponManagerScript weaponManager;

    void Start()
    {
        weaponManager = GetComponent<WeaponManagerScript>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents unwanted rotation
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Check if the camera is assigned
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is NOT assigned! Please assign it in the Inspector.");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weaponManager.FireWeapon();
        }
        HandleMovement();
        HandleJump();
        HandleMouseLook();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    private void HandleMouseLook()
    {
        if (playerCamera == null) return; // Prevent errors if the camera is missing

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player left/right
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera up/down
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player Health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        // Implement respawn or game over logic
    }
}
