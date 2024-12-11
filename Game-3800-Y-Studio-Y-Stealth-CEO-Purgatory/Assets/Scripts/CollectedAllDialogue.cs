using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectedAllDialogue : MonoBehaviour
{
    private static int numCollected;
    private static int totalDialogueCount;

    private void Start() {
        totalDialogueCount = 6;
        numCollected = 0;
        Debug.Log("num remaining: " + totalDialogueCount);
    }

    public static void CountDialogue() {
        numCollected++;
        CheckCollectedAll();
        Debug.Log("num remaining: " + (totalDialogueCount - numCollected));
    }

    private static void CheckCollectedAll() {
        if (numCollected == totalDialogueCount) {
            ManagingOfTheScenes.goToScene(0);
        }
    }
}
