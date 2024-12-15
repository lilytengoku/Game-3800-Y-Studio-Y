using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneMusic : MonoBehaviour
{
    private bool isCutscene;
    private AudioSource play;
    [SerializeField] private AudioClip mainMusic;
    [SerializeField] private AudioClip cutsceneMusic;
    // Start is called before the first frame update
    void Start()
    {
        play = GetComponent<AudioSource>();
        isCutscene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCutscene && play.clip != cutsceneMusic)
        {
            play.clip = cutsceneMusic;
            play.Play();
        }
        else if (!isCutscene && play.clip != mainMusic)
        {
            play.clip = mainMusic;
            play.Play();
        }
    }

    public void toggleCutscene() {
        isCutscene = !isCutscene;
    }

    public void toggleCutscene(bool toggle)
    {
        isCutscene = toggle;
    }
}
