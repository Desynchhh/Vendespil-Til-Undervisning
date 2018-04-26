using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class ShowXML : MonoBehaviour {

    public GameObject prefab;
    public GameObject holder;
    public QuestionDatabase itemDB;

    public void LoadItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/XML/Data.xml", FileMode.Open);
        itemDB = serializer.Deserialize(stream) as QuestionDatabase;
        stream.Close();

        foreach (Question temp in itemDB.list)
        {
            Vector3 temppos = holder.transform.position;
            GameObject go = Instantiate(prefab, temppos, Quaternion.identity) as GameObject;
            go.transform.parent = holder.transform;

            go.GetComponent<Brik>().IdNumber = temp.IdNumber;
            go.GetComponent<Brik>().question = temp.question;
            go.GetComponent<Brik>().rightAnswer = temp.rightAnswer;
            go.GetComponent<Brik>().wrongAnswer1 = temp.wrongAnswer1;
            go.GetComponent<Brik>().wrongAnswer2 = temp.wrongAnswer2;
            go.GetComponent<Brik>().wrongAnswer3 = temp.wrongAnswer3;
        }
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
