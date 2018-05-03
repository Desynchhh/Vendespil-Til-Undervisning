using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;

public class GameStart : MonoBehaviour
{
    public GameObject EditMenu;
    public GameObject GameMenu;

    public void Awake()
    {
        LoadAllQuestions();
    }

    public void LoadAllQuestions()
    {
        XDocument xdoc = XDocument.Load(Application.dataPath + "/XML/Data.xml");

        foreach(XElement question in xdoc.Descendants("Question"))
        {
            EditMenu.transform.GetComponent<CreateButton>().SpawnEditButton();
            //GameMenu.transform.GetComponent<CreateButton>().InstantiateNewButton()
        }
    }
}
