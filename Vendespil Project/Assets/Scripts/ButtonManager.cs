using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;

public class ButtonManager : MonoBehaviour
{
    [Header("Data")]
    public int totalQuestions;
    public int id;
    public string question, answer, wrongAnswer1, wrongAnswer2, wrongAnswer3;

    [Header("Element & Attribute names")]
    private string QuestionElement = "Question";
    private string IdAttribute = "id";
    private string questionElement = "question";
    private string rightAnswerElement = "rightAnswer";
    private string wrongAnswer1Element = "wrongAnswer1";
    private string wrongAnswer2Element = "wrongAnswer2";
    private string wrongAnswer3Element = "wrongAnswer3";

    public void LoadInfo()
    {
        totalQuestions = GameObject.FindGameObjectsWithTag("QuestionButton").Length;
        XDocument xdoc = XDocument.Load(Application.dataPath + "/XML/Data.xml");
        xdoc.Descendants(QuestionElement).Where(el => int.Parse(el.Attribute(IdAttribute).Value) == totalQuestions).Select(el => new
        {
            id = el.Attribute(IdAttribute).Value,
            question = el.Element(questionElement).Value,
            answer = el.Element(rightAnswerElement).Value,
            wrongAnswer1 = el.Element(wrongAnswer1Element).Value,
            wrongAnswer2 = el.Element(wrongAnswer2Element).Value,
            wrongAnswer3 = el.Element(wrongAnswer3Element).Value
        }).ToList().ForEach(el =>
        {
            //Debug.Log("xml id: " + el.id.ToString());
            //Debug.Log("xml question: " + el.question.ToString());
            //Debug.Log("xml answer: " + el.answer.ToString());
            //Debug.Log("xml wrong answer 1: " + el.wrongAnswer1.ToString());
            //Debug.Log("xml wrong answer 2: " + el.wrongAnswer2.ToString());
            //Debug.Log("xml wrong answer 3: " + el.wrongAnswer3.ToString());
            id = int.Parse(el.id);
            question = el.question.ToString();
            answer = el.answer.ToString();
            wrongAnswer1 = el.wrongAnswer1.ToString();
            wrongAnswer2 = el.wrongAnswer2.ToString();
            wrongAnswer3 = el.wrongAnswer3.ToString();
        });
        //Debug.Log("total questions: " + totalQuestions.ToString());
    }
}
