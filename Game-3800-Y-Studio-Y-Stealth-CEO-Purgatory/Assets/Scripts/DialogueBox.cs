using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string[] lines;
    [SerializeField] private TextMeshProUGUI textbox;
    [SerializeField] private PlayerController player;
    [SerializeField] private bool required = true;
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

    public void LoadData(GameData data) {
        Debug.Log("Loading saved position: " + data.playerPosition);
        player.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data) {
        Vector3 pos = player.transform.position;
        //Debug.Log("Player pos:" + pos + "\n");
        data.playerPosition = pos;
    }
}
