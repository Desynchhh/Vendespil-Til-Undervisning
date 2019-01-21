using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
