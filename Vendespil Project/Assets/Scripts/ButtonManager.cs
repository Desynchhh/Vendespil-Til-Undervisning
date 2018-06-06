using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;

public class ButtonManager : MonoBehaviour
{
    [Header("Data")]
    public int id;
    public string question, answer, wrongAnswer1, wrongAnswer2, wrongAnswer3;
    public bool isAnswered = false;

    [Header("Element & Attribute names")]
    private string QuestionElement = "Question";
    private string idAttribute = "id";
    private string questionElement = "question";
    private string rightAnswerElement = "rightAnswer";
    private string wrongAnswer1Element = "wrongAnswer1";
    private string wrongAnswer2Element = "wrongAnswer2";
    private string wrongAnswer3Element = "wrongAnswer3";

    private string filePath;

    private void Awake()
    {
         filePath = Application.persistentDataPath + "/Data.xml";
    }

    public void LoadInfo(int idToFind)
    {
        XDocument xdoc = XDocument.Load(filePath);
        xdoc.Descendants(QuestionElement).Where(q => int.Parse(q.Attribute(idAttribute).Value) == idToFind).Select(q => new
        {
            id = q.Attribute(idAttribute).Value,
            question = q.Element(questionElement).Value,
            answer = q.Element(rightAnswerElement).Value,
            wrongAnswer1 = q.Element(wrongAnswer1Element).Value,
            wrongAnswer2 = q.Element(wrongAnswer2Element).Value,
            wrongAnswer3 = q.Element(wrongAnswer3Element).Value
        }).ToList().ForEach(q =>
        {
            Debug.Log("id: " + q.id.ToString() + ", " + q.question.ToString());
            id = int.Parse(q.id);
            question = q.question.ToString();
            answer = q.answer.ToString();
            wrongAnswer1 = q.wrongAnswer1.ToString();
            wrongAnswer2 = q.wrongAnswer2.ToString();
            wrongAnswer3 = q.wrongAnswer3.ToString();
        });
    }
}
