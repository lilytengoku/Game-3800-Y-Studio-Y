using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class EntityController : MonoBehaviour
{
    protected Vector2 velocity;
    protected float movementSpeed;
    protected Rigidbody2D rb;
    protected FacingDirection facing;
    [SerializeField] protected GameObject sprite;
    private void Move()
    {
        rb.MovePosition(rb.position + velocity);
    }
    private void CalculateVelocity() {
        velocity *= movementSpeed/32 * (Time.deltaTime * 60);
    }
    public abstract void EntityBehavior();
    public abstract void EntityInitialize();

    protected void Update()
    {
        EntityBehavior();
        CalculateVelocity();
        SetSpriteDirection();
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facing = new FacingDirection();
        movementSpeed = 0;
        velocity = new Vector2(0, 0);
        EntityInitialize();
    }

    protected void SetSpriteDirection()
    {
        float angle = 0;
        if (facing.GetVector() == Vector3.right)
        {
            angle = 90;
        }
        else if (facing.GetVector() == Vector3.up)
        {
            angle = 180;
        }
        else if (facing.GetVector() == Vector3.left)
        {
            angle = 270;
        }
        else if (facing.GetVector() == Vector3.down)
        {
            angle = 0;
        }
        sprite.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, 0, angle), sprite.transform.rotation, Time.deltaTime);
    }
}
