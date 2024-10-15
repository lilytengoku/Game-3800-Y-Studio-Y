using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private string[] lines;
    [SerializeField] private TextMeshProUGUI textbox;
    [SerializeField] private PlayerController player;
    private int currLine;
    private bool textStart;

    private void Start()
    {
        textStart = false;
    }
    private void Update()
    {
        if (textStart)
        {
            player.SetMove(false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currLine++;
            }
            if (currLine == lines.Length)
            {
                textStart = false;
                textbox.text = "";
                player.SetMove(true);
                Destroy(gameObject);
            }
            else
            {
                textbox.text = lines[currLine];
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        textStart = true;
    }
}
