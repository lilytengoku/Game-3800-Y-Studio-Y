using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FacingDirection;

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

    private float currentMovement;
    private float rotationTimer;
    public override void EntityBehavior()
    {
        EnemyBehavior();
        EnemyLineOfSight();
    }

    public override void EntityInitialize()
    {
        facing.setDir(StartDirection);
        movementSpeed = 0;
        currentMovement = StartLength;
        rotationTimer = 0;
    }

    private void EnemyBehavior() {
        velocity = facing.GetVector();
        if (facing.GetVector().x != 0)
        {
            if (currentMovement >= MoveLength.x)
            {
                movementSpeed = 0;
                if (rotationTimer >= RotateTime)
                {
                    rotationTimer = 0;
                    facing.Rotate(EdgeRotations);
                    currentMovement = 0;
                }
                rotationTimer += Time.deltaTime;
            }
            else
            {
                movementSpeed = MoveSpeed.x;
                if (currentMovement + (movementSpeed / 32 * (Time.deltaTime * 60)) > MoveLength.x)
                {
                    movementSpeed = Mathf.Max(0, (MoveLength.x - currentMovement) * 32);
                }
                currentMovement += movementSpeed / 32 * (Time.deltaTime * 60);
            }
        }
        else if (facing.GetVector().y != 0) {
            if (currentMovement >= MoveLength.y)
            {
                movementSpeed = 0;
                if (rotationTimer >= RotateTime)
                {
                    rotationTimer = 0;
                    facing.Rotate(EdgeRotations);
                    currentMovement = 0;
                }
                rotationTimer += Time.deltaTime;
            }
            else
            {
                movementSpeed = MoveSpeed.y;
                if (currentMovement + (movementSpeed / 32 * (Time.deltaTime * 60)) > MoveLength.y)
                {
                    movementSpeed = Mathf.Max(0, (MoveLength.y - currentMovement) * 32);
                }
                currentMovement += movementSpeed / 32 * (Time.deltaTime * 60);
            }
        }
    }
    private void EnemyLineOfSight()
    {
        RaycastHit2D lineOfSight = Physics2D.BoxCast(transform.position, new Vector2(24, LineOfSight * 32), 0, facing.GetVector(), LayerMask.GetMask("Wall", "Player"));
        if (lineOfSight) {
            Debug.Log("Hit!");
        }
    }
}
