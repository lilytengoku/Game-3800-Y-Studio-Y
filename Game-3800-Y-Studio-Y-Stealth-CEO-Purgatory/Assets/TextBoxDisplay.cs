using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextBoxDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI prompt;
    private Image panel;

    void Start()
    {
        panel = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        panel.enabled = text.text != "";
        prompt.enabled = panel.enabled;
    }
}