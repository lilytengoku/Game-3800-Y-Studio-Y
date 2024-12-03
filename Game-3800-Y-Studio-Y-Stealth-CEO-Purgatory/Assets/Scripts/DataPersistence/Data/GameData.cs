using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int activeScene;
    public List<int> collectedNotes;
    public bool playerAlive;

    public GameData() {
        playerPosition = new Vector3(-8.5f, 13.2f, 0);
        this.activeScene = 1;
        collectedNotes = new List<int>();
        playerAlive = true; // NEVER SET TO FALSE
    }

}
