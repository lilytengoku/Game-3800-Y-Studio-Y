using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimGameOver : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Die()
    {
        PlayerController pc = GetComponentInParent<PlayerController>();
        GameObject pauseManager = GameObject.Find("PauseButton");
        if (pauseManager != null)
        {
            // Get the PauseOnEscape component
            pauseManager.GetComponent<PauseOnEscape>().gameOver();
            animator.SetBool("IsDead", false);
            pc.EndGameOver();
            pc.SetInput(true);
        }
    }
}
