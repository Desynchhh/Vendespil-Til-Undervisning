using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;
using System.IO;

public class GameStart : MonoBehaviour
{
    [Header("Panels")]
    public GameObject MainMenu;
    public GameObject NoQuestionsWarningMenu;
    public GameObject EditMenu;
    public GameObject CreateMenu;
    public GameObject QuestionMenu;
    public GameObject ResultsMenu;
    public GameObject EditQuestionMenu;
    public GameObject LoginMenu;
    public GameObject PickList;

    private string filePath;

    public void Awake()
    {
        EditMenu.SetActive(false);
        CreateMenu.SetActive(false);
        QuestionMenu.SetActive(false);
        ResultsMenu.SetActive(false);
        EditQuestionMenu.SetActive(false);
        NoQuestionsWarningMenu.SetActive(false);
        MainMenu.SetActive(false);
        PickList.SetActive(false);
        LoginMenu.SetActive(true);
    }
}
