using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Librer�a para que funcione el New Input System

public class PlayerController2D : MonoBehaviour
{
    // Referencias generales
    [SerializeField] Rigidbody2D playerRb; // Referencia al rigidbody del player
    [SerializeField] PlayerInput playerInput; // Referencia al gestor del input del jugador
    [SerializeField] Animator playerAnim; // Referencia al animator para gestionar las transiciones de animaci�n

    [Header("Movement Parameters")]
    private Vector3 moveInput; //Almac�n del input del player
    public float speed;
    [SerializeField] bool isFacingRight;

    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;
    // Variables para el GroundCheck
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;

    [Header("Swap Parameters")]
    [SerializeField] bool isAlive;

    void Start()
    {
        // Autoreferenciar componentes: nombre de variable = GetComponent()
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>();
        isFacingRight = true;
        isAlive = false;
    }

    void Update()
    {
        HandleAnimations();

        GroundCheck();

        if (moveInput.x > 0)
        {
            if (!isFacingRight)
            {
                Flip();
            }
        }
        if (moveInput.x < 0)
        {
            if (isFacingRight)
            {
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        playerRb.velocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, 0);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight; // !bool = su opuesto
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void HandleAnimations()
    {
        // Conector de valores generales con parametros de animaci�n
        playerAnim.SetBool("isJumping", !isGrounded);
        if (moveInput.x > 0 || moveInput.x < 0) playerAnim.SetBool("isRunning", true);
        else playerAnim.SetBool("isRunning", false);
    }

    #region Input Events

    // Para crear un evento:
        // Se difine public sin tipo de dato (VOID) y con una referencia al input (Callback.Context)

    public void HandleMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    public void HandleSwap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAlive = !isAlive;
        }
    }

    #endregion
}
