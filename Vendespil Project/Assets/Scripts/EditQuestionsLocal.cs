using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditQuestionsLocal : MonoBehaviour
{
    public void AddQuestion()
    {
        //Create a new button for the XML file
        transform.root.GetComponent<CreateButton>().InstantiateNewButton();
        //Edit the created question with EditQuestion() function
    }

    public void EditQuestion(string questionName)
    {
        //Deactivate "PanelEditMenu" and activate "edit question panel"
        //Overwrite previous data in XML with new data
        Debug.Log("EditQuestion");
    }

    public void DeleteQuestion(string questionName)
    {
        transform.root.GetComponent<CreateButton>().questionNumber--;
        Destroy(transform.Find("PanelEditMenu").Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).gameObject);
    }

    public void DeleteAllQuestions(GameObject content)
    {
        //Delete ALL question from XML file
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        transform.root.GetComponent<CreateButton>().questionNumber = 0;
    }
}
