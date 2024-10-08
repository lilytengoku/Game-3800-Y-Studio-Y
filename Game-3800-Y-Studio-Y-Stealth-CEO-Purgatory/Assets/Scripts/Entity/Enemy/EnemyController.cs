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
        currentMovement = StartLength * 32;
        rotationTimer = 0;
        MoveLength *= 32;
        MoveLength += new Vector2(16, 16);
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
                    currentMovement = 0;
                }
                Debug.Log(rotationTimer);
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
                    currentMovement = 0;
                }
            }
            else
            {
                movementSpeed = MoveSpeed.y;
                currentMovement += movementSpeed;
                if (currentMovement >= MoveLength.x)
                {
                    movementSpeed -= (currentMovement - MoveLength.y);
                }
            }
            Debug.Log(rotationTimer);
        }
    }
    private void EnemyLineOfSight()
    {
        RaycastHit2D lineOfSight = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0, facing.GetVector(), LineOfSight, LayerMask.GetMask("Wall", "Player"));
        if (lineOfSight && lineOfSight.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Application.Quit();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(Vector3.zero), Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.TRS(transform.position + LineOfSight * facing.GetVector(), Quaternion.Euler(Vector3.zero), Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
