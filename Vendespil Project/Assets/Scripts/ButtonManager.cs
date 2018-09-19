using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [Header("Data")]
    public int id;
    public string question, answer, wrongAnswer1, wrongAnswer2, wrongAnswer3;
    public bool isAnswered = false;
}
