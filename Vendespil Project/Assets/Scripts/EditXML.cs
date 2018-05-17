﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class EditXML : MonoBehaviour {

    [Header("Int")]
    public int ID;


    [Header("InputFields")]
    public InputField questionText;
    public InputField rightAnswerText;
    public InputField wrongAnswer1Text;
    public InputField wrongAnswer2Text;
    public InputField wrongAnswer3Text;

    private string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/XML/Data.xml";
        //LoadXML(ID);
    }

    public void ButtonPress()
    {
        Save(ID, questionText.text, rightAnswerText.text, wrongAnswer1Text.text, wrongAnswer2Text.text, wrongAnswer3Text.text);
    }
    
    public void LoadXML(int EditID)
    {
        if (File.Exists(filePath) && File.ReadAllLines(filePath).Length > 0)
        {
            string node = "//Question[@id='" + EditID + "']";

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList aNodes = doc.SelectNodes(node);

            foreach (XmlNode aNode in aNodes)
            {
                XmlAttribute idAttribute = aNode.Attributes["id"];

                if (idAttribute != null)
                {
                    Debug.Log(EditID + " was loaded!");

                    questionText.text = aNode["question"].InnerText;
                    rightAnswerText.text = aNode["rightAnswer"].InnerText;
                    wrongAnswer1Text.text = aNode["wrongAnswer1"].InnerText;
                    wrongAnswer2Text.text = aNode["wrongAnswer2"].InnerText;
                    wrongAnswer3Text.text = aNode["wrongAnswer3"].InnerText;
                }
            }
        }
    }

    public void Save(int EditID , string question, string rightAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
    {
        if (File.Exists(filePath) && File.ReadAllLines(filePath).Length > 0)
        {
            string node = "//Question[@id='" + EditID + "']";

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList aNodes = doc.SelectNodes(node);

            foreach (XmlNode aNode in aNodes)
            {
                XmlAttribute idAttribute = aNode.Attributes["id"];

                if (idAttribute != null)
                {
                    if (System.Convert.ToInt32(idAttribute.Value) == EditID)
                    {
                        Debug.Log(EditID + " was saved!");

                        aNode["question"].InnerText = questionText.text;
                        aNode["rightAnswer"].InnerText = rightAnswerText.text;
                        aNode["wrongAnswer1"].InnerText = wrongAnswer1Text.text;
                        aNode["wrongAnswer2"].InnerText = wrongAnswer2Text.text;
                        aNode["wrongAnswer3"].InnerText = wrongAnswer3Text.text;
                    }
                }
            }
            doc.Save(filePath);
        }
    }
}