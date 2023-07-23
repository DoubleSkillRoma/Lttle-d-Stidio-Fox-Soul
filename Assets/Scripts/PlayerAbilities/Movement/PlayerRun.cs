using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    public PlayerRunData data;
    public Rigidbody2D rb { get; private set; }

    public bool isFacingRight { get; private set; }
    public bool isGrounded { get; private set; } 

    private Vector2 _moveInput;

    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isFacingRight = true;
        isGrounded = false; 
    }

    private void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");

        if (_moveInput.x != 0)
            CheckDirectionToFace(_moveInput.x > 0);

        // Jump
        if (Input.GetKeyDown(KeyCode.W) && isGrounded) 
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Run();
    }


    private void Run()
    {
        float targetSpeed = _moveInput.x * data.runMaxSpeed;

        float accelRate;

        if (!isGrounded)
        {
            accelRate = data.runAccelAmount * data.accelInAir;
        }
        else
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.runAccelAmount : data.runDeccelAmount;
        }

        if (data.doConserveMomentum && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && isGrounded)
        {
            accelRate = 0;
        }

        float speedDif = targetSpeed - rb.velocity.x;

        float movement = speedDif * accelRate;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }

    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != isFacingRight)
            Turn();
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0f, data.jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
