using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class Brik : MonoBehaviour {

    [Header("UI")]
    public Text QuestionButtonText;
    public Text QuestionText;
    public GameObject AnswerButton;
    public GameObject wrongAnswer1Button;
    public GameObject wrongAnswer2Button;
    public GameObject wrongAnswer3Button;

    public List<RectTransform> ButtonList = new List<RectTransform>();

    public List<RectTransform> ButtonListNew = new List<RectTransform>();

    private string filePath;

    public void LoadXML(int EditID)
    {
        ButtonList.Add(AnswerButton.GetComponent<RectTransform>());
        ButtonList.Add(wrongAnswer1Button.GetComponent<RectTransform>());
        ButtonList.Add(wrongAnswer2Button.GetComponent<RectTransform>());
        ButtonList.Add(wrongAnswer3Button.GetComponent<RectTransform>());

        Shuffle();

        filePath = Application.dataPath + "/XML/Data.xml";
        if (File.Exists(filePath) && File.ReadAllLines(filePath).Length > 0)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            string node = "//Question[@id='" + EditID + "']";

            XmlDocument doc = new XmlDocument();
            doc.Load(fs);

            XmlNodeList aNodes = doc.SelectNodes(node);

            foreach (XmlNode aNode in aNodes)
            {
                XmlAttribute idAttribute = aNode.Attributes["id"];

                if (idAttribute != null)
                {
                    QuestionButtonText.text = aNode["question"].InnerText;
                    QuestionText.text = aNode["question"].InnerText;
                    AnswerButton.GetComponentInChildren<Text>().text = aNode["rightAnswer"].InnerText;
                    wrongAnswer1Button.GetComponentInChildren<Text>().text = aNode["wrongAnswer1"].InnerText;
                    wrongAnswer2Button.GetComponentInChildren<Text>().text = aNode["wrongAnswer2"].InnerText;
                    wrongAnswer3Button.GetComponentInChildren<Text>().text = aNode["wrongAnswer3"].InnerText;
                }
            }

        }
    }

    public void Shuffle()
    {
        Debug.Log(ButtonList.Count);
        for (int i = 0; i < ButtonList.Count; i++)
        {
            int rnd = Random.Range(0, ButtonList.Count);
            var tempGO = ButtonList[rnd];
            ButtonList[rnd] = ButtonList[i];
            ButtonList[i] = tempGO;
        }

        foreach(RectTransform rectTemp in ButtonList)
        {
            //Debug.Log(rectTemp.localPosition);
            //Debug.Log(rectTemp.localPosition);
            //AnswerButton.GetComponent<RectTransform>().localPosition = rectTemp.localPosition;
        }
    }
}
