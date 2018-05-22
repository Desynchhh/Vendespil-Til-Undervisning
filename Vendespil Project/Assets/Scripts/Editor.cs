using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class Editor : MonoBehaviour
{
    [Header("UI")]
    public InputField QuestionText;
    public InputField RightAswer;
    public InputField WrongAnswer1;
    public InputField WrongAnswer2;
    public InputField WrongAnswer3;

    [HideInInspector]
    public QuestionDatabase itemDB;
    [HideInInspector]
    public int Number;
    private XmlTextReader reader;
    private XmlTextWriter writer;
    private string filePath;
    [HideInInspector]
    public List<int> idNummer = new List<int>();

    private void Start()
    {
        filePath = Application.persistentDataPath + "/Data.xml";
        reader = new XmlTextReader(filePath);
    }


    public void NumberOfItems()
    {
        if (File.ReadAllLines(filePath).Length > 0)
        {
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
                            default:
                                break;
                        }
                        break;
                }
            }

            if (idNummer.Count > 0)
            {
                Number = idNummer.Max();
                idNummer.Clear();
            }
            reader.Close();
        }
        else
        {
            Debug.Log("File is empty");
            Number = 0;
        }
    }

    public void Button()
    {
        if (File.Exists(filePath))
        {
            NumberOfItems();
            UpdateXML();
            Question a = new Question();
            a.IdNumber = Number + 1;
            a.question = QuestionText.text;
            a.rightAnswer = RightAswer.text;
            a.wrongAnswer1 = WrongAnswer1.text;
            a.wrongAnswer2 = WrongAnswer2.text;
            a.wrongAnswer3 = WrongAnswer3.text;
            itemDB.list.Add(a);

            XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
            FileStream stream = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(stream, itemDB);
            stream.Close();
            Debug.Log("File updated, new question id is " + a.IdNumber + ".");
        }
        else
        {
            Question a = new Question();
            a.IdNumber = 1;
            a.question = QuestionText.text;
            a.rightAnswer = RightAswer.text;
            a.wrongAnswer1 = WrongAnswer1.text;
            a.wrongAnswer2 = WrongAnswer2.text;
            a.wrongAnswer3 = WrongAnswer3.text;
            itemDB.list.Add(a);

            XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
            FileStream stream = new FileStream(filePath, FileMode.CreateNew);
            serializer.Serialize(stream, itemDB);
            stream.Close();
            Debug.Log("File created");
        }
    }

    public void UpdateXML()
    {
        if (File.ReadAllLines(filePath).Length > 0)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
            FileStream stream = new FileStream(filePath, FileMode.Open);
            itemDB = serializer.Deserialize(stream) as QuestionDatabase;
            stream.Close();
        }
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

    public void RemoveAll()
    {
        itemDB.list.Clear();
        File.WriteAllText(filePath, "");
        Debug.Log("File clear");
    }

    public void RemoveSingle(int ID)
    {
        if (File.Exists(filePath) && File.ReadAllLines(filePath).Length > 0)
        {
            string node = "//Question[@id='" + ID + "']";
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNodeList nodes = doc.SelectNodes(node);
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes[i].ParentNode.RemoveChild(nodes[i]);
            }
            doc.Save(filePath);
        }
    }
}