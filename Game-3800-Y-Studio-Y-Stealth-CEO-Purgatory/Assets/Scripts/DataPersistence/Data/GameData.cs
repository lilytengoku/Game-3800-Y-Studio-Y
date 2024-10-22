using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int activeScene;

    public GameData() {
        playerPosition = new Vector3(-10.5f, -5.5f, 0);
        this.activeScene = 0;
    }

}
