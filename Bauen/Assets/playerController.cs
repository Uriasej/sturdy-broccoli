using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [SerializeField] private Vector2 mousePosition;

    [SerializeField] private GameObject ball;

    [SerializeField] private Vector2 worldPosition;

    private GameObject selected;

    private Rigidbody2D selectedRB;

    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();

        gameObject.transform.position = worldPosition;
    }

    void FixedUpdate()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Instantiate(ball, worldPosition, Quaternion.identity);
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        Vector2 difference = worldPosition - new Vector2(selected.transform.position.x, selected.transform.position.y);

        selectedRB.AddForce(difference, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ball")
        {
            selected = collision.gameObject;
            selectedRB = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }
}
