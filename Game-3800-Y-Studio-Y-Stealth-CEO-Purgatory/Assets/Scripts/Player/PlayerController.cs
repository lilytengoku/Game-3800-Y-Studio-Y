using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 velocity;
    private Rigidbody2D rb;
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float SprintSpeed;
    private bool isSprint;
    private float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementSpeed = WalkSpeed;
        isSprint = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move() {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");
        velocity.Normalize();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprint = true;
            movementSpeed = SprintSpeed;
        }
        else
        {
            movementSpeed = WalkSpeed;
            isSprint = false;
        }
        velocity *= movementSpeed * (Time.deltaTime * 60);
        rb.velocity = velocity;
    }
}
