using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2D;

    private bool isJumping;

    private float jumpForce;

    private float moveSpeed;

    private bool jump;

    public float moveVertical;

    public float moveHorizontal;

    [SerializeField] public Vector2 playerPos;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 0.5f;

        jumpForce = 3f;

        isJumping = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveHorizontal = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.action.triggered;
    }

    void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (jump == true && !isJumping)
        {
            rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cube")
        {
            if (collision.gameObject.GetComponent<color>().canKill == true)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "ground")
        {
            isJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isJumping = true;
        }
    }
}
