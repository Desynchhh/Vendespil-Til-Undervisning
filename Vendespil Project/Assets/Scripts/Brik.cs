using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brik : MonoBehaviour {

    [Header("UI")]
    public Text text;

    [Header("Data")]
    public int IdNumber;
    public string question;
    public string rightAnswer;
    public string wrongAnswer1;
    public string wrongAnswer2;
    public string wrongAnswer3;

    private void Start()
    {
        text.text = question;
    }
}
