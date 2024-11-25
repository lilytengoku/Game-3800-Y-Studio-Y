using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : EntityController
{
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float DisappearTime;
    [SerializeField] private float DisappearRecharge;
    private bool isDisappear;
    private bool doInput;
    private Collider2D collide;
    private float currDisappearTime;
    private float currDisappearRecharge;
    private SpriteRenderer spriteImage;
    private Animator spriteAnimator;
    private LineRenderer lr;
    private float flasher = 0f;
    bool gameover;

    private void GetDisappearFromInput() {
        if (Input.GetKey(KeyCode.Space) && currDisappearRecharge <= 0 && doInput) {
            currDisappearTime += 1/60f;
            if (currDisappearTime >= DisappearTime) {
                currDisappearRecharge = DisappearRecharge;
            }
            isDisappear = true;
            collide.enabled = false;
            float alpha = 0.25f;

            if (DisappearTime - currDisappearTime <= 1.5f)
            {
                flasher += 2f/7f;
                Debug.Log(flasher);
                alpha = Mathf.Sin(flasher) > 0 ? 0.75f : 0.25f;
            }
            else flasher = 0f;

            spriteImage.color = new Color(spriteImage.color.r, spriteImage.color.g, spriteImage.color.b, alpha);
        }
        else
        {
            flasher = 0f;
            isDisappear = false;
            collide.enabled = true;
            spriteImage.color = new Color(spriteImage.color.r, spriteImage.color.g, spriteImage.color.b, 1f);
            currDisappearTime -= 1 /120f;
            currDisappearTime = Mathf.Max(0, currDisappearTime);
        }
        currDisappearRecharge -= 1/60f;
    }

    public void SetInput(bool doInput) {
        this.doInput = doInput;
    }
    private void SetMovementFromInput()
    {
        if (doInput && !isDisappear)
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
        if (gameover) {
            SetInput(false);
            spriteAnimator.SetBool("IsDead", true);
        }
        velocity = Vector2.zero;
        GetDisappearFromInput();
        SetMovementFromInput();
    }

    private void DisappearMeterRender()
    {
        if(currDisappearTime == 0)
        {
            lr.material.color = new Color(lr.material.color.r, lr.material.color.g, lr.material.color.b, Mathf.Max(0, lr.material.color.a - 1f/60f));
        }
        else lr.material.color = new Color(lr.material.color.r, lr.material.color.g, lr.material.color.b, Mathf.Max(1, lr.material.color.a + 1f / 60f));
        lr.SetPosition(0, new Vector2(transform.position.x - 0.5f, transform.position.y - 0.625f));
        lr.SetPosition(1, new Vector2((transform.position.x - 0.5f) + Mathf.Clamp((DisappearTime - currDisappearTime)/DisappearTime, 0, 1), transform.position.y - 0.625f));
    }

    public override void EntityInitialize()
    {
        movementSpeed = WalkSpeed;
        doInput = true;
        collide = GetComponent<Collider2D>();
        currDisappearTime = 0;
        currDisappearRecharge = 0;
        spriteImage = sprite.GetComponent<SpriteRenderer>();
        spriteAnimator = sprite.GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
    }

    public override void PostMove() {
        DisappearMeterRender();
    }
    public void DoGameOver() {
        gameover = true;
    }

    public bool IsGameOver() {
        return gameover;
    }
}
