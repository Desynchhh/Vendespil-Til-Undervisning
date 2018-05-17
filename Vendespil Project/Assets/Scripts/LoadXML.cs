using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class LoadXML : MonoBehaviour {

    public GameObject Prefab;
    public GameObject Canvas;

    private string filePath;

    //[HideInInspector]
    public QuestionDatabase itemDB;

    private void Start()
    {
        filePath = Application.dataPath + "/Resources/XML/Data.xml";
        Load();
    }

    public void Load()
    {
        if (File.ReadAllLines(filePath).Length > 0)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
            FileStream stream = new FileStream(filePath, FileMode.Open);
            itemDB = serializer.Deserialize(stream) as QuestionDatabase;
            stream.Close();
        }
        foreach (Question temp in itemDB.list)
        {
            //Debug.Log(temp.IdNumber);
            GameObject newButton = Instantiate(Prefab) as GameObject;
            newButton.transform.SetParent(Canvas.transform, false);
            newButton.GetComponent<Brik>().LoadXML(temp.IdNumber);
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
