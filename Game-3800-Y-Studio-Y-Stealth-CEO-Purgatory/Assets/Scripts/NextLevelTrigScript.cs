using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider) {
        //Debug.Log("Collision:" + collider + collider.tag);
        if (collider.tag == "Player") {
            ManagingOfTheScenes.nextScene();
        }
    }
}
