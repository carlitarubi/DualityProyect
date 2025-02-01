using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PruebaPlayerController : MonoBehaviour
{
    
    private Rigidbody2D playerRb;
    private Animator Anim;
    private float horInput;

    //Variables de estadística del player
    public float speed;
    public float jumpForce; 
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        horInput = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(horInput * speed, playerRb.velocity.y);
    }

    void Jump()
    {
       if(Input.GetKeyDown(KeyCode.Space ))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }
}
