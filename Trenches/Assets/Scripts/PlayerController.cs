using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveSpeed;

    public float direction;

    private bool isVaulting;

    [SerializeField] public Color playerColor;

    private bool canVault;

    private float jumpForce;

    private float moveVertical;

    private float moveHorizontal;

    private bool vault;

    private bool isCrouching;

    private bool isStanding;

    void Start()
    {

        playerColor = gameObject.transform.GetComponent<SpriteRenderer>().color;

        canVault = false;

        moveSpeed = 1f;

        jumpForce = 60f;

        isStanding = true;

        isCrouching = false;
        
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveHorizontal = context.ReadValue<float>();
    }

    public void OnVault(InputAction.CallbackContext context)
    {
        vault = context.action.triggered;
    }

    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb.AddForce(new Vector2(moveSpeed * moveHorizontal, 0f), ForceMode2D.Impulse);

            direction = moveHorizontal * 1;
        }

        if (vault == true && canVault == true && !isVaulting)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
