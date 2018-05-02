using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class Editor : MonoBehaviour {

    [Header("UI")]
    public InputField QuestionText;
    public InputField RightAswer;
    public InputField WrongAnswer1;
    public InputField WrongAnswer2;
    public InputField WrongAnswer3;

    [Header("MISC")]
    public QuestionDatabase itemDB;
    public int Number;
    private XmlTextReader reader;
    private string filePath;
    public List<int> idNummer = new List<int>();

    private void Start()
    {

    }


    public void NumberOfItems()
    {
        filePath = Application.dataPath + "/XML/Data.xml";
        reader = new XmlTextReader(filePath);
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    switch (reader.Name)
                    {
                        case "Question":
                            idNummer.Add(System.Convert.ToInt32(reader.GetAttribute("id")));
                            break;
                    }
                    break;

                case XmlNodeType.EndElement:
                    switch (reader.Name)
                    {
                    }
                    break;
            }
        }

        if(idNummer.Count > 0)
        {
            Number = idNummer.Max();
            idNummer.Clear();
        }
        reader.Close();
    }

    public void Button()
    {
        NumberOfItems();
        Question a = new Question();
        a.IdNumber = Number + 1;
        a.question = QuestionText.text;
        a.rightAnswer = RightAswer.text;
        a.wrongAnswer1 = WrongAnswer1.text;
        a.wrongAnswer2 = WrongAnswer2.text;
        a.wrongAnswer3 = WrongAnswer3.text;
        itemDB.list.Add(a);
        Debug.Log(Number);

        XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/XML/Data.xml", FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();

        //Add(QuestionText.text, RightAswer.text, WrongAnswer1.text, WrongAnswer2.text, WrongAnswer3.text);
    }



    [System.Serializable]

    public class Question
    {
        [XmlAttribute("id")]
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

    public void Read()
    {
        filePath = Application.dataPath + "/XML/Data.xml";
        reader = new XmlTextReader(filePath);
        int Tempid = 0;
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    switch (reader.Name)
                    {
                        case "Question":
                            Tempid = int.Parse(reader.GetAttribute("id"));
                            idNummer.Add(System.Convert.ToInt32(reader.GetAttribute("id")));
                            break;
                        case "question":
                            Debug.Log(Tempid + ": " + reader.ReadString());
                            break;
                        case "rightAnswer":
                            Debug.Log(Tempid + ": " + reader.ReadString());
                            break;
                        case "wrongAnswer1":
                            Debug.Log(Tempid + ": " + reader.ReadString());
                            break;
                        case "wrongAnswer2":
                            Debug.Log(Tempid + ": " + reader.ReadString());
                            break;
                        case "wrongAnswer3":
                            Debug.Log(Tempid + ": " + reader.ReadString());
                            break;
                    }
                    break;

                case XmlNodeType.EndElement:
                    switch (reader.Name)
                    {
                    }
                    break;
            }

        }
        if(idNummer != null)
        {
            Number = idNummer.Max();
            idNummer.Clear();
        }
        reader.Close();
    }
}