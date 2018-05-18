using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class AddXML : MonoBehaviour
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
    private string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/Resources/XML/Data.xml";
        Debug.Log(filePath);
    }

    public void Add()
    {
        if (File.Exists(filePath) == true)
        {
            UpdateXML();
            Question a = new Question();
            a.IdNumber = GetHighestId() + 1;
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
            Debug.Log("File created, the id is 1!");
        }
    }

    public int GetHighestId()
    {
        if (File.Exists(filePath) && File.ReadAllLines(filePath).Length > 0)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            List<int> attributes = new List<int>();
            XmlNodeList aNodes = doc.SelectNodes("//*[@id]");

            if (aNodes != null)
            {
                foreach (XmlNode aNode in aNodes)
                {
                    attributes.Add(System.Convert.ToInt32(aNode.Attributes["id"].Value));
                }
                int id = attributes.Max();
                attributes.Clear();
                return id;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
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
}
