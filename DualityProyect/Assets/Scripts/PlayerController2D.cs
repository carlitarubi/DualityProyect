using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // Librería para que funcione el New Input System

public class PlayerController2D : MonoBehaviour
{
    // Referencias generales
    [SerializeField] Rigidbody2D playerRb; // Referencia al rigidbody del player
    [SerializeField] PlayerInput playerInput; // Referencia al gestor del input del jugador
    [SerializeField] Animator playerAnim; // Referencia al animator para gestionar las transiciones de animación

    [Header("Movement Parameters")]
    private Vector3 moveInput; //Almacén del input del player
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
        isAlive = true;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            GameManager.Instance.points += 1;
            //GetComponent<Soundpickup>().PlayPickupSound();
            other.gameObject.SetActive(false);

        }
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
        // Conector de valores generales con parametros de animación
        playerAnim.SetBool("Jump", !isGrounded);
        if (moveInput.x > 0 || moveInput.x < 0) playerAnim.SetBool("Run", true);
        else playerAnim.SetBool("Run", false);
    }

    private static void RestartScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
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

    // Cambia de plano
    public void HandleSwap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isAlive == true)
            {
                transform.position += new Vector3(0, -11, 0);
            } 
            else
            {
                transform.position += new Vector3(0, 11, 0);
            }
            isAlive = !isAlive;
        }
    }

    // Reinicia la escena
    public void HandleReset(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RestartScene();
        }
    }

    #endregion
}
