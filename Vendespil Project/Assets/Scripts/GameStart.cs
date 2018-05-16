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
    public GameObject EditMenu;
    public GameObject CreateMenu;
    public GameObject GameMenu;
    public GameObject QuestionMenu;
    public GameObject ResultsMenu;

    public void Awake()
    {
        if (File.Exists(Application.dataPath + "/XML/Data.xml") && File.ReadAllLines(Application.dataPath + "/XML/Data.xml").Length > 0)
            LoadAllQuestions();
        EditMenu.SetActive(false);
        GameMenu.SetActive(false);
        CreateMenu.SetActive(false);
        QuestionMenu.SetActive(false);
        ResultsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void LoadAllQuestions()
    {
        int counter = 1;
        XDocument xdoc = XDocument.Load(Application.dataPath + "/XML/Data.xml");

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
            //Debug.Log(counter);
        });
        counter++;
    }
}
