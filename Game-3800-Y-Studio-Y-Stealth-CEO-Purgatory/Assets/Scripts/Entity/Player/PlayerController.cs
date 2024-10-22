using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : EntityController
{
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float SprintSpeed;
    [SerializeField] private float DisappearTime;
    [SerializeField] private float DisappearRecharge;
    private bool isSprint;
    private bool isDisappear;
    private bool doMove;
    private Collider2D collide;
    private float currDisappearTime;
    private float currDisappearRecharge;
    private SpriteRenderer spriteImage;
    [SerializeField] private TextMeshPro text;
    private void GetSprintFromInput() {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isSprint = true;
            movementSpeed = SprintSpeed;
        }
        else
        {
            movementSpeed = WalkSpeed;
            isSprint = false;
        }
    }

    private void GetDisappearFromInput() {
        if (Input.GetKey(KeyCode.Space) && currDisappearRecharge <= 0) {
            currDisappearTime += 1/60f;
            if (currDisappearTime >= DisappearTime) {
                currDisappearRecharge = DisappearRecharge;
            }
            isDisappear = true;
            collide.enabled = false;
            spriteImage.color = new Color(spriteImage.color.r, spriteImage.color.g, spriteImage.color.b, 0.25f);
        }
        else
        {
            isDisappear = false;
            collide.enabled = true;
            spriteImage.color = new Color(spriteImage.color.r, spriteImage.color.g, spriteImage.color.b, 1f);
            currDisappearTime -= 1 /120f;
            currDisappearTime = Mathf.Max(0, currDisappearTime);
        }
        currDisappearRecharge -= 1/60f;

        float v = DisappearTime - currDisappearTime;
        text.text = ((int)v) + "";
    }

    public void SetMove(bool doMove) {
        this.doMove = doMove;
    }
    private void SetMovementFromInput()
    {
        if (doMove && !isDisappear)
        {
            velocity.x = Input.GetAxisRaw("Horizontal");
            velocity.y = Input.GetAxisRaw("Vertical");
            if (velocity.x > 0)
            {
                facing.setRight();
            }
            else if (velocity.x < 0)
            {
                facing.setLeft();
            }
            else if (velocity.y > 0)
            {
                facing.setUp();
            }
            else if (velocity.y < 0)
            {
                facing.setDown();
            }
            velocity.Normalize();
        }
    }
    public override void EntityBehavior()
    {
        velocity = Vector2.zero;
        GetDisappearFromInput();
        SetMovementFromInput();
        GetSprintFromInput();
    }

    public override void EntityInitialize()
    {
        movementSpeed = WalkSpeed;
        isSprint = false;
        doMove = true;
        collide = GetComponent<Collider2D>();
        currDisappearTime = 0;
        currDisappearRecharge = 0;
        spriteImage = sprite.GetComponent<SpriteRenderer>();
    }

}
