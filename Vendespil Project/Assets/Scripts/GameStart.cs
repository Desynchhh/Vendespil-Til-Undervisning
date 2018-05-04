using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;
using System.IO;

public class GameStart : MonoBehaviour
{
    [Header("Panels")]
    public GameObject EditMenu;
    public GameObject CreateMenu;
    public GameObject GameMenu;

    public void Awake()
    {
        if (File.Exists(Application.dataPath + "/XML/Data.xml") && File.ReadAllLines(Application.dataPath + "/XML/Data.xml").Length > 0)
        {
            GetComponent<Editor>().Read();
            LoadAllQuestions();
        }
        EditMenu.SetActive(false);
        GameMenu.SetActive(false);
        CreateMenu.SetActive(false);
    }

    public void LoadAllQuestions()
    {
        GetEditButtons();
        //GetGameButtons();
    }

    private void GetEditButtons()
    {
        XDocument xdoc = XDocument.Load(Application.dataPath + "/XML/Data.xml");

        foreach (XElement question in xdoc.Descendants("Question"))
        {
            EditMenu.transform.GetComponent<CreateButton>().SpawnEditButton();
            Debug.Log("Spawn Game Button");
        }
    }

    private void GetGameButtons()
    {
        foreach(Transform child in EditMenu.transform.Find("ScrollView").GetChild(0).GetChild(0))
        {
            ButtonManager questionInfo = child.GetComponent<ButtonManager>();
            EditMenu.transform.GetComponent<CreateButton>().SpawnGameButton(questionInfo.id, questionInfo.question, questionInfo.answer, questionInfo.wrongAnswer1, questionInfo.wrongAnswer2, questionInfo.wrongAnswer3);
        }
    }
}
