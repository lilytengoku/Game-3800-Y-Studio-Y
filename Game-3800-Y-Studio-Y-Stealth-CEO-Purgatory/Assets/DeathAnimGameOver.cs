using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimGameOver : MonoBehaviour
{
    private Animator animator;
    private GameObject pauseManager;
    private PlayerController pc;
    private AudioSource scream;
    void Start()
    {
        animator = GetComponent<Animator>();
        pc = GetComponentInParent<PlayerController>();
        pauseManager = GameObject.Find("PauseButton");
        scream = GetComponent<AudioSource>();
    }
    void Die()
    {
        if (pauseManager != null)
        {
            // Get the PauseOnEscape component
            pauseManager.GetComponent<PauseOnEscape>().gameOver();
            animator.SetBool("IsDead", false);
            pc.EndGameOver();
            pc.SetInput(true);
        }
    }

    void Scream()
    {
        scream.Play();
    }
}
