using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb2D;

    public bool isJumping;

    public float jumpForce;

    public float moveSpeed;

    public bool canRespawn;

    public float moveVertical;

    public float moveHorizontal;

    public bool jump;

    public void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 3f;
        jumpForce = 60f;
        isJumping = false;
        canRespawn = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveHorizontal = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.action.triggered;
    }

    public void OnRespawn(InputAction.CallbackContext context)
    {
        Debug.Log("Respawn Attempt");
        
        if (canRespawn == false)
        {
            gameObject.transform.position = new Vector2(0, 0);
            canRespawn = false;
            Debug.Log("Respawn Success!");
        }
    }

    public void FixedUpdate()
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "platform")
        {
            isJumping = false;
        }
        else if (collision.tag == "lava")
        {
            var player = gameObject.transform.position;
            Vector2 playerPos = new Vector2(player.x, player.y);
            GameObject.Find("Lava").GetComponent<MoveToPlayer>().DiedByLava();
            ParticleSystem lavaParticles = GameObject.Find("Lava").GetComponent<ParticleSystem>();
            lavaParticles.Play();
            gameObject.transform.position = new Vector2(0, 0);
        }

        else if (collision.tag == "death")
        {
            var MoveToPlayer = GameObject.Find("Lava").GetComponent<MoveToPlayer>();
            MoveToPlayer.Died();
            rb2D.AddForce(new Vector2(0f, 200f), ForceMode2D.Impulse);
        }

        else if (collision.tag == "deathBoth")
        {
            rb2D.AddForce(new Vector2(0f, 10000f), ForceMode2D.Impulse);

            GameObject deathBoth = GameObject.Find("BounceBoth");

            ParticleSystem DeathBoth = deathBoth.transform.Find("DeathBoth").gameObject.GetComponent<ParticleSystem>();

            DeathBoth.Play();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "platform")
        {
            isJumping = true;
        }

        ParticleSystem lavaParticles = GameObject.Find("Lava").GetComponent<ParticleSystem>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Touched another player");
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        rb2D.constraints = RigidbodyConstraints2D.None;
        Debug.Log("Untouched another player");
    }
}
