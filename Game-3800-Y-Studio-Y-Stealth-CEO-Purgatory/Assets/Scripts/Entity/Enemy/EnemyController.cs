using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FacingDirection;
using static PauseOnEscape;

public class EnemyController : EntityController
{
    [Tooltip("The amount of rotation that the enemy does when reaching its endpoint by 90 degrees. Negative means counterclockwise.")]
    [SerializeField] private int EdgeRotations;
    [Tooltip("The direction the enemy starts in.")]
    [SerializeField] private Dir StartDirection;
    [Tooltip("The amount of time the enemy takes to complete its rotations.")]
    [SerializeField] private float RotateTime;
    [Tooltip("The speed at which the enemy travels in the x and y directions. Set them equal if you desire.")]
    [SerializeField] private Vector2 MoveSpeed;
    [Tooltip("The length that the enemy travels in the x and y directions.")]
    [SerializeField] private Vector2 MoveLength;
    [Tooltip("The length that the enemy starts having already have traveled. Use if you want the enemy to start between two endpoints. Place the enemy in said position.")]
    [SerializeField] private float StartLength;
    [Tooltip("Sets the enemy's base line of sight. Perhaps should be identical across all enemies.")]
    [SerializeField] private float LineOfSight;
    private LineRenderer lr;

    private float currentMovement;
    private float rotationTimer;
    private CircleCollider2D collide;

    public override void EntityBehavior()
    {
        EnemyBehavior();
        EnemyLineOfSight();
    }

    public override void EntityInitialize()
    {
        lr = GetComponent<LineRenderer>();
        collide = GetComponent<CircleCollider2D>();
        facing.setDir(StartDirection);
        angleDirection = facing.GetVector();
        movementSpeed = 0;
        currentMovement = StartLength * 32;
        rotationTimer = 0;
        MoveLength *= 32;
    }

    private void EnemyBehavior() {
        velocity = facing.GetVector();
        if (facing.GetVector().x != 0)
        {
            if (currentMovement >= MoveLength.x)
            {
                movementSpeed = 0;
                rotationTimer += 1f/60f;
                if (rotationTimer >= RotateTime)
                {
                    rotationTimer = 0;
                    facing.Rotate(EdgeRotations);
                    angleDirection = facing.GetVector();
                    currentMovement = 0;
                }
                //Debug.Log(rotationTimer);
            }
            else
            {
                movementSpeed = MoveSpeed.x;
                currentMovement += movementSpeed;
                if (currentMovement >= MoveLength.x)
                {
                    movementSpeed -= (currentMovement - MoveLength.x);
                }
            }
        }
        else if (facing.GetVector().y != 0) {
            if (currentMovement >= MoveLength.y)
            {
                movementSpeed = 0;
                rotationTimer += 1f / 60f;
                if (rotationTimer >= RotateTime)
                {
                    rotationTimer = 0;
                    facing.Rotate(EdgeRotations);
                    angleDirection = facing.GetVector();
                    currentMovement = 0;
                }
            }
            else
            {
                movementSpeed = MoveSpeed.y;
                currentMovement += movementSpeed;
                if (currentMovement >= MoveLength.y)
                {
                    movementSpeed -= (currentMovement - MoveLength.y);
                }
            }
            //Debug.Log(rotationTimer);
        }
    }
    private void EnemyLineOfSight()
    {
        RaycastHit2D lineOfSight = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0, facing.GetVector(), LineOfSight - 0.25f, LayerMask.GetMask("Wall", "Player", "Enemy"));
        lr.material.color = new Color(lr.material.color.r, lr.material.color.g, lr.material.color.b, 0.3f);
        lr.SetPosition(0, transform.position);
        if (lineOfSight &&
            (lineOfSight.collider.gameObject.layer == LayerMask.NameToLayer("Wall") ||
            lineOfSight.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")))
        {
            lr.SetPosition(1, lineOfSight.point);
            if (facing.GetVector() == Vector3.right || facing.GetVector() == Vector3.left)
                lr.endWidth = Mathf.Abs(transform.position.x - lineOfSight.point.x) / LineOfSight;
            else if (facing.GetVector() == Vector3.down || facing.GetVector() == Vector3.up)
                lr.endWidth = Mathf.Abs(transform.position.y - lineOfSight.point.y) / LineOfSight;
        }
        else
        {
            lr.SetPosition(1, transform.position + facing.GetVector() * LineOfSight);
            lr.endWidth = 1f;
        }
        if (lineOfSight && lineOfSight.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerController pc = lineOfSight.collider.gameObject.GetComponent<PlayerController>();
            if (pc != null && !pc.IsGameOver()) {
                pc.DoGameOver();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && pc != null) {
            if (!pc.IsGameOver()) pc.DoGameOver();
        }
    }
}
