using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Diagnostics;

public class DialogueBox : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string[] lines;
    [SerializeField] private TextMeshProUGUI textbox;
    [SerializeField] private PlayerController player;
    [SerializeField] private int boxNumber;
    [SerializeField] private bool required = true;
    [SerializeField] private Image uiImage;
    [SerializeField] private Sprite image;
    [SerializeField] private bool hasImage = false;
    private CutsceneMusic cs;
    private int currLine;
    private bool textStart;
    private bool isActive;

    private void Start()
    {
        textStart = false;
        isActive = gameObject.activeInHierarchy;
        cs = FindObjectOfType<CutsceneMusic>();
    }
    private void Update()
    {
        if (textStart)
        {
            player.SetInput(false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currLine++;
            }
            if (currLine == lines.Length)
            {
                textStart = false;
                textbox.text = "";
                player.SetInput(true);
                CollectedAllDialogue.CountDialogue();
                DataPersistenceManager.instance.SaveGame();
                if (hasImage)
                {
                    uiImage.enabled = false;
                }
                cs.toggleCutscene(false);
                gameObject.SetActive(false);
            }
            else
            {
                textbox.text = lines[currLine];
                if (hasImage)
                {
                    uiImage.enabled = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        textStart = true;
        AudioSource paperSfx = GetComponent<AudioSource>();
        paperSfx.Play();
        if (hasImage)
        {
            cs.toggleCutscene(true);
            uiImage.sprite = image;
        }
    }

    public void LoadData(GameData data) {
        // load active notes
        if (data.collectedNotes.Contains(boxNumber))
        {
            UnityEngine.Debug.Log("Loaded inactive box #" + boxNumber);
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data) {
        // save active notes
        if (!gameObject.activeInHierarchy)
        {
            data.collectedNotes.Add(boxNumber);
        }
    }
}
