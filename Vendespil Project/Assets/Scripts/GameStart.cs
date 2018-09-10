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
        Debug.Log("Setting file path..");
        filePath = Application.persistentDataPath + "/Data.xml";
        Debug.Log("File path set!");
        if (File.Exists(filePath) && File.ReadAllLines(filePath).Length > 0)
            LoadAllQuestions();

        Debug.Log("all questions loaded");
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

    public void LoadAllQuestions()
    {
        int counter = 1;
        XDocument xdoc = XDocument.Load(filePath);

        xdoc.Descendants("Question").Where(el => int.Parse(el.Attribute("id").Value) >= counter).Select(el => new
        {
            id = int.Parse(el.Attribute("id").Value),
            question = el.Element("question").Value,
            answer = el.Element("rightAnswer").Value,
            wrongAnswer1 = el.Element("wrongAnswer1").Value,
            wrongAnswer2 = el.Element("wrongAnswer2").Value,
            wrongAnswer3 = el.Element("wrongAnswer3").Value
        }).ToList().ForEach(el =>
        {
            transform.parent.Find("PanelEditMenu").GetComponent<CreateButton>().SpawnEditButton(el.id, el.question, el.answer, el.wrongAnswer1, el.wrongAnswer2, el.wrongAnswer3);
        });
        counter++;
    }
}
