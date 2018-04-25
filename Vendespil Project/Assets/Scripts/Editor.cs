using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class Editor : MonoBehaviour {

    [Header("UI")]
    public InputField QuestionText;
    public InputField RightAswer;
    public InputField WrongAnswer1;
    public InputField WrongAnswer2;
    public InputField WrongAnswer3;

    [Header("MISC")]
    public QuestionDatabase itemDB;

    public void Button()
    {
        Question a = new Question();
        a.IdNumber = 0;
        a.question = QuestionText.text;
        a.rightAnswer = RightAswer.text;
        a.wrongAnswer1 = WrongAnswer1.text;
        a.wrongAnswer2 = WrongAnswer2.text;
        a.wrongAnswer3 = WrongAnswer3.text;
        itemDB.list.Add(a);

        XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/XML/Data.xml", FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();


        //Add(QuestionText.text, RightAswer.text, WrongAnswer1.text, WrongAnswer2.text, WrongAnswer3.text);
    }



    [System.Serializable]
    public class Question
    {
        public int IdNumber;
        public string question;
        public string rightAnswer;
        public string wrongAnswer1;
        public string wrongAnswer2;
        public string wrongAnswer3;
    }

    [System.Serializable]
    public class QuestionDatabase
    {
        public List<Question> list = new List<Question>();
    }
}
