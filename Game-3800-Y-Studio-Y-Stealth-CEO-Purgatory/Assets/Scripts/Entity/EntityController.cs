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
    protected Vector2 angleDirection;
    [SerializeField] protected GameObject sprite;
    private void Move()
    {
        rb.MovePosition(rb.position + velocity);
        PostMove();
    }
    private void CalculateVelocity() {
        float tempSpeed = movementSpeed;
        tempSpeed /= 32;
        velocity *= tempSpeed;
    }
    public abstract void EntityBehavior();
    public abstract void EntityInitialize();

    public virtual void PostMove() { }

    protected void FixedUpdate()
    {
        EntityBehavior();
        CalculateVelocity();
        SetSpriteDirection(angleDirection);
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

    protected virtual void SetSpriteDirection(Vector2 dir)
    {
        float angle = 0;
        if (dir.x > 0)
        {
            if (dir.y > 0)
            {
                angle = 45;
            }
            else if (dir.y < 0)
            {
                angle = 315;
            }
            else angle = 0;
        }
        else if (dir.x < 0)
        {
            if (dir.y > 0)
            {
                angle = 135;
            }
            else if (dir.y < 0)
            {
                angle = 225;
            }
            else angle = 180;
        }
        else {
            if (dir.y > 0)
            {
                angle = 90;
            }
            else if (dir.y < 0)
            {
                angle = 270;
            }
            else angle = sprite.transform.rotation.z - 90;
        }
        angle += 90;
        sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, Quaternion.Euler(0, 0, angle), 9f * 1f/60f);
    }

}
