using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour, IDataPersistence
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
                CollectedAllDialogue.CountDialogue();
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
        DataPersistenceManager.instance.SaveGame();
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
