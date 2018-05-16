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

    private string filePath;
    private XmlTextReader reader;
    [HideInInspector]
    public QuestionDatabase itemDB;
    private int TempID;

    private void Start()
    {
        filePath = Application.dataPath + "/XML/Data.xml";
        reader = new XmlTextReader(filePath);
    }

    public void Add()
    {
        if (File.Exists(filePath))
        {
            Debug.Log("dssdas");
            GetID();
            Question a = new Question();
            a.IdNumber = TempID + 1;
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
            FileStream stream = new FileStream(Application.dataPath + "/XML/Data.xml", FileMode.CreateNew);
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
            FileStream stream = new FileStream(Application.dataPath + "/XML/Data.xml", FileMode.Open);
            itemDB = serializer.Deserialize(stream) as QuestionDatabase;
            stream.Close();
        }
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
        }
    }

    public void GetID()
    {
        if (File.ReadAllLines(filePath).Length > 0)
        {
            List<int> TempList = new List<int>();
            reader = new XmlTextReader(filePath);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "Question":
                                TempList.Add(System.Convert.ToInt32(reader.GetAttribute("id")));
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

            if (TempList.Count > 0)
            {
                TempID = TempList.Max();
                TempList.Clear();
            }
            reader.Close();
        }
        else
        {
            Debug.Log("File is empty");
            TempID = 0;
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
